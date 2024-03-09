using Grpc.Core.Interceptors;
using Grpc.Core;
using Invitation.Command.Exceptions;

namespace Invitation.Command.Interceptors
{
    public class HandleErrorInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (NotFoundException)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Result not found."));
            }
            catch (RuleVaildationException e)
            {
                throw new RpcException(new Status(StatusCode.FailedPrecondition, e.Message));
            }
        }
    }
}

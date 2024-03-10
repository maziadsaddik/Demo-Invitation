using Calzolari.Grpc.AspNetCore.Validation;
using Invitation.Command.Interceptors;
using Invitation.Command.Validators;

namespace Invitation.Command.Extensions.Services
{
    public static class  GrpcRegisterExtension
    {
        public static void AddGrpcWithValidators(this IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableMessageValidation();
                options.Interceptors.Add<HandleErrorInterceptor>();
            });

            AddValidators(services);
        }


        private static void AddValidators(IServiceCollection services)
        {
            services.AddGrpcValidation();
            services.AddValidator<InvitationRequestValidator>();
            services.AddValidator<InvitationInfoRequestValidator>();
        }
    }
}

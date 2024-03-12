using Grpc.Core;
using Invitation.Command.Extensions;
using MediatR;
namespace Invitation.Command.Services
{
    public class InvitationService(IMediator mediator,ILogger<InvitationService> logger) : Invitation.InvitationBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<InvitationService> _logger = logger;
 
        public override async Task<Response> SendInvitation(InvitationRequest request, ServerCallContext context)
        {
            var command = request.ToCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Member has Sended.",
                Id = id
            };
        }
        public override async Task<Response> Accept(InvitationInfoRequest request, ServerCallContext context)
        {
            var command = request.ToAcceptCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Member has Canceled.",
                Id = id
            };
        }

        public override async Task<Response> Cancel(InvitationInfoRequest request, ServerCallContext context)
        {
            var command = request.ToCommand();
            var id = await _mediator.Send(command);
        return new Response
        {
            Message = "Member has Canceled.",
            Id = id
            };
        }

        public override async Task<Response> Reject(InvitationInfoRequest request, ServerCallContext context)
        {
            var command = request.ToRejectCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Member has Rejected.",
                Id = id
            };
        }

        public override async Task<Response> JoinMember(InvitationRequest request, ServerCallContext context)
        {
            
            var command = request.ToJoinCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Join Succefully",
                Id = id
            };
        }

        public override async Task<Response> ChangePermissions(InvitationRequest request, ServerCallContext context)
        {
            var command = request.TochangePermissionCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Change Permissions Succefully",
                Id = id
            };
        }

        public override async Task<Response> LeaveMember(InvitationInfoRequest request, ServerCallContext context)
        {
            var command = request.ToLeaveCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Member has Leaved.",
                Id = id
            };
        }

        public override async Task<Response> RemoveMember(InvitationInfoRequest request, ServerCallContext context)
        {
            var command = request.ToRemoveCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Member has Canceled.",
                Id = id
            };
        }
    }
}

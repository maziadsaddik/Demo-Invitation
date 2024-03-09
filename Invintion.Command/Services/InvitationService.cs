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
                Id = "10"
            };
        }

        public override async Task<Response> Cancel(InvitationInfoRequest request, ServerCallContext context)
        {
            var command = request.ToCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Member has Canceled.",
                Id = "10"
            };
        }

        public override async Task<Response> Reject(InvitationInfoRequest request, ServerCallContext context)
        {
            var command = request.ToRejectCommand();
            var id = await _mediator.Send(command);
            return new Response
            {
                Message = "Member has Canceled.",
                Id = "10"
            };
        }

        //public override async Task<Response> JoinMemberByAdmin(InvitationRequest request, ServerCallContext context)
        //{
        //    //JoinInvitationCommand joinInvitationCommand = await request.ConvertTOJoinInvitationCommand();
        //    //int id = await mediator.Send(joinInvitationCommand);
        //    return new Response
        //    {
        //        Message = "Member has Canceled.",
        //        Id = "10"
        //    };
        //}

        //public override async Task<Response> ChangePermissions(InvitationRequest request, ServerCallContext context)
        //{
        //    //ChangePermissionInvitationCommand joinInvitationCommand = await request.ConvertTOChangePermissionInvitationCommand();
        //    //int id = await mediator.Send(joinInvitationCommand);
        //    return new Response
        //    {
        //        Message = "Member has Canceled.",
        //        Id = "10"
        //    };
        //}

        //public override async Task<Response> LeaveMember(InvitationInfoRequest request, ServerCallContext context)
        //{
        //    //LeaveInvitationCommand leaveInvitation = await request.ConvertTOLeaveInvitationCommand();
        //    //int id = await mediator.Send(leaveInvitation);
        //    return new Response
        //    {
        //        Message = "Member has Canceled.",
        //        Id = "10"
        //    };
        //}

        //public override async Task<Response> RemoveMember(InvitationInfoRequest request, ServerCallContext context)
        //{
        //    //RemoveInvitationCommand rejectInvitation = await request.ConvertTORemoveInvitationCommand();
        //    //int id = await mediator.Send(rejectInvitation);
        //    return new Response
        //    {
        //        Message = "Member has Canceled.",
        //        Id = "10"
        //    };
        //}
    }
}

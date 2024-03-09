using Invitation.Command.CommandHandlers.Accept;
using Invitation.Command.CommandHandlers.Cancel;
using Invitation.Command.CommandHandlers.Reject;
using Invitation.Command.CommandHandlers.Send;
using Invitation.Command.Domain;
using Invitation.Command.Domain.Records;
using Invitation.Command.Events;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;

namespace Invitation.Command.Extensions
{
    public static class CommandsExtensions
    {
        public static SendInvitationCommand ToCommand(this InvitationRequest request)
        {
            SendInvitationCommand sendInvitation = new SendInvitationCommand(
                accountId: request.InvitationInfo.AccountId,
                MemberId: request.InvitationInfo.MemberId,
                UserId: request.InvitationInfo.UserId,
                subscriptionId: request.InvitationInfo.SubscriptionId,
                Permissions: new List<Permission>()
                );
            foreach(var item in request.Permissions)
            {
                sendInvitation.Permissions.Add(
                    new Permission( Id:item.Id,Name:item.Name )
                    );
            }
            return sendInvitation;
        }
        public static CancelInvitationCommand ToCommand(this InvitationInfoRequest request)
        {
            CancelInvitationCommand sendInvitation = new CancelInvitationCommand(
                accountId: request.AccountId,
                MemberId: request.MemberId,
                UserId: request.UserId,
                subscriptionId: request.SubscriptionId
               );
            return sendInvitation;
        }

        public static AcceptInvitationCommand ToAcceptCommand(this InvitationInfoRequest request)
        {
            AcceptInvitationCommand sendInvitation = new AcceptInvitationCommand(
                accountId: request.AccountId,
                MemberId: request.MemberId,
                UserId: request.UserId,
                subscriptionId: request.SubscriptionId
               );
            return sendInvitation;
        }
        public static RejectInvitationCommand ToRejectCommand(this InvitationInfoRequest request)
        {
            RejectInvitationCommand sendInvitation = new RejectInvitationCommand(
                accountId: request.AccountId,
                MemberId: request.MemberId,
                UserId: request.UserId,
                subscriptionId: request.SubscriptionId
               );
            return sendInvitation;
        }
    }
}

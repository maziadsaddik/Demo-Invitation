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
    }
}

using Invitation.Command.CommandHandlers.Accept;
using Invitation.Command.CommandHandlers.Cancel;
using Invitation.Command.CommandHandlers.ChangePermissions;
using Invitation.Command.CommandHandlers.Join;
using Invitation.Command.CommandHandlers.Leave;
using Invitation.Command.CommandHandlers.Reject;
using Invitation.Command.CommandHandlers.Remove;
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
            CancelInvitationCommand CancelInvitation = new CancelInvitationCommand(
                accountId: request.AccountId,
                MemberId: request.MemberId,
                UserId: request.UserId,
                subscriptionId: request.SubscriptionId
               );
            return CancelInvitation;
        }

        public static AcceptInvitationCommand ToAcceptCommand(this InvitationInfoRequest request)
        {
            AcceptInvitationCommand AcceptInvitation = new AcceptInvitationCommand(
                accountId: request.AccountId,
                MemberId: request.MemberId,
                UserId: request.UserId,
                subscriptionId: request.SubscriptionId
               );
            return AcceptInvitation;
        }
        public static RejectInvitationCommand ToRejectCommand(this InvitationInfoRequest request)
        {
            RejectInvitationCommand RejectInvitation = new RejectInvitationCommand(
                accountId: request.AccountId,
                MemberId: request.MemberId,
                UserId: request.UserId,
                subscriptionId: request.SubscriptionId
               );
            return RejectInvitation;
        }

        public static JoinInvitationCommand ToJoinCommand(this InvitationRequest request)
        {
            JoinInvitationCommand JoinInvitation = new JoinInvitationCommand(
                accountId: request.InvitationInfo.AccountId,
                MemberId: request.InvitationInfo.MemberId,
                UserId: request.InvitationInfo.UserId,
                subscriptionId: request.InvitationInfo.SubscriptionId,
                Permissions: new List<Permission>()
                );
            foreach (var item in request.Permissions)
            {
                JoinInvitation.Permissions.Add(
                    new Permission(Id: item.Id, Name: item.Name)
                    );
            }
            return JoinInvitation;
        }
        public static ChangePermissionsInvitationCommand TochangePermissionCommand(this InvitationRequest request)
        {
            ChangePermissionsInvitationCommand ChangePermission = new ChangePermissionsInvitationCommand(
                accountId: request.InvitationInfo.AccountId,
                MemberId: request.InvitationInfo.MemberId,
                UserId: request.InvitationInfo.UserId,
                subscriptionId: request.InvitationInfo.SubscriptionId,
                Permissions: new List<Permission>()
                );
            foreach (var item in request.Permissions)
            {
                ChangePermission.Permissions.Add(
                    new Permission(Id: item.Id, Name: item.Name)
                    );
            }
            return ChangePermission;
        }
        public static RemoveIvitationCommand ToRemoveCommand(this InvitationInfoRequest request)
        {
            RemoveIvitationCommand RemoveInvitation = new RemoveIvitationCommand(
                accountId: request.AccountId,
                MemberId: request.MemberId,
                UserId: request.UserId,
                subscriptionId: request.SubscriptionId
               );
            return RemoveInvitation;
        }

        public static LeaveInvitationCommand ToLeaveCommand(this InvitationInfoRequest request)
        {
            LeaveInvitationCommand LeaveInvitation = new LeaveInvitationCommand(
                accountId: request.AccountId,
                MemberId: request.MemberId,
                UserId: request.UserId,
                subscriptionId: request.SubscriptionId
               );
            return LeaveInvitation;
        }
    }
}

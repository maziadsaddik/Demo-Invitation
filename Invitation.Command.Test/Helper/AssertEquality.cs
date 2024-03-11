using Invitation.Command.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invitation.Command.Test.Helper
{
    public class AssertEquality
    {
        public static void OfSendedEvent(Event @event, InvitationRequest request, Response response)
        {
            var created = (InvitationSended)@event;

            Assert.Equal(response.Id, created.AggregateId.ToString());
            Assert.Equal(request.InvitationInfo.UserId, created.Data.UserId);
            Assert.Equal(DateTime.UtcNow, created.DateTime, TimeSpan.FromMinutes(1));
            Assert.Equal(1, created.Sequence);
            Assert.Equal(1, created.Version);
            Assert.Equal(request.InvitationInfo.AccountId, created.Data.AccountId);
            Assert.Equal(request.InvitationInfo.MemberId, created.Data.MemberId);
            Assert.Equal(request.InvitationInfo.SubscriptionId, created.Data.SubscriptionId);
            Assert.NotEmpty(created.Data.Permissions);
        }
        public static void OfCanceledEvent(Event @event, InvitationInfoRequest request, Response response, int expectedSequence)
        {
            var created = (InvitationCanceled)@event;

            Assert.Equal(response.Id, created.AggregateId.ToString());
            Assert.Equal(request.UserId, created.Data.UserId);
            Assert.Equal(DateTime.UtcNow, created.DateTime, TimeSpan.FromMinutes(1));
            Assert.Equal(expectedSequence, created.Sequence);
            Assert.Equal(1, created.Version);
            Assert.Equal(request.AccountId, created.Data.AccountId);
            Assert.Equal(request.MemberId, created.Data.MemberId);
            Assert.Equal(request.SubscriptionId, created.Data.SubscriptionId);
        }
        public static void OfRejectedEvent(Event @event, InvitationInfoRequest request, Response response, int expectedSequence)
        {
            var created = (InvitationRejected)@event;

            Assert.Equal(response.Id, created.AggregateId.ToString());
            Assert.Equal(request.UserId, created.Data.UserId);
            Assert.Equal(DateTime.UtcNow, created.DateTime, TimeSpan.FromMinutes(1));
            Assert.Equal(expectedSequence, created.Sequence);
            Assert.Equal(1, created.Version);
            Assert.Equal(request.AccountId, created.Data.AccountId);
            Assert.Equal(request.MemberId, created.Data.MemberId);
            Assert.Equal(request.SubscriptionId, created.Data.SubscriptionId);
        }
        public static void OfAcceptedEvent(Event @event, InvitationInfoRequest request, Response response, int expectedSequence)
        {
            var created = (InvitationAccepted)@event;

            Assert.Equal(response.Id, created.AggregateId.ToString());
            Assert.Equal(request.UserId, created.Data.UserId);
            Assert.Equal(DateTime.UtcNow, created.DateTime, TimeSpan.FromMinutes(1));
            Assert.Equal(expectedSequence, created.Sequence);
            Assert.Equal(1, created.Version);
            Assert.Equal(request.AccountId, created.Data.AccountId);
            Assert.Equal(request.MemberId, created.Data.MemberId);
            Assert.Equal(request.SubscriptionId, created.Data.SubscriptionId);
        }
    }
}

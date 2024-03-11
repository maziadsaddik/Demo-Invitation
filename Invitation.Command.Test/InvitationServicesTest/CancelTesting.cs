using Grpc.Core;
using Invitation.Command.Test.Helper;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Invitation.Command.Abstractions.Persistence;
using Calzolari.Grpc.Net.Client.Validation;

namespace Invitation.Command.Test.InvitationServicesTest
{
    public class CancelTesting : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CancelTesting(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
        {
            _factory = factory.WithDefaultConfigurations(helper, services =>
            {
                services.ReplaceWithInMemoryDatabase();
            });
        }

        [Theory]
        [InlineData("0558047", "1", "2", "90")]
        [InlineData("0558045", "2", "3", "19")]
        [InlineData("0558048", "1", "3", "50")]
        public async Task Cancel_SendValidRequest_InvitationCanceledEventSaved(
            string accountId,
            string userId,
        string memberId,
            string subscriptionId
        )
        {
            var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

            var invitationRequest = new InvitationRequest();
            invitationRequest.InvitationInfo = new InvitationInfoRequest()
            {
                AccountId = accountId,
                UserId = userId,
                MemberId = memberId,
                SubscriptionId = subscriptionId,
            };
            invitationRequest.Permissions.Add(new Permissions
            {
                Id = "1",
                Name = "Transfer"
            });
            invitationRequest.Permissions.Add(new Permissions
            {
                Id = "2",
                Name = "PurchaseCards"
            });

            await client.SendInvitationAsync(invitationRequest);
            var response = await client.CancelAsync(invitationRequest.InvitationInfo);
            using var scope = _factory.Services.CreateScope();
            var stream = scope.ServiceProvider.GetRequiredService<IEventStore>();
            var @events = await stream.GetStreamAsync(response.Id);

            Assert.Equal(2,events.Count());
            AssertEquality.OfCanceledEvent(events[1], invitationRequest.InvitationInfo, response, expectedSequence: 2);
        }
        [Theory]
        [InlineData(false, true, true, true, nameof(InvitationRequest.InvitationInfo.AccountId))]
        [InlineData(true, false, true, true, nameof(InvitationRequest.InvitationInfo.UserId))]
        [InlineData(true, true, false, true, nameof(InvitationRequest.InvitationInfo.MemberId))]
        [InlineData(true, true, true, false, nameof(InvitationRequest.InvitationInfo.SubscriptionId))]

        public async Task Send_SendInvalidRequest_ThrowsInvalidArgumentRpcException(
            bool validaccountId,
            bool validuserId,
            bool validmemberId,
            bool validsubscriptionId,
            string errorPropertyName
        )
        {
            var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

            var accountId = validaccountId ? Guid.NewGuid().ToString() : " ";
            var userId = validuserId ? Guid.NewGuid().ToString() : " ";
            var memberId = validmemberId ? Guid.NewGuid().ToString() : " ";
            var subscriptionId = validsubscriptionId ? Guid.NewGuid().ToString() : " ";


            var request = new InvitationRequest();

            request.InvitationInfo = new InvitationInfoRequest()
            {
                AccountId = accountId,
                UserId = userId,
                MemberId = memberId,
                SubscriptionId = subscriptionId,
            };
            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.CancelAsync(request.InvitationInfo));

            Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);
            Assert.Contains(
                exception.GetValidationErrors(),
                e => e.PropertyName.EndsWith(errorPropertyName)
            );
        }
        [Fact]
        public async Task CancelInvitation_MemberIsFoundInSubscriptionButNeedToCancel_Successfully()
        {
            var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());


            InvitationRequest invitationRequest = new InvitationRequest();
            invitationRequest.InvitationInfo = new InvitationInfoRequest()
            {
                AccountId = "2",
                UserId = "2",
                MemberId = "3",
                SubscriptionId = "91"
            };
            invitationRequest.Permissions.Add(new Permissions
            {
                Id = "1",
                Name = "Transfer"
            });
            invitationRequest.Permissions.Add(new Permissions
            {
                Id = "2",
                Name = "PurchaseCards"
            });
            await client.SendInvitationAsync(invitationRequest);
            var response = await client.CancelAsync(invitationRequest.InvitationInfo);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task CancelInvitation_MemberIsNotFoundInSubscription_Exception()
        {
            Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

            InvitationInfoRequest invitationInfo = new InvitationInfoRequest()
            {
                AccountId ="2",
                UserId = "2",
                MemberId = "3",
                SubscriptionId = "91"
            };
            await Assert.ThrowsAsync<RpcException>(async () =>
            {
                await client.CancelAsync(invitationInfo);
            });
        }
    }
}

using Calzolari.Grpc.Net.Client.Validation;
using Grpc.Core;
using Invitation.Command.Test.Helper;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Invitation.Command.Test.InvitationServicesTest
{
    public class LeaveTesting : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public LeaveTesting(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
        {
            _factory = factory.WithDefaultConfigurations(helper, services =>
            {
                services.ReplaceWithInMemoryDatabase();
            });
        }
        [Theory]
        [InlineData(false, true, true, true, nameof(InvitationRequest.InvitationInfo.AccountId))]
        [InlineData(true, false, true, true, nameof(InvitationRequest.InvitationInfo.UserId))]
        [InlineData(true, true, false, true, nameof(InvitationRequest.InvitationInfo.MemberId))]
        [InlineData(true, true, true, false, nameof(InvitationRequest.InvitationInfo.SubscriptionId))]

        public async Task Leave_SendInvalidRequest_ThrowsInvalidArgumentRpcException(
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
            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.LeaveMemberAsync(request.InvitationInfo));

            Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);
            Assert.Contains(
                exception.GetValidationErrors(),
                e => e.PropertyName.EndsWith(errorPropertyName)
            );
        }
        [Fact]
        public async Task Leave_MemberIsAlreadyJoined_Successfully()
        {
            Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

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
            await client.AcceptAsync(invitationRequest.InvitationInfo);
            var response = await client.LeaveMemberAsync(invitationRequest.InvitationInfo);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Leave_MemberIsNotJoined_Exception()
        {
            Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());


            InvitationInfoRequest invitationInfo = new InvitationInfoRequest()
            {
                AccountId = "2",
                UserId = "2",
                MemberId = "3",
                SubscriptionId ="91"
            };
            await Assert.ThrowsAsync<RpcException>(async () =>
            {
                await client.LeaveMemberAsync(invitationInfo);
            });
        }

        [Fact]
        public async Task Leave_InvitationWasSentButMemberDidNotSeeItToAcceptOrCancel_Exception()
        {
            Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

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
            await Assert.ThrowsAsync<RpcException>(async () =>
            {
                await client.LeaveMemberAsync(invitationRequest.InvitationInfo);
            });
        }
    }
}

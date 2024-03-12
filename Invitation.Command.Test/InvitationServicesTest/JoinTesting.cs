using Calzolari.Grpc.Net.Client.Validation;
using Grpc.Core;
using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.Test.Helper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Invitation.Command.Test.InvitationServicesTest
{
    public class JoinTesting : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public JoinTesting(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
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
        public async Task Join_SendValidRequest_InvitationJoinedEventSaved(
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

            var response = await client.JoinMemberAsync(invitationRequest);

            using var scope = _factory.Services.CreateScope();
            var stream = scope.ServiceProvider.GetRequiredService<IEventStore>();
            var events = await stream.GetStreamAsync(response.Id);

            Assert.Single(events);
            AssertEquality.OfJoinedEvent(events[0], invitationRequest, response);
        }

        [Theory]
        [InlineData(false, true, true, true, true, nameof(InvitationRequest.InvitationInfo.AccountId))]
        [InlineData(true, false, true, true, true, nameof(InvitationRequest.InvitationInfo.UserId))]
        [InlineData(true, true, false, true, true, nameof(InvitationRequest.InvitationInfo.MemberId))]
        [InlineData(true, true, true, false, true, nameof(InvitationRequest.InvitationInfo.SubscriptionId))]
        [InlineData(true, true, true, true, false, nameof(InvitationRequest.Permissions))]
        public async Task Join_SendInvalidRequest_ThrowsInvalidArgumentRpcException(
            bool validaccountId,
            bool validuserId,
            bool validmemberId,
            bool validsubscriptionId,
            bool validPermissions,
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
            if (validPermissions)
            {
                request.Permissions.Add(new Permissions
                {
                    Id = "1",
                    Name = "Transfer"
                });
                request.Permissions.Add(new Permissions
                {
                    Id = "2",
                    Name = "PurchaseCards"
                });
            }
            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.JoinMemberAsync(request));

            Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);
            Assert.Contains(
                exception.GetValidationErrors(),
                e => e.PropertyName.EndsWith(errorPropertyName)
            );
        }
        [Fact]
        public async Task Join_MemberIsNotJoined_Successfully()
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
            var response = await client.JoinMemberAsync(invitationRequest);
            Assert.NotNull(response);
        }
        //[Fact]
        //public async Task Join_MemberIsAlreadyJoined_Exception()
        //{
        //    Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

        //    InvitationRequest invitationRequest = new InvitationRequest();
        //    invitationRequest.InvitationInfo = new InvitationInfoRequest()
        //    {
        //        AccountId = 2,
        //        UserId = 2,
        //        MemberId = 3,
        //        SubscriptionId = 91
        //    };
        //    invitationRequest.Permissions.Add(new Permissions
        //    {
        //        Id = 1,
        //        Name = "Transfer"
        //    });
        //    invitationRequest.Permissions.Add(new Permissions
        //    {
        //        Id = 2,
        //        Name = "PurchaseCards"
        //    });
        //    await client.SendInvitationToMemberAsync(invitationRequest);
        //    await client.AcceptAsync(invitationRequest.InvitationInfo);
        //    await Assert.ThrowsAsync<RpcException>(async () =>
        //    {
        //        await client.JoinMemberByAdminAsync(invitationRequest);
        //    });
        //}
    }
}

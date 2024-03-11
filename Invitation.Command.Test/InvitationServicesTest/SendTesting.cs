using Azure.Core;
using Calzolari.Grpc.Net.Client.Validation;
using Google.Protobuf.Collections;
using Grpc.Core;
using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.Domain.Records;

using Invitation.Command.Infrastructure.database;
using Invitation.Command.Test.Helper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Invitation.Command.Test.InvitationServicesTest
{
    public class SendTesting:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SendTesting(WebApplicationFactory<Program> factory,ITestOutputHelper helper)
        {
            _factory = factory.WithDefaultConfigurations(helper, services =>
            {
                services.ReplaceWithInMemoryDatabase();
            });
        }

        [Theory]
        [InlineData("0558047", "1", "2", "90")]
        [InlineData("0558045", "2", "3", "19")]
        [InlineData("0558048", "1", "3","50")]
        public async Task Send_SendValidRequest_InvitationSendededEventSaved(
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
            
            var response = await client.SendInvitationAsync(invitationRequest);

            using var scope = _factory.Services.CreateScope();
            var stream = scope.ServiceProvider.GetRequiredService<IEventStore>();
            var events = await stream.GetStreamAsync(response.Id);

            Assert.Single(events);
            AssertEquality.OfSendedEvent(events[0], invitationRequest, response);
        }
        [Theory]
        [InlineData(false, true,true, true,true ,nameof(InvitationRequest.InvitationInfo.AccountId))]
        [InlineData(true, false, true, true, true, nameof(InvitationRequest.InvitationInfo.UserId))]
        [InlineData(true, true, false, true, true, nameof(InvitationRequest.InvitationInfo.MemberId))]
        [InlineData(true, true, true, false, true, nameof(InvitationRequest.InvitationInfo.SubscriptionId))]
        [InlineData(true, true, true, true, false, nameof(InvitationRequest.Permissions))]
        public async Task Send_SendInvalidRequest_ThrowsInvalidArgumentRpcException(
            bool validaccountId,
            bool validuserId,
            bool validmemberId,
            bool validsubscriptionId,
            bool validPermissions,
            string errorPropertyName
        )
        {
            var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

            var accountId = validaccountId ? Guid.NewGuid().ToString(): " ";
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
            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.SendInvitationAsync(request));

            Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);
            Assert.Contains(
                exception.GetValidationErrors(),
                e => e.PropertyName.EndsWith(errorPropertyName)
            );
        }
        [Fact]
        public async Task Send_MemberWassendedAnddoCancaltoInvitationthanSendagain_Successfully()
        {
            var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

            InvitationRequest invitationRequest = new InvitationRequest();
            invitationRequest.InvitationInfo = new InvitationInfoRequest()
            {
                AccountId = "2",
                UserId = "2",
                MemberId = "2",
                SubscriptionId = "1"
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
            await client.CancelAsync(invitationRequest.InvitationInfo);
            var response = await client.SendInvitationAsync(invitationRequest);
            Assert.NotNull(response);

        }
        [Fact]
        public async Task Send_MemberSendedThanRejectToInvitationThanSendAgain_Successfully()
        {
            var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

            InvitationRequest invitationRequest = new InvitationRequest();
            invitationRequest.InvitationInfo = new InvitationInfoRequest()
            {
                AccountId = "2",
                UserId = "2",
                MemberId = "2",
                SubscriptionId = "1"
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
            await client.RejectAsync(invitationRequest.InvitationInfo);
            
            var response = await client.SendInvitationAsync(invitationRequest);
            Assert.NotNull(response);
        }
        [Fact]
        public async Task Send_MemberSendedInvitationTwice_Exception()
        {
            var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

            InvitationRequest invitationRequest = new InvitationRequest();
            invitationRequest.InvitationInfo = new InvitationInfoRequest()
            {
                AccountId = "2",
                UserId = "2",
                MemberId = "2",
                SubscriptionId = "1"
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
            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.SendInvitationAsync(invitationRequest));
            Assert.Equal(StatusCode.FailedPrecondition, exception.StatusCode);
        }
        [Fact]
        public async Task Send_MemberSendedAndDoAcceptToInvitationThanSendAgain_Exception()
        {
            var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

            InvitationRequest invitationRequest = new InvitationRequest();
            invitationRequest.InvitationInfo = new InvitationInfoRequest()
            {
                AccountId = "2",
                UserId = "2",
                MemberId = "2",
                SubscriptionId = "1"
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
            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.SendInvitationAsync(invitationRequest));
            Assert.Equal(StatusCode.FailedPrecondition, exception.StatusCode);
        }
    }


    // Joined  => exited => send 
    //[Fact]
    //public async Task SendNewInvitation_MemberWasJoinedAndNeedToRejoinAfterExit_Successfully()
    //{
    //    var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

    //    InvitationRequest invitationRequest = new InvitationRequest();
    //    invitationRequest.InvitationInfo = new InvitationInfoRequest()
    //    {
    //        AccountId = "2",
    //        UserId = "2",
    //        MemberId = "2",
    //        SubscriptionId = "1"
    //    };
    //    invitationRequest.Permissions.Add(new Permissions
    //    {
    //        Id = "1",
    //        Name = "Transfer"
    //    });
    //    invitationRequest.Permissions.Add(new Permissions
    //    {
    //        Id ="2",
    //        Name = "PurchaseCards"
    //    });
    //    await client.SendInvitationAsync(invitationRequest);
    //    var response = await client.AcceptAsync(invitationRequest.InvitationInfo);
    //    //await client.LeaveMemberAsync(invitationRequest.InvitationInfo);

    //    //var response = await client.JoinMemberByAdminAsync(invitationRequest);
    //    Assert.NotNull(response);
    //}
   // [Fact]
    //public async Task SendNewInvitation_AlreadyExists_Exception(){
    //    Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

    //    InvitationRequest invitationRequest = new InvitationRequest();
    //    invitationRequest.InvitationInfo = new InvitationInfoRequest()
    //    {
    //        AccountId = 2,
    //        UserId = 2,
    //        MemberId = 3,
    //        SubscriptionId = 1
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
    //        await client.SendInvitationToMemberAsync(invitationRequest);
    //    });
    //}

    //// send invite and then need some time to response member (accept | reject)
    //[Fact] 
    //public async Task SendNewInvitation_Pinding_Exception(){
    //    Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

    //    InvitationRequest invitationRequest = new InvitationRequest();
    //    invitationRequest.InvitationInfo = new InvitationInfoRequest()
    //    {
    //        AccountId = 2,
    //        UserId = 2,
    //        MemberId = 3,
    //        SubscriptionId = 1
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
    //    await Assert.ThrowsAsync<RpcException>(async () =>
    //    {
    //        await client.SendInvitationToMemberAsync(invitationRequest);
    //    });
    //}
    //}
}

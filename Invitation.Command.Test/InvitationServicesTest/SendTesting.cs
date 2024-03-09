using Google.Protobuf.Collections;
using Grpc.Core;
using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.Infrastructure.database;
using Invitation.Command.Test.Helper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<InvitationDbContext>();
            //var stream = _factory.Services.GetRequiredService<IEventStore>();
            var response = await client.SendInvitationAsync(invitationRequest);


            //var @events = await stream.GetStreamAsync(response.Id);

            //Assert.Single(events);
            //AssertEquality.OfCreatedEvent(events[0], request, response);
        }
        //[Theory]
        //[InlineData(false, "Workout", "2022-03-27", "",nameof(InvitationRequest.InvitationInfo.AccountId))]
        //[InlineData(true, " ", "2022-03-27", "", nameof(InvitationRequest.InvitationInfo.UserId))]
        //[InlineData(true, "Read a book", "1800-03-27", "", nameof(InvitationRequest.InvitationInfo.MemberId))]
        //[InlineData(true, "Read a book", "2200-03-27", "", nameof(InvitationRequest.InvitationInfo.SubscriptionId))]
        //public async Task Create_SendInvalidRequest_ThrowsInvalidArgumentRpcException(
        //    bool validaccountId,
        //    string userId,
        //    string memberId,
        //    string subscriptionId,
        //    string errorPropertyName
        //)
        //{
        //    var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

        //    var accountId = validaccountId ? Guid.NewGuid().ToString() : " ";

        //    var request = new InvitationRequest()
        //    {
        //        UserId = userId,
        //        Title = title,
        //        DueDate = ProtoConverters.ToUtcTimestamp(dueDateString),
        //    };

        //    var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.CreateAsync(request));

        //    Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);
        //    Assert.Contains(
        //        exception.GetValidationErrors(),
        //        e => e.PropertyName.EndsWith(errorPropertyName)
        //    );
        //}
        //[Fact]
        //public async Task SendNewInvitation_FirstTime_Successfully()
        //{
        //    var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

        //InvitationRequest invitationRequest = new InvitationRequest();
        //invitationRequest.InvitationInfo = new InvitationInfoRequest()
        //{
        //    AccountId = "1",
        //    UserId = "1",
        //    MemberId = "2",
        //    SubscriptionId = "90"
        //};
        //invitationRequest.Permissions.Add(new Permissions
        //{
        //    Id = "1",
        //    Name = "Transfer"
        //});
        //invitationRequest.Permissions.Add(new Permissions
        //{
        //    Id = "2",
        //    Name = "PurchaseCards"
        //});

        //var response = await client.SendInvitationAsync(invitationRequest);

        //Assert.NotNull(response);
    }


        // Joined  => exited => send 
        //[Fact] 
        //public async Task SendNewInvitation_MemberWasJoinedAndNeedToRejoinAfterExit_Successfully()
        //{
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
        //    await client.LeaveMemberAsync(invitationRequest.InvitationInfo);

        //    var response = await client.JoinMemberByAdminAsync(invitationRequest);
        //    Assert.NotNull(response);
        //} 
        //[Fact] 
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

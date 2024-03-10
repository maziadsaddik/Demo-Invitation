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

        //[Fact]
        //public async Task CancelInvitation_MemberIsFoundInSubscriptionButNeedToCancel_Successfully()
        //{
        //    var client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());


        //    InvitationRequest invitationRequest = new InvitationRequest();
        //    invitationRequest.InvitationInfo = new InvitationInfoRequest()
        //    {
        //        AccountId = "2",
        //        UserId = "2",
        //        MemberId = "3",
        //        SubscriptionId = "91"
        //    };
        //    invitationRequest.Permissions.Add(new Permissions
        //    {
        //        Id = "1",
        //        Name = "Transfer"
        //    });
        //    invitationRequest.Permissions.Add(new Permissions
        //    {
        //        Id = "2",
        //        Name = "PurchaseCards"
        //    });
        //    await client.SendInvitationAsync(invitationRequest);
        //    var response = await client.CancelAsync(invitationRequest.InvitationInfo);
        //    Assert.NotNull(response);
        //}

        //[Fact]
        //public async Task CancelInvitation_MemberIsNotFoundInSubscription_Exception()
        //{
        //    //Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

        //    //InvitationInfoRequest invitationInfo = new InvitationInfoRequest()
        //    //{
        //    //    AccountId = 2,
        //    //    UserId = 2,
        //    //    MemberId = 3,
        //    //    SubscriptionId = 91
        //    //};
        //    //await Assert.ThrowsAsync<RpcException>(async () =>
        //    //{
        //    //    await client.CancelAsync(invitationInfo);
        //    //});
        //}
    }
}

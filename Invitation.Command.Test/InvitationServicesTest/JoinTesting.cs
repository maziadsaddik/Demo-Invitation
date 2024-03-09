//using Grpc.Core;
//using InvintionCommandTest.Helper;
//using InvitationCommandTest;
//using Microsoft.AspNetCore.Mvc.Testing;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit.Abstractions;

//namespace InvintionCommandTest.Tests
//{
//    public class JoinTesting : IClassFixture<WebApplicationFactory<Program>>
//    {
//        private readonly WebApplicationFactory<Program> _factory;

//        public JoinTesting(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
//        {
//            _factory = factory.WithDefaultConfigurations(helper, services =>
//            {
//                services.ReplaceWithInMemoryDatabase();
//            });
//        }

//        [Fact] 
//        public async Task Join_MemberIsNotJoined_Successfully() {
//            Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

//            InvitationRequest invitationRequest = new InvitationRequest();
//            invitationRequest.InvitationInfo = new InvitationInfoRequest()
//            {
//                AccountId = 2,
//                UserId = 2,
//                MemberId = 3,
//                SubscriptionId = 91
//            };
//            invitationRequest.Permissions.Add(new Permissions
//            {
//                Id = 1,
//                Name = "Transfer"
//            });
//            invitationRequest.Permissions.Add(new Permissions
//            {
//                Id = 2,
//                Name = "PurchaseCards"
//            });
//            var response = await client.JoinMemberByAdminAsync(invitationRequest);
//            Assert.NotNull(response);
//        }
//        [Fact] 
//        public async Task Join_MemberIsAlreadyJoined_Exception() {
//            Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

//            InvitationRequest invitationRequest = new InvitationRequest();
//            invitationRequest.InvitationInfo = new InvitationInfoRequest()
//            {
//                AccountId = 2,
//                UserId = 2,
//                MemberId = 3,
//                SubscriptionId = 91
//            };
//            invitationRequest.Permissions.Add(new Permissions
//            {
//                Id = 1,
//                Name = "Transfer"
//            });
//            invitationRequest.Permissions.Add(new Permissions
//            {
//                Id = 2,
//                Name = "PurchaseCards"
//            });
//            await client.SendInvitationToMemberAsync(invitationRequest);
//            await client.AcceptAsync(invitationRequest.InvitationInfo);
//            await Assert.ThrowsAsync<RpcException>(async () =>
//            {
//                await client.JoinMemberByAdminAsync(invitationRequest);
//            });
//        }
//    }
//}

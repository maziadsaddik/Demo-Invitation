//using Grpc.Core;
//using InvintionCommandTest.Helper;

//using InvitationCommandTest;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Xunit.Abstractions;

//namespace InvintionCommandTest.Tests
//{
//    public class RemoveTesting : IClassFixture<WebApplicationFactory<Program>>
//    {
//        private readonly WebApplicationFactory<Program> _factory;

//        public RemoveTesting(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
//        {
//            _factory = factory.WithDefaultConfigurations(helper, services =>
//            {
//                services.ReplaceWithInMemoryDatabase();
                
//            });
//        }

//        [Fact] public async Task Remove_MemberIsAlreadyJoined_Successfully() {
//            Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());

//            InvitationRequest invitationRequest = new InvitationRequest();
//            invitationRequest.InvitationInfo = new InvitationInfoRequest()
//            {
//                AccountId = 2,
//                UserId = 2,
//                MemberId = 5,
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
//            var response = await client.RemoveMemberAsync(invitationRequest.InvitationInfo);
//            Assert.NotNull(response);
//        }

//        [Fact] 
//        public async Task Remove_MemberIsNotJoined_Exception() {
//            Invitation.InvitationClient client = new Invitation.InvitationClient(_factory.CreateGrpcChannel());
//            InvitationInfoRequest invitationInfo = new InvitationInfoRequest()
//            {
//                AccountId = 2,
//                UserId = 2,
//                MemberId = 3,
//                SubscriptionId = 91
//            };
//            await Assert.ThrowsAsync<RpcException>(async () =>
//            {
//                await client.RemoveMemberAsync(invitationInfo);
//            });
//        }
//    }
//}

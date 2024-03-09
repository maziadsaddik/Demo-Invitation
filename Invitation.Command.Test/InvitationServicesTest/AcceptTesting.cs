//using Grpc.Core;
//using InvintionCommandTest.Helper;
//using InvitationCommandTest;
//using Microsoft.AspNetCore.Mvc.Testing;

//using Xunit.Abstractions;

//namespace InvintionCommandTest.Tests
//{
//    public class AcceptTesting : IClassFixture<WebApplicationFactory<Program>>
//    {
//        private readonly WebApplicationFactory<Program> _factory;

//        public AcceptTesting(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
//        {
//            _factory = factory.WithDefaultConfigurations(helper, services =>
//            {
//                services.ReplaceWithInMemoryDatabase();
//            });
//        }

//        [Fact] 
//        public async Task AcceptInvitation_Found_Successfully() {
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
//            var response = await client.AcceptAsync(invitationRequest.InvitationInfo);
//            Assert.NotNull(response);
//        }
//        [Fact] 
//        public async Task AccceptInvitation_Notfound_Exception() {
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
//                await client.AcceptAsync(invitationInfo);
//            });
//        }
//    }
//}

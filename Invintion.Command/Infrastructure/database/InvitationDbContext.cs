using Invitation.Command.Events;
using Invitation.Command.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Invitation.Command.Infrastructure.database
{
    public class InvitationDbContext(DbContextOptions<InvitationDbContext> options) : DbContext(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageConfigurations());
            modelBuilder.ApplyConfiguration(new BaseEventConfigurations());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationSended, InvitationSendedData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationAccepted, InvitationAcceptedData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationCanceled, InvitationCanceledData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationRejected, InvitationRejectedData>());

            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationJoined, InvitationJoinedData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationChangedPermission, InvitationChangedPermissionData>());


            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationRemoved, InvitationRemovedData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationLeaved, InvitationLeavedData>());
        }
    }
}

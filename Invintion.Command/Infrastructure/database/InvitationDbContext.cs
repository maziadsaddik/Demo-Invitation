using Invitation.Command.Events;
using Invitation.Command.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Invitation.Command.Infrastructure.database
{
    public class InvitationDbContext(DbContextOptions<InvitationDbContext> options) : DbContext(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<Student> Student { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageConfigurations());
            modelBuilder.ApplyConfiguration(new BaseEventConfigurations());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationSended, InvitationSendedData>());
            //modelBuilder.ApplyConfiguration(new GenericEventConfiguration<JoinInvitationEventEntity, InvitationData>());
            //modelBuilder.ApplyConfiguration(new GenericEventConfiguration<ChangePermissionsInvitationEventEntity, InvitationData>());
            //modelBuilder.ApplyConfiguration(new GenericEventConfiguration<AcceptInvitationEventEntity, InvitationInfoData>());
            //modelBuilder.ApplyConfiguration(new GenericEventConfiguration<CancelInvitationEventEntity, InvitationInfoData>());
            //modelBuilder.ApplyConfiguration(new GenericEventConfiguration<RejectInvitationEventEntity, InvitationInfoData>());
            //modelBuilder.ApplyConfiguration(new GenericEventConfiguration<RemoveInvitationEventEntity, InvitationInfoData>());
            //modelBuilder.ApplyConfiguration(new GenericEventConfiguration<LeaveInvitationEventEntity, InvitationInfoData>());
        }
    }

    public class Student
    {
        public int Id { get; set; }
    }
}

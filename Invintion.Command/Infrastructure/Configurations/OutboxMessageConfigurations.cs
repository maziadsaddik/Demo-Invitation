using Invitation.Command.Infrastructure.database;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Invitation.Command.Infrastructure.Configurations
{
    public class OutboxMessageConfigurations : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasOne(x => x.Event)
                .WithOne()
                .HasForeignKey<OutboxMessage>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

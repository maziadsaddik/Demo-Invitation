using Invitation.Command.Events;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Invitation.Command.Infrastructure.Configurations
{
    public class GenericEventConfiguration<TEntity, TData> : IEntityTypeConfiguration<TEntity>
            where TEntity : Event<TData>
            where TData : class
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.Data).HasConversion(
                 x => Serialize(x),
                 x => Deserialize(x)
            ).HasColumnName("Data");
        }

        private static JsonSerializerOptions GetJsonSerializerOptions() => new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private static string Serialize(TData data) => JsonSerializer.Serialize(data, GetJsonSerializerOptions());
        private static TData Deserialize(string data) => JsonSerializer.Deserialize<TData>(data, GetJsonSerializerOptions())
            ?? throw new InvalidOperationException("Failed to deserialize JSON data");
    }
}

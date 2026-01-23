using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class SomeEventConfiguration : IEntityTypeConfiguration<SomeEvent>
{
    public void Configure(EntityTypeBuilder<SomeEvent> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x => x.EventDate);
        builder.Property(x => x.OrganizationId);
        builder.Property(x => x.MinParticipant);
        builder.Property(x => x.MaxParticipant);
        builder.Property(x => x.Durations);
        builder.Property(x => x.EventCatalogId);
        
        builder.HasOne(x => x.EventCatalog)
            .WithMany()
            .HasForeignKey(x => x.EventCatalogId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

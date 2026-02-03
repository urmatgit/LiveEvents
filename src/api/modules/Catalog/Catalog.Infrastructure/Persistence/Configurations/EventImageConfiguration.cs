using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

public class EventImageConfiguration : IEntityTypeConfiguration<EventImage>
{
    public void Configure(EntityTypeBuilder<EventImage> builder)
    {
        builder.ToTable("EventImages");

        builder.HasKey(ei => ei.Id);

        builder.Property(ei => ei.ImageUrl)
            .IsRequired(true)
            .HasMaxLength(500);

        builder.HasOne(ei => ei.SomeEvent)
            .WithMany(se => se.EventImages)
            .HasForeignKey(ei => ei.SomeEventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

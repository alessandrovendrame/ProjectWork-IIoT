using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomTech.CloudManager.Domain.Entities;

namespace RoomTech.CloudManager.Infrastructure.Persistence.EntityConfiguration
{
    public class SubjectEntityConfiguration : IEntityTypeConfiguration<Subject>
    {
        internal const string table = "subject";

        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable(table);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();
            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();
            builder.Property(e => e.Description)
                .HasColumnName("description")
                .IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomTech.CloudManager.Domain.Entities;

namespace RoomTech.CloudManager.Infrastructure.Persistence.EntityConfiguration
{
    public class ClassroomEntityConfiguration : IEntityTypeConfiguration<Classroom>
    {
        internal const string table = "classroom";

        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.ToTable(table);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();
            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();
            builder.Property(e => e.Floor)
                .HasColumnName("floor")
                .IsRequired();
            builder.Property(e => e.Building)
                .HasColumnName("building")
                .IsRequired();
        }
    }
}

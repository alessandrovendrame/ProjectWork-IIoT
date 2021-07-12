using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomTech.CloudManager.Domain.Entities;

namespace RoomTech.CloudManager.Infrastructure.Persistence.EntityConfiguration
{
    public class TeacherEntityConfiguration : IEntityTypeConfiguration<Teacher>
    {
        internal const string table = "teacher";

        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable(table);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();
            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();
            builder.Property(e => e.Surname)
                .HasColumnName("surname")
                .IsRequired();
        }
    }
}

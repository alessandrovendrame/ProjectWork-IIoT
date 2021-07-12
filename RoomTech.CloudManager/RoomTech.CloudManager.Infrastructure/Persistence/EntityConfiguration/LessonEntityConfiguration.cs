using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomTech.CloudManager.Domain.Entities;

namespace RoomTech.CloudManager.Infrastructure.Persistence.EntityConfiguration
{
    public class LessonEntityConfiguration : IEntityTypeConfiguration<Lesson>
    {
        internal const string table = "lesson";

        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable(table);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();
            builder.Property(e => e.Teacher)
                .HasColumnName("teacher")
                .IsRequired();
            builder.Property(e => e.Subject)
                .HasColumnName("subject")
                .IsRequired();
            builder.Property(e => e.Classroom)
                .HasColumnName("classroom")
                .IsRequired();
            builder.Property(e => e.Floor)
                .HasColumnName("floor")
                .IsRequired();
            builder.Property(e => e.Date)
                .HasColumnName("date")
                .IsRequired();
            builder.Property(e => e.Duration)
                .HasColumnName("duration")
                .IsRequired();
            builder.Property(e => e.StartTime)
                .HasColumnName("startTime")
                .IsRequired();
            
        }
    }
}

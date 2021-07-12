using Microsoft.EntityFrameworkCore;
using RoomTech.CloudManager.Domain.Entities;
using RoomTech.CloudManager.Infrastructure.Persistence.EntityConfiguration;

namespace RoomTech.CloudManager.Infrastructure.Persistence.DatabaseContext
{
    public class RoomTechDbContext : DbContext
    {


        internal DbSet<Classroom> Classrooms { get; private set; }
       // internal DbSet<Entity> Entities { get; private set; }
        internal DbSet<Lesson> Lessons { get; private set; }
        internal DbSet<Subject> Subjects { get; private set; }
        internal DbSet<Teacher> Teachers { get; private set; }

        public RoomTechDbContext(DbContextOptions<RoomTechDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClassroomEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LessonEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherEntityConfiguration());
        }
    }
}

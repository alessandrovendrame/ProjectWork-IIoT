using Microsoft.EntityFrameworkCore;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.Domain.Entities;
using RoomTech.CloudManager.Infrastructure.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Infrastructure.Persistence.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly RoomTechDbContext _dbContext;

        public TeacherRepository(RoomTechDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _dbContext.Teachers.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _dbContext.Teachers.FindAsync(id).ConfigureAwait(false);
        }

        public async Task InsertNewTeacherAsync(Teacher teacher)
        {
            await _dbContext.Teachers.AddAsync(teacher).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }

        public List<string> GetUniqueTeachers(List<Teacher> teachers)
        {
            List<string> teachersName = new List<string>();
            foreach (var teacher in teachers)
            {
                teachersName.Add(teacher.Surname);
            }
            var uniqueTeachers = teachersName.Distinct().ToList();
            return uniqueTeachers;
        }
    }
}

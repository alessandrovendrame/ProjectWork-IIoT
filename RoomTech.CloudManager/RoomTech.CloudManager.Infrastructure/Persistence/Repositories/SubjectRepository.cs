using Microsoft.EntityFrameworkCore;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.Domain.Entities;
using RoomTech.CloudManager.Infrastructure.Persistence.DatabaseContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Infrastructure.Persistence.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly RoomTechDbContext _dbContext;
        public SubjectRepository(RoomTechDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<Subject>> GetAllAsync()
        {
            return await _dbContext.Subjects.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            return await _dbContext.Subjects.FindAsync(id).ConfigureAwait(false);
        }

        public async Task InsertNewSubjectAsync(Subject subject)
        {
            await _dbContext.Subjects.AddAsync(subject).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.Domain.Entities;
using RoomTech.CloudManager.Infrastructure.Persistence.DatabaseContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Infrastructure.Persistence.Repositories
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly RoomTechDbContext _dbContext;

        public ClassroomRepository(RoomTechDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Classroom>> GetAllAsync()
        {
            return await _dbContext.Classrooms.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Classroom> GetClassroomByIdAsync(int id)
        {
            return await _dbContext.Classrooms.FindAsync(id).ConfigureAwait(false);
        }

        public async Task InsertNewClassroomAsync(Classroom classRoom)
        {
            await _dbContext.Classrooms.AddAsync(classRoom).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }

        public List<string> GetUniqueFloors(List<Classroom> classrooms)
        {
            List<string> floors = new List<string>();
            foreach (var classroom in classrooms)
            {
                floors.Add(classroom.Floor);
            }
            var uniqueFloors = floors.Distinct().ToList();
            return uniqueFloors;
        }

        public List<string> GetUniqueNames(List<Classroom> classrooms)
        {
            List<string> floors = new List<string>();
            foreach (var classroom in classrooms)
            {
                floors.Add(classroom.Name);
            }
            var uniqueFloors = floors.Distinct().ToList();
            return uniqueFloors;
        }
    }
}

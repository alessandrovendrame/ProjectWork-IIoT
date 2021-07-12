using RoomTech.CloudManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories
{
    public interface IClassroomRepository
    {
        Task<List<Classroom>> GetAllAsync();
        Task<Classroom> GetClassroomByIdAsync(int id);
        Task InsertNewClassroomAsync(Classroom classroom);
        List<string> GetUniqueFloors(List<Classroom> classrooms);
        List<string> GetUniqueNames(List<Classroom> classrooms);
    }
}

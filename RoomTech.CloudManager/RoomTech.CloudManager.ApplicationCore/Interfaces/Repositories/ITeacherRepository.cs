using RoomTech.CloudManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllAsync();
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task InsertNewTeacherAsync(Teacher teacher);
        List<string> GetUniqueTeachers(List<Teacher> teachers);
    }
}
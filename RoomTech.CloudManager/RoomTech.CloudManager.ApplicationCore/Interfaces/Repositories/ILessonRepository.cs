using RoomTech.CloudManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        Task<List<Lesson>> GetAllAsync();
        Task<Lesson> GetLessonByIdAsync(int id);
        Task InsertNewLessonAsync(Lesson lesson);
        List<string> GetUniqueFloors(List<Classroom> classrooms);
        Task DeleteLesson(int id);
        Task EditLesson(int id, string newTeacher, string NewSubject, string NewClassroom, string NewFloor, DateTime NewDate, int NewDuration, TimeSpan NewStartTime);
        List<string> GetUniqueTeachers(List<Teacher> teachers);
        List<string> GetUniqueClassrooms(List<Lesson> lessons);
        Task<List<Lesson>> GetLessonByFloor(string deviceId);
    }
}

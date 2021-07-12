using Microsoft.EntityFrameworkCore;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.Domain.Entities;
using RoomTech.CloudManager.Infrastructure.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Infrastructure.Persistence.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly RoomTechDbContext _dbContext;

        public LessonRepository(RoomTechDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _dbContext.Lessons.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Lesson> GetLessonByIdAsync(int id)
        {
            return await _dbContext.Lessons.FindAsync(id).ConfigureAwait(false);
        }

        public async Task InsertNewLessonAsync(Lesson lesson)
        {
            await _dbContext.Lessons.AddAsync(lesson).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }
        public async Task DeleteLesson(int id)
        {
            var lesson = _dbContext.Lessons.FirstOrDefault(c => c.Id == id);
            _dbContext.Lessons.Remove(lesson);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }

        public async Task EditLesson(int id, string newTeacher, string NewSubject, string NewClassroom, string NewFloor, DateTime NewDate, int NewDuration, TimeSpan NewStartTime)
        {
            var lessonToModify = _dbContext.Lessons.FirstOrDefault(lesson => lesson.Id == id);
            lessonToModify.Teacher = newTeacher;
            lessonToModify.Subject = NewSubject;
            lessonToModify.Classroom = NewClassroom;
            lessonToModify.Floor = NewFloor;
            lessonToModify.Date = NewDate;
            lessonToModify.Duration = NewDuration;
            lessonToModify.StartTime = NewStartTime;
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

        public List<string> GetUniqueClassrooms(List<Lesson> lessons)
        {
            List<string> picNames = new List<string>();
            foreach (var lesson in lessons)
            {
                picNames.Add(lesson.Classroom);
            }
            var uniquePicNames = picNames.Distinct().ToList();
            return uniquePicNames;
        }

        public async Task<List<Lesson>> GetLessonByFloor(string deviceId)
        {
            List<Lesson> allLessons = await _dbContext.Lessons.ToListAsync().ConfigureAwait(false);
            List<Lesson> result = new List<Lesson>();
            foreach (var lesson in allLessons)
            {
                if (lesson.Floor == deviceId)
                {
                    result.Add(lesson);
                }
            }
            return result;
        }
    }
}

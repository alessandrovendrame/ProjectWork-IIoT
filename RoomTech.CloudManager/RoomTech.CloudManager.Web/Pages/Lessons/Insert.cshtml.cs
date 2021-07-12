using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Web.Pages.Lessons
{
    public class InsertModel : PageModel
    {
        public IClassroomRepository _classroomRepository;
        public ITeacherRepository _teacherRepository;
        public ILessonRepository _lessonRepository;
        public SelectList UniqueClassesName { get; set; }
        public SelectList UniqueFloorsNumber { get; set; }
        public SelectList UniqueTeachersSurname { get; set; }

        public InsertModel(IClassroomRepository classroomRepository, ITeacherRepository teacherRepository, ILessonRepository lessonRepository)
        {
            _classroomRepository = classroomRepository;
            _teacherRepository = teacherRepository;
            _lessonRepository = lessonRepository;
        }

        [BindProperty]
        public List<string> UniqueClasses { get; set; }

        [BindProperty]
        public List<string> UniqueTeachers { get; set; }

        [BindProperty]
        public List<string> UniqueFloors { get; set; }

        [BindProperty]
        public Lesson Lesson { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UniqueClasses = new List<string>();
            var allClassrooms = await _classroomRepository.GetAllAsync().ConfigureAwait(false);
            UniqueClasses = _classroomRepository.GetUniqueNames(allClassrooms);
            UniqueClassesName = new SelectList(UniqueClasses);

            UniqueFloors = new List<string>();
            var allFloors = await _classroomRepository.GetAllAsync().ConfigureAwait(false);
            UniqueFloors = _classroomRepository.GetUniqueFloors(allFloors);
            UniqueFloorsNumber = new SelectList(UniqueFloors);

            UniqueTeachers = new List<string>();
            var allTeachers = await _teacherRepository.GetAllAsync().ConfigureAwait(false);
            UniqueTeachers = _teacherRepository.GetUniqueTeachers(allTeachers);
            UniqueTeachersSurname = new SelectList(UniqueTeachers);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UniqueClasses = new List<string>();
            var allClassrooms = await _classroomRepository.GetAllAsync().ConfigureAwait(false);
            UniqueClasses = _classroomRepository.GetUniqueNames(allClassrooms);
            UniqueClassesName = new SelectList(UniqueClasses);

            UniqueFloors = new List<string>();
            var allFloors = await _classroomRepository.GetAllAsync().ConfigureAwait(false);
            UniqueFloors = _classroomRepository.GetUniqueFloors(allFloors);
            UniqueFloorsNumber = new SelectList(UniqueFloors);

            UniqueTeachers = new List<string>();
            var allTeachers = await _teacherRepository.GetAllAsync().ConfigureAwait(false);
            UniqueTeachers = _teacherRepository.GetUniqueTeachers(allTeachers);
            UniqueTeachersSurname = new SelectList(UniqueTeachers);

            var newLesson = new Lesson(Lesson.Teacher, Lesson.Subject, Lesson.Classroom, Lesson.Floor, Lesson.Date, Lesson.Duration, Lesson.StartTime);

            await _lessonRepository.InsertNewLessonAsync(newLesson).ConfigureAwait(false);
            return Page();
        }

    }
}

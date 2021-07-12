using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.Domain.Entities;
using RoomTech.CloudManager.Infrastructure.Persistence.DatabaseContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Web.Pages.Dashboard
{
    public class CalendarModel : PageModel
    {
        public IClassroomRepository _classroomRepository;
        public ITeacherRepository _teacherRepository;
        public ILessonRepository _lessonRepository;

        public CalendarModel(
            IClassroomRepository classroomRepository,
            ITeacherRepository teacherRepository,
            ILessonRepository lessonRepository)
        {
            _classroomRepository = classroomRepository;
            _teacherRepository = teacherRepository;
            _lessonRepository = lessonRepository;
        }

        [BindProperty]
        public Lesson Lesson { get; set; }

        public IActionResult OnGet()
        {
            Lesson = new Lesson();
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    await _lessonRepository.DeleteLesson(Lesson.Id);
        //    return Page();
        //}

        public async Task<IActionResult> OnGetDeleteLesson(int id)
        {
            await _lessonRepository.DeleteLesson(id).ConfigureAwait(false);
            ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnGetEditLesson(int id)
        {
            await _lessonRepository.EditLesson(Lesson.Id, Lesson.Teacher, Lesson.Subject, Lesson.Classroom, Lesson.Floor, Lesson.Date, Lesson.Duration, Lesson.StartTime).ConfigureAwait(false);
            return Page();
        }


    }
}

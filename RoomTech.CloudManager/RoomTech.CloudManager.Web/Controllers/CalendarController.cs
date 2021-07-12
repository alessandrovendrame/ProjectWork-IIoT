using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.Domain.Entities;

namespace RoomTech.CloudManager.Web.Controllers {

    [Route ("api/Calendar")]
    [ApiController]
    public class CalendarController : Controller {
        public IClassroomRepository _classroomRepository;
        public ITeacherRepository _teacherRepository;
        public ILessonRepository _lessonRepository;

        public CalendarController (IClassroomRepository classroomRepository, ITeacherRepository teacherRepository, ILessonRepository lessonRepository) {

            _classroomRepository = classroomRepository;
            _teacherRepository = teacherRepository;
            _lessonRepository = lessonRepository;
        }

        public IActionResult Index () {
            return View ();
        }


        [HttpGet("LoadLessons")]
        public async Task<ActionResult<IEnumerable<Lesson>>> LoadLessons () {

            return await _lessonRepository.GetAllAsync ().ConfigureAwait (false);
        }

        [HttpPost("EditLesson/{id}")]
        public async Task<ActionResult> EditLesson(Lesson lesson, int id)
        {
             lesson.Date = lesson.Date.AddHours(2);
             await _lessonRepository.EditLesson(id,lesson.Teacher,lesson.Subject,lesson.Classroom,lesson.Floor,lesson.Date,lesson.Duration,lesson.Date.TimeOfDay);
             return StatusCode(200);
        }
        [HttpPost("DeleteLesson/{id}")]
        public async Task<ActionResult> DeleteLesson(int id)
        {
            await _lessonRepository.DeleteLesson(id);
            return StatusCode(200);
        }
    }
}
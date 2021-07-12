using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.Domain.Entities;

namespace RoomTech.CloudManager.Web.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        public ILessonRepository _lessonRepository;
        public DashboardModel(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public List<Lesson> TodayLessons { get; set; }
        public List<Lesson> Lessons { get; set; }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var currentDate = DateTime.Now;
            var today = currentDate.ToShortDateString();
            Lessons = await _lessonRepository.GetAllAsync().ConfigureAwait(false);
            TodayLessons = new List<Lesson>();
            foreach (var lesson in Lessons)
            {
                if (today == lesson.Date.ToShortDateString())
                {
                    TodayLessons.Add(lesson);
                }
            }



            return Page();
        }
    }
}

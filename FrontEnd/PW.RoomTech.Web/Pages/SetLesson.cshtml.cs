using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PW.RoomTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PW.RoomTech.Web.Pages
{
    public class SetLessonModel : PageModel
    {
        private readonly ILogger<SetLessonModel> _logger;

        public SetLessonModel(ILogger<SetLessonModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public LessonModel Lesson { get; set; }
        public void OnGet()
        {
        }
    }
}

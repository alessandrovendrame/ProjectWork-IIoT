using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PW.RoomTech.Web.Pages
{
    public class AuleModel : PageModel
    {
        private readonly ILogger<AuleModel> _logger;

        public AuleModel(ILogger<AuleModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}

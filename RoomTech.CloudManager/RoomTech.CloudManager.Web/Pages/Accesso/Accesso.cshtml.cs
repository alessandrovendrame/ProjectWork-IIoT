using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoomTech.CloudManager.Web.Pages.Accesso
{
    public class AccessoModel : PageModel
    {
        [BindProperty]
        public bool aut { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("/Dashboard/Dashboard");
        }

    }
}

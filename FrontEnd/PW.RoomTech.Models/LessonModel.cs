using System;
using System.ComponentModel.DataAnnotations;

namespace PW.RoomTech.Models
{
    public class LessonModel
    {
        [Required]
        public string Classroom { get; set; }
        [Required]
        public string Teacher { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}

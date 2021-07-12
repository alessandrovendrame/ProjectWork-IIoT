using System;

namespace RoomTech.CloudManager.Domain.Entities
{
    public class Lesson : Entity
    {
        public string Teacher { get; set; }
        public string Subject { get; set; }
        public string Classroom { get; set; }
        public string Floor { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public TimeSpan StartTime { get; set; }

        public Lesson()
        {

        }

        public Lesson(string teacher, string subject, string classroom, string floor, DateTime date, int duration, TimeSpan startTime)
        {
            Teacher = teacher;
            Subject = subject;
            Classroom = classroom;
            Floor = floor;
            Date = date;
            Duration = duration;
            StartTime = startTime;
        }
    }
}

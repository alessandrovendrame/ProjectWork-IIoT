namespace RoomTech.CloudManager.Domain.Entities
{
    public class Teacher : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Teacher(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}


namespace RoomTech.CloudManager.Domain.Entities
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Subject(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}

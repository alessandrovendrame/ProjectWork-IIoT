namespace RoomTech.CloudManager.Domain.Entities
{
    public class Classroom : Entity
    {
        public string Name { get; set; }
        public string Floor { get; set; }
        public string Building{ get; set; }

        public Classroom(string name, string floor, string building)
        {
            Name = name;
            Floor = floor;
            Building = building;
        }
    }
}

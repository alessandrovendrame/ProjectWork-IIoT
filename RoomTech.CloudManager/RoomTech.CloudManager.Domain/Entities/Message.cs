namespace RoomTech.CloudManager.Domain.Entities
{
    public class Message
    {
        public string Caller { get; set; }
        public string Body { get; set; }

        public Message(string caller, string body)
        {
            Caller = caller;
            Body = body;
        }
    }
}

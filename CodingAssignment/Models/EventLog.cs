namespace CodingAssignment.Models
{
    class EventLog
    {
        public string Id { get; set; }
        public string State { get; set; }
        public long TimeStamp { get; set; }
        public string Type { get; set; }
        public string Host { get; set; }
    }
}

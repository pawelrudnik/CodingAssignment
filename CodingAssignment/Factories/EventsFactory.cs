using CodingAssignment.Interfaces;
using CodingAssignment.Models;

namespace CodingAssignment.Factories
{
    class EventsFactory : IEventsFactory
    {
        private const int MAX_EVENT_DURATION = 4;

        public Event CreateEvent(string id, long duration, string type, string host)
        {
            return new Event()
            {
                Id = id,
                Duration = duration,
                Alert = duration > MAX_EVENT_DURATION,
                Type = type,
                Host = host
            };
        }
    }
}

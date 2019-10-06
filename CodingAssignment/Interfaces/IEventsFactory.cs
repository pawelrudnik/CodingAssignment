using CodingAssignment.Models;

namespace CodingAssignment.Interfaces
{
    interface IEventsFactory
    {
        Event CreateEvent(string id, long duration, string type, string host);
    }
}

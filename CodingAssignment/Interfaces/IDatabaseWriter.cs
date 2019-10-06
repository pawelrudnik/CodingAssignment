using System.Collections.Generic;
using CodingAssignment.Models;

namespace CodingAssignment.Interfaces
{
    interface IDatabaseWriter
    {
        void SaveEvents(IEnumerable<Event> events);
    }
}
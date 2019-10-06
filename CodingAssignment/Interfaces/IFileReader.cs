using System.Collections.Generic;
using CodingAssignment.Models;

namespace CodingAssignment.Interfaces
{
    interface IFileReader
    {
        IEnumerable<Event> ReadEvents(string path);
    }
}

using CodingAssignment.Interfaces;
using CodingAssignment.Models;
using LiteDB;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CodingAssignment.Services
{
    class DatabaseWriter : IDatabaseWriter
    {
        private readonly ILogger logger;

        public DatabaseWriter(ILogger<DatabaseWriter> logger)
        {
            this.logger = logger;
        }

        public void SaveEvents(IEnumerable<Event> events)
        {
            using (var db = new LiteDatabase("events.db"))
            {
                try
                {
                    var col = db.GetCollection<Event>("events");
                    col.InsertBulk(events);
                    logger.LogInformation("Writing to database successful");
                }
                catch (LiteException ex)
                {
                    logger.LogError(ex, "Error writing to database");
                }
            }
        }
    }
}

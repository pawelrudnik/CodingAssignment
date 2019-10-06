using CodingAssignment.Interfaces;
using CodingAssignment.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodingAssignment.Services
{
    class FileReader : IFileReader
    {
        private readonly ILogger logger;
        private readonly IEventsFactory eventsFactory;

        public FileReader(ILogger<FileReader> logger, IEventsFactory eventsFactory)
        {
            this.logger = logger;
            this.eventsFactory = eventsFactory;
        }

        public IEnumerable<Event> ReadEvents(string path)
        {
            var logsDictionary = new Dictionary<string, long>();
            var events = new List<Event>();

            logger.LogInformation($"Opening file {path}");

            using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bufferedStream = new BufferedStream(fileStream))
            using (StreamReader streamReader = new StreamReader(bufferedStream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var eventLog = ParseJson(line);
                    if (eventLog != null)
                    {
                        if (logsDictionary.ContainsKey(eventLog.Id))
                        {
                            var duration = Math.Abs(eventLog.TimeStamp - logsDictionary[eventLog.Id]);
                            events.Add(eventsFactory.CreateEvent(eventLog.Id, duration, eventLog.Type, eventLog.Host));
                        }
                        else
                        {
                            logsDictionary.Add(eventLog.Id, eventLog.TimeStamp);
                        }
                    }
                }
            }

            logger.LogInformation($"Read {events.Count} events from file");
            return events;
        }

        private EventLog ParseJson(string json)
        {
            EventLog eventLog = null;

            try
            {
                eventLog = JsonConvert.DeserializeObject<EventLog>(json);
            } 
            catch (JsonReaderException ex)
            {
                logger.LogError(ex, $"Error parsing line {json}");
            }

            return eventLog;
        }
    }
}

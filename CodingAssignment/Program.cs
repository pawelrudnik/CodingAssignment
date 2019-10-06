using CodingAssignment.Factories;
using CodingAssignment.Interfaces;
using CodingAssignment.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace CodingAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
            {
                Console.Write("Please provide path to log file");
                return;
            }
            
            var filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.Write("Provided file path doesn't exist");
                return;
            }

            var serviceProvider = ConfigureServices();
            var fileReader = serviceProvider.GetRequiredService<IFileReader>();
            var databaseWriter = serviceProvider.GetRequiredService<IDatabaseWriter>();

            var events = fileReader.ReadEvents(filePath);
            databaseWriter.SaveEvents(events);
        }

        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddLogging(loggingBuilder => loggingBuilder.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddSingleton<IFileReader, FileReader>()
                .AddSingleton<IDatabaseWriter, DatabaseWriter>()
                .AddSingleton<IEventsFactory, EventsFactory>()
                .BuildServiceProvider();
        }
    }
}

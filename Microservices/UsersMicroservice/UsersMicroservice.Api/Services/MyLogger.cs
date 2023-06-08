using System;
using System.IO;
using UsersMicroservice.Domain.Abstractions;

namespace UsersMicroservice.Api.Services
{
    public class MyLogger : IMyLogger
    {
        public void LogInfo(string message)
        {
            using (var file = File.Open("logs.txt", FileMode.Append, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(message);
            }
            Console.WriteLine(message);
        }
        
    }
}

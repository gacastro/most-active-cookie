using System;
using System.IO;
using System.Linq;

namespace MostActiveCookie
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Hello, thank you for using the most active cookie program");
            Console.WriteLine("Let's just hope the cookie monster doesn't find out");

            var argumentParser = new ArgumentParser(args);

            if (argumentParser.ErrorMessages.Count == 0)
            {
                Console.WriteLine("Oooopss something went wrong");
                foreach (var errorMessage in argumentParser.ErrorMessages)
                {
                    Console.WriteLine(errorMessage);
                }
                return 1;
            }

            var lines = 
                File
                    .ReadAllLines(argumentParser.FilePath)
                    .Skip(1);

            var cookieJar = new CookieJar();
            foreach (var line in lines)
            {
                var lineElements = line.Split(',');
                cookieJar.AddCookie(lineElements[0], DateTimeOffset.Parse(lineElements[1]));
            }

            var mostActiveCookies = cookieJar.FindMostActiveFor(DateTimeOffset.Parse(argumentParser.ByDate));

            Console.WriteLine($"In the file {argumentParser.FilePath} we found these cookies as the most active:");
            foreach (var cookie in mostActiveCookies)
            {
                Console.WriteLine(cookie);
            }

            return 0;
        }
    }
}
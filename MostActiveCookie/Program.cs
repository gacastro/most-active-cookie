using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MostActiveCookie
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("*** Hello, thank you for using the most active cookie program ***");
            Console.WriteLine("*** Let's just hope the cookie monster doesn't find out ***");

            var argumentParser = GetArgumentParser(args);
            if (argumentParser == null)
                return 1;

            IList<string> mostActiveCookies;
            try
            {
                mostActiveCookies = GetMostActiveCookies(argumentParser);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Oops something went wrong:");
                Console.WriteLine($"=> {exception.Message}");
                return 1;
            }

            if (mostActiveCookies.Count == 0)
            {
                Console.WriteLine($"The file {argumentParser.FilePath} had no cookies for the given date");
                return 0;
            }
            
            Console.WriteLine($"In the file {argumentParser.FilePath} we found these cookies as the most active:");
            foreach (var cookie in mostActiveCookies)
                Console.WriteLine($"=> {cookie}");

            return 0;
        }

        private static ArgumentParser GetArgumentParser(string[] args)
        {
            var argumentParser = new ArgumentParser(args);

            if (argumentParser.ErrorMessages.Count == 0)
                return argumentParser;
            
            Console.WriteLine("Oops something went wrong:");
            foreach (var errorMessage in argumentParser.ErrorMessages)
            {
                Console.WriteLine($"=> {errorMessage}");
            }

            return null;
        }

        private static IList<string> GetMostActiveCookies(ArgumentParser argumentParser)
        {
            var lines = 
                File
                    .ReadAllLines(argumentParser.FilePath)
                    .Skip(1);

            var cookieJar = new CookieJar();
            foreach (var line in lines)
            {
                var lineElements = line.Split(',');
                cookieJar.AddCookie(DateTimeOffset.Parse(lineElements[1]), lineElements[0]);
            }

            return cookieJar.FindMostActiveFor(
                DateTimeOffset.Parse(argumentParser.ByDate));
        }
    }
}
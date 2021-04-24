using System;

namespace MostActiveCookie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, thank you for using the most active cookie program");
            Console.WriteLine("Let's just hope the cookie monster doesn't find out");

            foreach (var arg in args)
            {
                Console.WriteLine($"argument: ''''''{arg}'''''");
            }
            var argumentParser = new ArgumentParser(args);
        }
    }
}
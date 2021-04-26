using System.Collections.Generic;
using System.IO;

namespace MostActiveCookie
{
    public class ArgumentParser
    {
        public string FilePath { get; private set; }
        public string ByDate { get; private set; }
        public IList<string> ErrorMessages { get; }
        
        public ArgumentParser(IReadOnlyList<string> args)
        {
            ErrorMessages = new List<string>();

            if (args.Count == 0)
            {
                ErrorMessages.Add("No arguments have been passed in");
                return;
            }
            
            for (var index = 0; index < args.Count; index++)
            {
                switch (args[index])
                {
                    case "--f":
                    {
                        //index will be ready to jump into next pair of arguments
                        SetFilePathAndIndex(args, ref index);
                        break;
                    }
                    case "--d":
                    {
                        //index will be ready to jump into next pair of arguments
                        SetByDateAndIndex(args, ref index);
                        break;
                    }
                }
            }

            ValidateFilePath();
            ValidateByDate();
        }

        private void SetFilePathAndIndex(IReadOnlyList<string> args, ref int index)
        {
            var indexWithinRange = ++index < args.Count;
            if (indexWithinRange)
            {
                FilePath = args[index];
            }
        }
        
        private void SetByDateAndIndex(IReadOnlyList<string> args, ref int index)
        {
            var indexWithinRange = ++index < args.Count;
            if (indexWithinRange)
            {
                ByDate = args[index];
            }
        }

        private void ValidateFilePath()
        {
            if (FilePath == null)
            {
                ErrorMessages.Add("You need to specify a file path");
                return;
            }

            if (File.Exists(FilePath))
                return;

            FilePath = null;
            ErrorMessages.Add("You need to provide an existing file path");
        }

        private void ValidateByDate()
        {
            if (ByDate == null)
            {
                ErrorMessages.Add("You need to specify a date");
            }
        }
    }
}
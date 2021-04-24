using System;
using MostActiveCookie;
using Xunit;

namespace Tests
{
    public class ArgumentParserTests
    {
        [Fact]
        public void Returns_Error_When_No_Arguments_Are_Passed()
        {
            var args = Array.Empty<string>();
            var argumentParser = new ArgumentParser(args);
            
            Assert.Single(argumentParser.ErrorMessages);
            Assert.Equal(
                "No arguments have been passed in",
                argumentParser.ErrorMessages[0]
            );
            Assert.Null(argumentParser.FilePath);
            Assert.Null(argumentParser.ByDate);
        }
        
        [Fact]
        public void Returns_Error_When_Wrong_Arguments_Are_Passed()
        {
            var args = new []{"string", "-d", "13312"};
            var argumentParser = new ArgumentParser(args);
            
            Assert.Equal(2, argumentParser.ErrorMessages.Count);
            Assert.Equal(
                "You need to specify a file path",
                argumentParser.ErrorMessages[0]
            );
            Assert.Equal(
                "You need to specify a date",
                argumentParser.ErrorMessages[1]
            );
            Assert.Null(argumentParser.FilePath);
            Assert.Null(argumentParser.ByDate);
        }
        
        [Fact]
        public void Returns_Error_When_incomplete_Arguments_Are_Passed()
        {
            var args = new []{"--d", "13312","--f"};
            var argumentParser = new ArgumentParser(args);
            
            Assert.Single(argumentParser.ErrorMessages);
            Assert.Equal(
                "You need to specify a file path",
                argumentParser.ErrorMessages[0]
            );
            Assert.Null(argumentParser.FilePath);
            Assert.NotNull(argumentParser.ByDate);
        }
        
        [Fact]
        public void Returns_Error_When_No_File_Path_Is_Passed()
        {
            var args = new[]{"--d", "23-04-2021"};
            var argumentParser = new ArgumentParser(args);
            
            Assert.Null(argumentParser.FilePath);
            Assert.Single(argumentParser.ErrorMessages);
            Assert.Equal(
                "You need to specify a file path",
                argumentParser.ErrorMessages[0]
            );
        }
        
        [Theory]
        [InlineData("not/a/real/path")]
        [InlineData("just a string")]
        public void Returns_Error_When_File_Path_Doesnt_Exist(string filePath)
        {
            var args = new[]
            {
                "--f", filePath,
                "--d", "23-04-2021"
            };
            var argumentParser = new ArgumentParser(args);

            Assert.Null(argumentParser.FilePath);
            Assert.Single(argumentParser.ErrorMessages);
            Assert.Equal(
                "You need to provide an existing file path",
                argumentParser.ErrorMessages[0]
            );
        }
        
        [Fact]
        public void Returns_Error_When_No_Date_Is_Passed()
        {
            var args = new[]{"--f", "/Users/goncalocastro/Dev/coding-challenges/MostActiveCookie/Tests/ArgumentParserTests.cs"};
            var argumentParser = new ArgumentParser(args);
            
            Assert.Null(argumentParser.ByDate);
            Assert.Single(argumentParser.ErrorMessages);
            Assert.Equal(
                "You need to specify a date",
                argumentParser.ErrorMessages[0]
            );
        }

        [Theory]
        [InlineData("--f,../../../ArgumentParserTests.cs,--d,23-04-2021", "../../../ArgumentParserTests.cs", "23-04-2021")]
        [InlineData("--f,../../../ArgumentParserTests.cs,not supposed to be here,--d,23-04-2021", "../../../ArgumentParserTests.cs", "23-04-2021")]
        [InlineData("--d,23-04-2021,--f,../../../ArgumentParserTests.cs", "../../../ArgumentParserTests.cs", "23-04-2021")]
        public void Returns_Arguments_Correctly(string inputArgs, string pathToFile, string byDate)
        {
            var args = inputArgs.Split(',');
            var argumentParser = new ArgumentParser(args);
            
            Assert.Empty(argumentParser.ErrorMessages);
            Assert.Equal(pathToFile, argumentParser.FilePath);
            Assert.Equal(byDate, argumentParser.ByDate);
        }
    }
}
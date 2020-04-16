using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = GetInputPath();
            string[] lines = ReadFile(input);
            List<string> sorted = SortText(lines);

            string output = "";
            bool isCollectionOutput = IsCollectionOutput();
            if(isCollectionOutput)
            {
                output = OutputAsCollection(sorted);
            }

            input = GetWritePath();
            if(isCollectionOutput)
                WriteFile(input, output);
            else
                WriteFile(input, sorted);

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }

        private static List<string> SortText(string[] lines)
        {
            Console.WriteLine("Sorting. . .");
            List<string> sorted = lines.ToList();
            sorted.Sort();
            Console.WriteLine("Sorted in alphabetical order!");
            Console.WriteLine();
            return sorted;
        }
        private static string GetInputPath()
        {
            Console.WriteLine("Type the input to your file:");
            string input;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                if (File.Exists(input) && Path.GetExtension(input) == ".txt")
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That file type is not supported! Only text files (*.txt) files are supported!");
            }
            Console.ResetColor();
            Console.WriteLine();
            return input;
        }
        private static string[] ReadFile(string input)
        {
            Console.WriteLine("Reading File. . .");
            string[] lines = null;
            try
            {
                lines = GetLines(input).Result;
            }
            catch (Exception e)
            {
                LogError(e);
            }
            Console.WriteLine("Successfully read file!");
            Console.WriteLine();
            return lines;
        }
        private static bool IsCollectionOutput()
        {
            Console.WriteLine("Do you want to output a string collection? (y/n)");
            string input;
            bool value;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                if (input.ToLower() == "y")
                {
                    value = true;
                    break;
                }
                else if (input.ToLower() == "n")
                {
                    value = false;
                    break;
                }
            }
            Console.ResetColor();
            Console.WriteLine();
            return value;
        }
        private static string OutputAsCollection(List<string> sorted)
        {
            string output;
            StringBuilder builder = new StringBuilder();
            foreach (string line in sorted)
            {
                if (sorted.IndexOf(line) != sorted.Count - 1)
                    builder.AppendFormat($"\"{ line }\", ");
                else
                    builder.AppendFormat($"\"{ line }\"");
            }
            output = builder.ToString();
            return output;
        }
        private static string GetWritePath()
        {
            string input;
            Console.WriteLine("Write down the path you want to save the sorted text to:");
            input = "";
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                if (Path.IsPathFullyQualified(input))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The input does not exist!");
            }
            Console.ResetColor();
            Console.WriteLine();
            return input;
        }
        private static void WriteFile(string path, List<string> sorted)
        {
            Console.WriteLine("Writing to file. . .");
            try
            {
                WriteLines(path, sorted);
            }
            catch (Exception e)
            {
                LogError(e);
            }
            Console.WriteLine("File successfully writen!");
            Console.WriteLine();
        }
        private static void WriteFile(string path, string sorted)
        {
            Console.WriteLine("Writing to file. . .");
            try
            {
                WriteText(path, sorted);
            }
            catch (Exception e)
            {
                LogError(e);
            }
            Console.WriteLine("File successfully writen!");
            Console.WriteLine();
        }

        private static void LogError(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: Something went wrong\n{ e.Message }");
            Console.ResetColor();
        }
        private static async Task<string[]> GetLines(string input)
        {
            return await File.ReadAllLinesAsync(input);
        }
        private static async void WriteLines(string input, IEnumerable<string> contents)
        {
            await File.WriteAllLinesAsync(input, contents);
        }
        private static async void WriteText(string input, string contents)
        {
            await File.WriteAllTextAsync(input, contents);
        }
    }
}

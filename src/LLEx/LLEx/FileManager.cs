using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLEx
{
    internal class FileManager
    {
        private readonly String input;

        public FileManager(string[] args)
        {
            String? input;

            ParseArguments(args, out input);

            if (input == null)
            {
                throw new ArgumentNullException("-t");
            }

            String fileExtension = Path.GetExtension(input).ToLower();

            if (fileExtension.CompareTo(".cgn") != 0)
            {
                throw new ArgumentException("Invalid file extension. Please provide a .cgn file!");
            }

            this.input = input;
        }

        public static void ParseArguments(string[] args, out String? input)
        {
            var arguments = new Dictionary<String, String>();

            for (int i = 0; i < args.Length; i += 2)
            {
                arguments[args[i]] = args[i + 1];
            }

            arguments.TryGetValue("-t", out input);
        }

        public String GetTextFromInputFile()
        {
            return File.ReadAllText(input);
        }

        public void SaveOutput(string text)
        {
            StreamWriter sw = File.CreateText("Output.llex");
            sw.Write(text);
            sw.Dispose();
            sw.Close();
        }

    }
}

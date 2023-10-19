using System.IO;

namespace LLEx
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileManager fileManager = new FileManager(args);

            String text = fileManager.GetTextFromInputFile();

            String output;

            new Tokenizer(text, out output);

            fileManager.SaveOutput(output);

            
        }
    }
}

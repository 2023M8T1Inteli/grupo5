using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace LLEx
{
    public class Program
    {
        private const int BUFFER_SIZE = 8;

        public static void Main(String[] args)
        {
            String? path;

            var arguments = new Dictionary<String, String>();

            for (int i = 0; i < args.Length; i += 2)
            {
                arguments[args[i]] = args[i + 1];
            }

            arguments.TryGetValue("-t", out path);

            if (path == null)
            {
                throw new ArgumentNullException("-t");
            }

            String fileExtension = Path.GetExtension(path).ToLower();

            if (fileExtension.CompareTo(".cgn") != 0)
            {
                throw new ArgumentException("Invalid file extension. Please provide a .cgn file!");
            }

            Source source = new Source(path, BUFFER_SIZE);

            StringBuilder output;

            new Tokenizer(source, out output);

            StreamWriter sw = File.CreateText("Output.llex");
            sw.Write(output);
            sw.Dispose();
            sw.Close();
        }
    }
}

using System.IO;
using LLEx.Tokens;

namespace LLEx
{
    public class Tokenizer
    {
        public Tokenizer(string text, out string output)
        {
            output = new String("");
            output += "<Tokens>\n";
            output += $"\t{new Identifier("a").ToString()}\n";
            output += "</Tokens>";
        }

    }
}

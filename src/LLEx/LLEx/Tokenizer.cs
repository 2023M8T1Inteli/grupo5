using System;
using System.IO;
using System.Text;
using LLEx.Tokens;

namespace LLEx
{
    public class Tokenizer
    {
        private Source src;
        private StringBuilder lexema;

        public Tokenizer(Source src, out StringBuilder output)
        {
            this.src = src;
            this.lexema = new StringBuilder();

            // Initializing the output as a string.
            output = new StringBuilder();

            // Starting with tag <tokens>.
            output.AppendLine("<tokens>");

            Token? token;

            while((token = ReadToken()) != null)
            {
                output.AppendLine($"<{token.Name}>{token.Value}</{token.Name}>");
            }

            // Ending with tag </tokens>.
            output.AppendLine("</tokens>");

            Console.Write(output.ToString());
        }

        private void ResetLexema()
        {
            this.lexema = new StringBuilder();
        }

        private Token? ReadToken()
        {
            // Resetting the lexema.
            this.ResetLexema();

            /* Reading the next token */
            
            // Reading the next char.
            char c = this.src.Peek();

            // Checking if is end of file.
            if (IsEndOfFile(c)) {
                Console.WriteLine("------------ EOF ------------");
                return null;
            }

            this.lexema.Append(c);

            if (IsCharAlphabetic(c))
            {
                // Retornar o token depois de verificar se ele é uma palavra reservada ou um identificador
            } else if (IsCharSpaceOrReturnOrNewLine(c))
            {
                // Fazer a lógica para continuar a leitura e ignorar caracteres em branco.
            }

            throw new Exception("Unhandled characther.");
        }

        private bool IsEndOfFile(char c)
        {
            return c.CompareTo('\0') == 0;
        }

        private bool IsCharAlphabetic(char c) {
            return IsCharAlphabeticUpperCase(c) || IsCharAlphabeticLowerCase(c);
        }

        private bool IsCharAlphabeticUpperCase(char c)
        {
            return (int)c > 64 && (int)c < 91;
        }

        private bool IsCharAlphabeticLowerCase(char c)
        {
            return (int)c > 96 && (int)c < 123;
        }

        private bool IsCharSpaceOrReturnOrNewLine(char c)
        {
            return c.CompareTo(' ') == 0 || c.CompareTo('\r') == 0 || c.CompareTo('\n') == 0;
        }

    }
}

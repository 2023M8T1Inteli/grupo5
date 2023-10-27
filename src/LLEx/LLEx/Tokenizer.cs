﻿using System;
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
                output.AppendLine($"\t<{token.Name}>{token.Value}</{token.Name}>");
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
            this.ResetLexema();

            char c = this.src.Peek();

            if (IsEndOfFile(c)) {
                return null;
            }

            this.lexema.Append(c);

            if (IsCharAlphabeticOrUnderline(c)) // Ou é um identificador ou uma palavra reservada
            {
                bool IsCharAlphanumericOrUnderlineBool;

                do
                {
                    c = this.src.Peek();
                    
                    if((IsCharAlphanumericOrUnderlineBool = IsCharAlphanumericOrUnderline(c)))
                    {
                        this.lexema.Append(c);
                    } else
                    {
                        this.src.GoBack();
                    }
                } while (IsCharAlphanumericOrUnderlineBool);

                string lexemaStr = this.lexema.ToString();

                if (IsLexemaEqualsToPrograma(lexemaStr))
                {
                    return new PROGRAMA(lexemaStr);
                }
                else if (IsLexemaEqualsToSe(lexemaStr))
                {
                    return new SE(lexemaStr);
                }
                else if (IsLexemaEqualsToEntao(lexemaStr))
                {
                    return new ENTAO(lexemaStr);
                }
                else if (IsLexemaEqualsToSenao(lexemaStr))
                {
                    return new SENAO(lexemaStr);
                }
                else if (IsLexemaEqualsToEnquanto(lexemaStr))
                {
                    return new ENQUANTO(lexemaStr);
                }
                else if (IsLexemaEqualsToFaca(lexemaStr))
                {
                    return new FACA(lexemaStr);
                }
                else if (IsLexemaEqualsToNao(lexemaStr))
                {
                    return new NAO(lexemaStr);
                }
                else if (IsLexemaEqualsToInicio(lexemaStr))
                {
                    return new LBLOCK(lexemaStr);
                }
                else if (IsLexemaEqualsToFim(lexemaStr))
                {
                    return new RBLOCK(lexemaStr);
                }
                else if (IsLexemaEqualsToVerdade(lexemaStr))
                {
                    return new BOOLEAN(lexemaStr);
                }
                else if (IsLexemaEqualsToFalso(lexemaStr))
                {
                    return new BOOLEAN(lexemaStr);
                }
                else if (IsLexemaEqualsToLer(lexemaStr))
                {
                    return new COMANDO(lexemaStr);
                }
                else if (IsLexemaEqualsToLerVarios(lexemaStr))
                {
                    return new COMANDO(lexemaStr);
                }
                else if (IsLexemaEqualsToMostrar(lexemaStr))
                {
                    return new COMANDO(lexemaStr);
                }
                else if (IsLexemaEqualsToTocar(lexemaStr))
                {
                    return new COMANDO(lexemaStr);
                }
                else if (IsLexemaEqualsToMostrarTocar(lexemaStr))
                {
                    return new COMANDO(lexemaStr);
                }
                else if (IsLexemaEqualsToEsperar(lexemaStr))
                {
                    return new COMANDO(lexemaStr);
                }
                else if (IsLexemaEqualsToE(lexemaStr))
                {
                    return new OPMUL(lexemaStr);
                }
                else if (IsLexemaEqualsToOu(lexemaStr))
                {
                    return new OPSUM(lexemaStr);
                }
                else
                {
                    return new ID(lexemaStr);
                }

            }
            else if (IsCharSpaceOrReturnOrNewLineOrTab(c))
            {
                // Fazer a lógica para continuar a leitura e ignorar caracteres em branco.
                return new NullCharacter("");
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

        private bool IsCharUnderline(char c)
        {
            return c.CompareTo('_') == 0;
        }

        private bool IsCharAlphabeticOrUnderline(char c)
        {
            return IsCharAlphabetic(c) || IsCharUnderline(c);
        }

        private bool IsCharAlphanumericOrUnderline(char c)
        {
            return IsCharAlphabetic(c) || IsCharUnderline(c);
        }

        private bool IsCharSpaceOrReturnOrNewLineOrTab(char c)
        {
            return c.CompareTo(' ') == 0 || c.CompareTo('\r') == 0 || c.CompareTo('\n') == 0 || c.CompareTo('\t') == 0;
        }

        private bool IsLexemaEqualsToPrograma(string lexema)
        {
            return lexema.CompareTo("programa") == 0;
        }

        private bool IsLexemaEqualsToSe(string lexema)
        {
            return lexema.CompareTo("se") == 0;
        }

        private bool IsLexemaEqualsToEntao(string lexema)
        {
            return lexema.CompareTo("entao") == 0;
        }

        private bool IsLexemaEqualsToSenao(string lexema)
        {
            return lexema.CompareTo("senao") == 0;
        }

        private bool IsLexemaEqualsToEnquanto(string lexema)
        {
            return lexema.CompareTo("enquanto") == 0;
        }

        private bool IsLexemaEqualsToFaca(string lexema)
        {
            return lexema.CompareTo("faca") == 0;
        }

        private bool IsLexemaEqualsToNao(string lexema)
        {
            return lexema.CompareTo("nao") == 0;
        }

        private bool IsLexemaEqualsToInicio(string lexema)
        {
            return lexema.CompareTo("inicio") == 0;
        }

        private bool IsLexemaEqualsToFim(string lexema)
        {
            return lexema.CompareTo("fim") == 0;
        }

        private bool IsLexemaEqualsToVerdade(string lexema)
        {
            return lexema.CompareTo("verdade") == 0;
        }

        private bool IsLexemaEqualsToFalso(string lexema)
        {
            return lexema.CompareTo("falso") == 0;
        }
        private bool IsLexemaEqualsToLer(string lexema)
        {
            return lexema.CompareTo("ler") == 0;
        }

        private bool IsLexemaEqualsToLerVarios(string lexema)
        {
            return lexema.CompareTo("ler_varios") == 0;
        }

        private bool IsLexemaEqualsToMostrar(string lexema)
        {
            return lexema.CompareTo("mostrar") == 0;
        }

        private bool IsLexemaEqualsToTocar(string lexema)
        {
            return lexema.CompareTo("tocar") == 0;
        }
        private bool IsLexemaEqualsToMostrarTocar(string lexema)
        {
            return lexema.CompareTo("mostrar_tocar") == 0;
        }

        private bool IsLexemaEqualsToEsperar(string lexema)
        {
            return lexema.CompareTo("esperar") == 0;
        }

        private bool IsLexemaEqualsToE(string lexema)
        {
            return lexema.CompareTo("e") == 0;
        }

        private bool IsLexemaEqualsToOu(string lexema)
        {
            return lexema.CompareTo("ou") == 0;
        }

    }
}

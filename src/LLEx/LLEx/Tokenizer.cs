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
        private int line = 1;
        private bool isString = false;
        private bool isSimpleLineComment = false;
        private bool isMultipleLineComment = false;

        public Tokenizer(Source src, out StringBuilder output)
        {
            this.src = src;
            this.lexema = new StringBuilder();

            // Initializing the output as a string.
            output = new StringBuilder();

            // Starting with tag <tokens>.
            output.AppendLine("<tokens>");

            Token? token;

            while ((token = ReadToken()) != null)
            {
                output.AppendLine($"\t<{token.Name} line=\"{token.Line}\">{token.Value}</{token.Name}>");
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

            if (IsEndOfFile(c))
            {
                return null;
            }

            this.lexema.Append(c);

            if (IsCharSlash(c))
            {
                // One line comment
                c = this.src.Peek();
                if (IsCharSlash(c))
                {
                    isSimpleLineComment = true;
                }
                while (isSimpleLineComment)
                {
                    c = this.src.Peek();
                    if (IsCharNewLine(c))
                    {
                        isSimpleLineComment = false;
                        this.line++;
                        return ReadToken();
                    }
                }

                // Multiple lines comment
                if (IsCharTimes(c))
                {
                    isMultipleLineComment = true;

                    while (isMultipleLineComment)
                    {
                        c = this.src.Peek();
                        if (IsCharTimes(c))
                        {
                            c = this.src.Peek();
                            if (IsCharSlash(c))
                            {
                                isMultipleLineComment = false;
                                return ReadToken();
                            }
                        }
                        if (IsCharNewLine(c))
                        {
                            this.line++;
                        }
                    }
                }
            }
            else if (IsLexemaEqualsToAspas(c.ToString()))
            {
                this.isString = !isString;
                return new DQUOTE(c.ToString(), line);
            }
            else if (isString)
            {
                while (isString)
                {
                    c = this.src.Peek();

                    if (IsCharQuotationMark(c))
                    {
                        this.src.GoBack();
                        return new STRING(this.lexema.ToString(), line);
                    }
                    else
                    {
                        this.lexema.Append(c);
                    }
                }
            }
            if (IsCharAlphabeticOrUnderline(c)) // Ou é um identificador ou uma palavra reservada
            {
                bool IsCharAlphanumericOrUnderlineBool;

                do
                {
                    c = this.src.Peek();

                    if ((IsCharAlphanumericOrUnderlineBool = IsCharAlphanumericOrUnderline(c)))
                    {
                        this.lexema.Append(c);
                    }
                    else
                    {
                        this.src.GoBack();
                    }
                } while (IsCharAlphanumericOrUnderlineBool);

                string lexemaStr = this.lexema.ToString();

                if (IsLexemaEqualsToPrograma(lexemaStr))
                {
                    return new PROGRAMA(lexemaStr, line);
                }
                else if (IsLexemaEqualsToSe(lexemaStr))
                {
                    return new SE(lexemaStr, line);
                }
                else if (IsLexemaEqualsToEntao(lexemaStr))
                {
                    return new ENTAO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToSenao(lexemaStr))
                {
                    return new SENAO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToEnquanto(lexemaStr))
                {
                    return new ENQUANTO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToFaca(lexemaStr))
                {
                    return new FACA(lexemaStr, line);
                }
                else if (IsLexemaEqualsToNao(lexemaStr))
                {
                    return new NAO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToInicio(lexemaStr))
                {
                    return new LBLOCK(lexemaStr, line);
                }
                else if (IsLexemaEqualsToFim(lexemaStr))
                {
                    return new RBLOCK(lexemaStr, line);
                }
                else if (IsLexemaEqualsToVerdade(lexemaStr))
                {
                    return new BOOLEAN(lexemaStr, line);
                }
                else if (IsLexemaEqualsToFalso(lexemaStr))
                {
                    return new BOOLEAN(lexemaStr, line);
                }
                else if (IsLexemaEqualsToLer(lexemaStr))
                {
                    return new COMANDO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToLerVarios(lexemaStr))
                {
                    return new COMANDO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToMostrar(lexemaStr))
                {
                    return new COMANDO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToTocar(lexemaStr))
                {
                    return new COMANDO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToMostrarTocar(lexemaStr))
                {
                    return new COMANDO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToEsperar(lexemaStr))
                {
                    return new COMANDO(lexemaStr, line);
                }
                else if (IsLexemaEqualsToE(lexemaStr))
                {
                    return new OPMUL(lexemaStr, line);
                }
                else if (IsLexemaEqualsToOu(lexemaStr))
                {
                    return new OPSUM(lexemaStr, line);
                }
                else
                {
                    return new ID(lexemaStr, line);
                }

            }
            else if (IsCharNumeric(c))
            {
                c = this.src.Peek();
                while (IsCharNumeric(c))
                {
                    this.lexema.Append(c);
                    c = this.src.Peek();
                }
                this.src.GoBack();
                return new INTEGER(this.lexema.ToString(), line);
            }
            else if (IsCharRelationalOperator(c))
            {
                c = this.src.Peek();
                if (IsCharRelationalOperator(c))
                {
                    this.lexema.Append(c);
                    return new OPREL(this.lexema.ToString(), line);
                }
                else if (IsLexemaEqualsToAtribuicao(this.lexema.ToString()))
                {
                    this.src.GoBack();
                    return new ASSIGN(this.lexema.ToString(), line);
                }
                else
                {
                    this.src.GoBack();
                    return new OPREL(this.lexema.ToString(), line);
                }

            }
            else if (IsLexemaEqualsToSumOperator(c))
            {
                return new OPSUM(c.ToString(), line);
            }
            else if (IsCharMultiplicationOperator(c))
            {
                return new OPMUL(c.ToString(), line);
            }
            else if (IsLexemaEqualsToPotencia(c.ToString()))
            {
                return new OPPOW(c.ToString(), line);
            }
            else if (IsLexemaEqualsToDoisPontos(c.ToString()))
            {
                return new COLON(c.ToString(), line);
            }
            else if (IsLexemaEqualsToVirgula(c.ToString()))
            {
                return new COMMA(c.ToString(), line);
            }
            else if (IsLexemaEqualsToPontoFinal(c.ToString()))
            {
                return new DOT(c.ToString(), line);
            }
            else if (IsLexemaEqualsToAbreParenteses(c.ToString()))
            {
                return new LPAR(c.ToString(), line);
            }
            else if (IsLexemaEqualsToFechaParenteses(c.ToString()))
            {
                return new RPAR(c.ToString(), line);
            }
            else if (IsCharNewLine(c))
            {
                line++;
                return ReadToken();
            }
            else if (IsCharSpaceOrReturnOrTab(c))
            {
                return ReadToken();
            }

            throw new Exception("Unhandled characther.");
        }

        private bool IsEndOfFile(char c)
        {
            return c.CompareTo('\0') == 0;
        }

        private bool IsCharAlphabetic(char c)
        {
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

        private bool IsCharGreaterThan(char c)
        {
            return c.CompareTo('>') == 0;
        }

        private bool IsCharLessThan(char c)
        {
            return c.CompareTo('<') == 0;
        }

        private bool IsCharEquals(char c)
        {
            return c.CompareTo('=') == 0;
        }

        private bool IsCharAlphabeticOrUnderline(char c)
        {
            return IsCharAlphabetic(c) || IsCharUnderline(c);
        }

        private bool IsCharRelationalOperator(char c)
        {
            return IsCharGreaterThan(c) || IsCharLessThan(c) || IsCharEquals(c);
        }

        private bool IsCharAlphanumericOrUnderline(char c)
        {
            return IsCharAlphabetic(c) || IsCharUnderline(c) || IsCharNumeric(c);
        }

        private bool IsCharSpaceOrReturnOrTab(char c)
        {
            return c.CompareTo(' ') == 0 || c.CompareTo('\r') == 0 || c.CompareTo('\t') == 0;
        }

        private bool IsCharNewLine(char c)
        {
            return c.CompareTo('\n') == 0;
        }

        private bool IsCharSum(char c)
        {
            return c.CompareTo('+') == 0;
        }

        private bool IsCharSubtraction(char c)
        {
            return c.CompareTo('-') == 0;
        }

        private bool IsCharTimes(char c)
        {
            return c.CompareTo('*') == 0;
        }

        private bool IsCharSlash(char c)
        {
            return c.CompareTo('/') == 0;
        }

        private bool IsCharMod(char c)
        {
            return c.CompareTo('%') == 0;
        }

        private bool IsCharMultiplicationOperator(char c)
        {
            return IsCharTimes(c) || IsCharSlash(c) || IsCharMod(c);
        }

        private bool IsCharQuotationMark(char c)
        {
            return c.CompareTo('\"') == 0;
        }

        private bool IsCharNumeric(char c)
        {
            return (int)c > 47 && (int)c < 58;
        }

        private bool IsLexemaEqualsToSumOperator(char c)
        {
            return IsCharSum(c) || IsCharSubtraction(c);
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

        private bool IsLexemaEqualsToDoisPontos(string lexema)
        {
            return lexema.CompareTo(":") == 0;
        }

        private bool IsLexemaEqualsToVirgula(string lexema)
        {
            return lexema.CompareTo(",") == 0;
        }

        private bool IsLexemaEqualsToPontoFinal(string lexema)
        {
            return lexema.CompareTo(".") == 0;
        }

        private bool IsLexemaEqualsToAspas(string lexema)
        {
            return lexema.CompareTo("\"") == 0;
        }

        private bool IsLexemaEqualsToAtribuicao(string lexema)
        {
            return lexema.CompareTo("=") == 0;
        }

        private bool IsLexemaEqualsToAbreParenteses(string lexema)
        {
            return lexema.CompareTo("(") == 0;
        }

        private bool IsLexemaEqualsToFechaParenteses(string lexema)
        {
            return lexema.CompareTo(")") == 0;
        }

        private bool IsLexemaEqualsToPotencia(string lexema)
        {
            return lexema.CompareTo("^") == 0;
        }
    }
}
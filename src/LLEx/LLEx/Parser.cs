using System;
using System.Collections.Generic;
using System.Xml;


namespace LLEx
{
    public class Parser
    {
        private List<Token> tokens;
        private XmlDocument xmlDoc;
        private int currentTokenIndex;


        public class Token
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int Line { get; set; }

            public Token(string name, string value, int line)
            {
                Name = name;
                Value = value;
                Line = line;
            }
        }

        public Parser(string xmlFilePath)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            tokens = ParseTokens(xmlDoc.DocumentElement);
            currentTokenIndex = 0;
        }

        private List<Token> ParseTokens(XmlNode node)
        {
            List<Token> tokenList = new List<Token>();

            // Percorra os elementos XML e extraia os tokens
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    string tokenName = childNode.Name;
                    string tokenValue = childNode.InnerText;
                    int tokenLine = int.Parse(childNode.Attributes["line"].Value);
                    Token token = new Token(tokenName, tokenValue, tokenLine);
                    tokenList.Add(token);
                }
            }

            return tokenList;
        }

        public SyntaxTree Parse()
        {
            SyntaxNode program = ParseProgram();
            return new SyntaxTree(program);
        }

        private SyntaxNode ParseProgram()
        {
            Match("PROGRAMA");
            Match("DQUOTE");
            string programString = Match("STRING").Value;
            Match("DQUOTE");
            Match("COLON");
            SyntaxNode block = ParseBlock();
            Match("DOT");

            SyntaxNode programNode = new SyntaxNode("Program");
            programNode.AddChild(new SyntaxNode("PROGRAMA"));
            programNode.AddChild(new SyntaxNode("DQUOTE"));
            programNode.AddChild(new SyntaxNode("STRING", programString));
            programNode.AddChild(new SyntaxNode("DQUOTE"));
            programNode.AddChild(new SyntaxNode("COLON"));
            programNode.AddChild(block);
            programNode.AddChild(new SyntaxNode("DOT"));

            return programNode;
        }

        private SyntaxNode ParseBlock()
        {
            Match("LBLOCK");
            SyntaxNode statementList = ParseStatementList();
            Match("RBLOCK");

            SyntaxNode blockNode = new SyntaxNode("Block");
            blockNode.AddChild(new SyntaxNode("LBLOCK"));
            blockNode.AddChild(statementList);
            blockNode.AddChild(new SyntaxNode("RBLOCK"));

            return blockNode;
        }

        private SyntaxNode ParseStatementList()
        {
            SyntaxNode statementList = new SyntaxNode("StatementList");

            while (currentTokenIndex < tokens.Count)
            {
                if (IsCurrentTokenIn("ID", "SE", "ENQUANTO", "mostrar", "tocar", "esperar", "mostrar_tocar"))
                {
                    SyntaxNode statement = ParseStatement();
                    statementList.AddChild(statement);
                }
                else
                {
                    break;
                }
            }

            return statementList;
        }

        private SyntaxNode ParseStatement()
        {
            SyntaxNode statement = null;

            if (IsCurrentToken("ID"))
            {
                statement = ParseAssignStatement();
            }
            else if (IsCurrentToken("SE"))
            {
                statement = ParseIfStatement();
            }
            else if (IsCurrentToken("ENQUANTO"))
            {
                statement = ParseWhileStatement();
            }
            else if (IsCurrentToken("mostrar", "tocar", "esperar", "mostrar_tocar"))
            {
                statement = ParseCommandStatement();
            }

            return statement;
        }

        private SyntaxNode ParseAssignStatement()
        {
            SyntaxNode assignStatement = new SyntaxNode("AssignStatement");

            string id = Match("ID").Value;
            Match("ASSIGN");

            SyntaxNode expression = IsCurrentToken("ler", "ler_varios") ? ParseInputStatement() : ParseExpression();

            assignStatement.AddChild(new SyntaxNode("ID", id));
            assignStatement.AddChild(new SyntaxNode("ASSIGN"));
            assignStatement.AddChild(expression);

            return assignStatement;
        }

        private SyntaxNode ParseInputStatement()
        {
            SyntaxNode inputStatement = new SyntaxNode("InputStatement");

            if (IsCurrentToken("ler"))
            {
                Match("ler");
                Match("LPAR");
                Match("RPAR");
            }
            else if (IsCurrentToken("ler_varios"))
            {
                Match("ler_varios");
                Match("LPAR");
                SyntaxNode sumExpression1 = ParseSumExpression();
                Match("COMMA");
                SyntaxNode sumExpression2 = ParseSumExpression();
                Match("COMMA");
                SyntaxNode sumExpression3 = ParseSumExpression();
                Match("RPAR");
                inputStatement.AddChild(sumExpression1);
                inputStatement.AddChild(sumExpression2);
                inputStatement.AddChild(sumExpression3);
            }

            return inputStatement;
        }

        private SyntaxNode ParseIfStatement()
        {
            SyntaxNode ifStatement = new SyntaxNode("IfStatement");

            Match("SE");
            SyntaxNode expression = ParseExpression();
            Match("ENTAO");
            SyntaxNode trueBlock = ParseBlock();
            ifStatement.AddChild(expression);
            ifStatement.AddChild(trueBlock);

            if (IsCurrentToken("SENAO"))
            {
                Match("SENAO");
                SyntaxNode elseBlock = ParseBlock();
                ifStatement.AddChild(elseBlock);
            }

            return ifStatement;
        }

        private SyntaxNode ParseWhileStatement()
        {
            SyntaxNode whileStatement = new SyntaxNode("WhileStatement");

            Match("ENQUANTO");
            SyntaxNode expression = ParseExpression();
            Match("FACA");
            SyntaxNode block = ParseBlock();

            whileStatement.AddChild(expression);
            whileStatement.AddChild(block);

            return whileStatement;
        }

        private SyntaxNode ParseCommandStatement()
        {
            SyntaxNode commandStatement = new SyntaxNode("CommandStatement");

            if (IsCurrentToken("mostrar"))
            {
                Match("mostrar");
                Match("LPAR");
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");
                commandStatement.AddChild(sumExpression);
            }
            else if (IsCurrentToken("tocar"))
            {
                Match("tocar");
                Match("LPAR");
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");
                commandStatement.AddChild(sumExpression);
            }
            else if (IsCurrentToken("esperar"))
            {
                Match("esperar");
                Match("LPAR");
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");
                commandStatement.AddChild(sumExpression);
            }
            else if (IsCurrentToken("mostrar_tocar"))
            {
                Match("mostrar_tocar");
                Match("LPAR");
                SyntaxNode sumExpression1 = ParseSumExpression();
                Match("COMMA");
                SyntaxNode sumExpression2 = ParseSumExpression();
                Match("RPAR");
                commandStatement.AddChild(sumExpression1);
                commandStatement.AddChild(sumExpression2);
            }

            return commandStatement;
        }

        private SyntaxNode ParseExpression()
        {
            SyntaxNode expression = new SyntaxNode("Expression");

            SyntaxNode sumExpression1 = ParseSumExpression();

            if (IsCurrentToken("==", "<>", ">", "<", ">=", "<="))
            {
                string relop = Match("==", "<>", ">", "<", ">=", "<=").Value;
                SyntaxNode sumExpression2 = ParseSumExpression();

                expression.AddChild(sumExpression1);
                expression.AddChild(new SyntaxNode("RELOP", relop));
                expression.AddChild(sumExpression2);
            }
            else
            {
                expression.AddChild(sumExpression1);
            }

            return expression;
        }

        private SyntaxNode ParseSumExpression()
        {
            SyntaxNode sumExpression = new SyntaxNode("SumExpression");

            SyntaxNode multTerm1 = ParseMultTerm();
            SyntaxNode sumExpression2 = ParseSumExpression2();

            sumExpression.AddChild(multTerm1);
            sumExpression.AddChild(sumExpression2);

            return sumExpression;
        }

        private SyntaxNode ParseSumExpression2()
        {
            if (IsCurrentToken("+", "-", "ou"))
            {
                SyntaxNode sumExpression2 = new SyntaxNode("SumExpression2");

                string op = Match("+", "-", "ou").Value;
                SyntaxNode multTerm = ParseMultTerm();
                SyntaxNode nextSumExpression2 = ParseSumExpression2();

                sumExpression2.AddChild(new SyntaxNode("OP", op));
                sumExpression2.AddChild(multTerm);
                sumExpression2.AddChild(nextSumExpression2);

                return sumExpression2;
            }

            return new SyntaxNode("ε");
        }

        private SyntaxNode ParseMultTerm()
        {
            SyntaxNode multTerm = new SyntaxNode("MultTerm");

            SyntaxNode powerTerm = ParsePowerTerm();
            SyntaxNode multTerm2 = ParseMultTerm2();

            multTerm.AddChild(powerTerm);
            multTerm.AddChild(multTerm2);

            return multTerm;
        }

        private SyntaxNode ParseMultTerm2()
        {
            if (IsCurrentToken("*", "/", "%", "e"))
            {
                SyntaxNode multTerm2 = new SyntaxNode("MultTerm2");

                string op = Match("*", "/", "%", "e").Value;
                SyntaxNode powerTerm = ParsePowerTerm();
                SyntaxNode nextMultTerm2 = ParseMultTerm2();

                multTerm2.AddChild(new SyntaxNode("OP", op));
                multTerm2.AddChild(powerTerm);
                multTerm2.AddChild(nextMultTerm2);

                return multTerm2;
            }

            return new SyntaxNode("ε");
        }

        private SyntaxNode ParsePowerTerm()
        {
            SyntaxNode powerTerm = new SyntaxNode("PowerTerm");

            SyntaxNode factor = ParseFactor();

            if (IsCurrentToken("^"))
            {
                Match("^");
                SyntaxNode nextPowerTerm = ParsePowerTerm();

                powerTerm.AddChild(factor);
                powerTerm.AddChild(new SyntaxNode("OP", "^"));
                powerTerm.AddChild(nextPowerTerm);

                return powerTerm;
            }

            powerTerm.AddChild(factor);

            return powerTerm;
        }

        private SyntaxNode ParseFactor()
        {
            SyntaxNode factor = new SyntaxNode("Factor");

            if (IsCurrentToken("ID"))
            {
                factor.AddChild(new SyntaxNode("ID", Match("ID").Value));
            }
            else if (IsCurrentToken("INTEGER"))
            {
                factor.AddChild(new SyntaxNode("INTEGER", Match("INTEGER").Value));
            }
            else if (IsCurrentToken("verdade", "falso"))
            {
                factor.AddChild(new SyntaxNode("BOOLEAN", Match("verdade", "falso").Value));
            }
            else if (IsCurrentToken("+", "-"))
            {
                factor.AddChild(new SyntaxNode("OP", Match("+", "-").Value));
                factor.AddChild(ParseFactor());
            }
            else if (IsCurrentToken("NAO"))
            {
                factor.AddChild(new SyntaxNode("NAO"));
                factor.AddChild(ParseBoolean());
            }
            else if (IsCurrentToken("LPAR"))
            {
                Match("LPAR");
                SyntaxNode expression = ParseExpression();
                Match("RPAR");

                factor.AddChild(new SyntaxNode("LPAR"));
                factor.AddChild(expression);
                factor.AddChild(new SyntaxNode("RPAR"));
            }

            return factor;
        }

        private SyntaxNode ParseBoolean()
        {
            return new SyntaxNode("BOOLEAN", Match("verdade", "falso").Value);
        }

        private Token Match(params string[] expectedTokenTypes)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                if (Array.Exists(expectedTokenTypes, t => t == currentToken.Name))
                {
                    currentTokenIndex++;
                    return currentToken;
                }
                else
                {
                    throw new Exception($"Expected one of: {string.Join(", ", expectedTokenTypes)}, but found {currentToken.Name}");
                }
            }
            else
            {
                throw new Exception("Unexpected end of input.");
            }
        }

        private bool IsCurrentToken(params string[] expectedTokenTypes)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                return Array.Exists(expectedTokenTypes, t => t == currentToken.Name);
            }
            return false;
        }

        private bool IsCurrentTokenIn(params string[] possibleTokenTypes)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                return Array.Exists(possibleTokenTypes, t => t == currentToken.Name);
            }
            return false;
        }
    }


}

using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Xml;
using LLEx.Tokens;


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

        public SyntaxNode ParseProgram()
        {
            Match("PROGRAMA");
            Match("DQUOTE");
            string programString = Match("STRING").Value;
            Match("DQUOTE");
            Match("COLON");
            SyntaxNode block = ParseBlock();
            Match("DOT");


            SyntaxNode programNode = new SyntaxNode("program");
            programNode.AddAttributes("string", programString);
            programNode.AddAttributes("blockNode", block);

            return programNode;
        }

        private SyntaxNode ParseBlock()
        {
            SyntaxNode blockNode = new SyntaxNode("block");

            Match("LBLOCK");
            SyntaxNode statementList = ParseStatementList();
            Match("RBLOCK");

            blockNode.AddAttributes("statementListNode", statementList);

            return blockNode;
        }

        private SyntaxNode ParseStatementList()
        {
            SyntaxNode statementList = new SyntaxNode("statementList");
            
            while (tokens[currentTokenIndex].Name != "RBLOCK")
            {
                if (IsCurrentToken("ID", "SE", "ENQUANTO") || IsCurrentTokenValue("mostrar", "tocar", "esperar", "mostrar_tocar"))
                {
                    SyntaxNode statement = ParseStatement();
                    statementList.AddAttributes("statementNode", statement);
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
            else
            {
                statement = ParseCommandStatement();
            }

            return statement;
        }

        private SyntaxNode ParseAssignStatement()
        {
            SyntaxNode assignStatement = new SyntaxNode("assignStatement");

      
            string id = Match("ID").Value;
            Match("ASSIGN");

            bool isInput = IsCurrentTokenValue("ler", "ler_varios");
            SyntaxNode expression;
            if (isInput)
            {
                expression = ParseInputStatement();
            }
            else
            {
                expression = ParseExpression();
            }

            assignStatement.AddAttributes("idNode",new SyntaxNodeLeaf("ID", id));
            assignStatement.AddAttributes("assignNode", new SyntaxNodeLeaf("ASSIGN", "="));
            assignStatement.AddAttributes("expressionAssingNode", expression);

            return assignStatement;
        }


        private SyntaxNode ParseInputStatement()
        {
            SyntaxNode inputStatement = new SyntaxNode("InputStatement");
            bool isInput = IsCurrentTokenValue("ler");


            if (isInput)
            {
                MatchValue("ler");
                Match("LPAR");
                Match("RPAR");

                inputStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","ler"));
                
            }
            else if (IsCurrentTokenValue("ler_varios"))
            {
                List<SyntaxNode> sumExpressions = new List<SyntaxNode>();
                Match("ler_varios");
                Match("LPAR");
                while (tokens[currentTokenIndex].Name != "RPAR"){
                    Boolean fisrtExpression = true;
                    if(fisrtExpression){
                        sumExpressions.Add(ParseSumExpression());
                        fisrtExpression = false;
                    }
                    Match("COMMA");
                    sumExpressions.Add(ParseSumExpression());
                    
                }
                

                inputStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","ler_varios"));
                for (int i = 0; i < sumExpressions.Count; i++)
                {
                    SyntaxNode sumExpression = sumExpressions[i];
                    inputStatement.AddAttributes("sumExpressionNode", sumExpression);
                }
            }

            return inputStatement;
        }

        private SyntaxNode ParseIfStatement()
        {
            SyntaxNode ifStatement = new SyntaxNode("ifStatement");

            Match("SE");
            SyntaxNode expression = ParseExpression();
            Match("ENTAO");
            SyntaxNode parseBlock = ParseBlock();
            
            
            ifStatement.AddAttributes("nodeExpressionSeNode", expression);
            ifStatement.AddAttributes("parseBlockEntaoNode",parseBlock);

            if (IsCurrentToken("SENAO"))
            {
                Match("SENAO");
                SyntaxNode elseBlock = ParseBlock();
                ifStatement.AddAttributes("parseBlockSenaoNode",parseBlock);
                
            }

            return ifStatement;
        }

        private SyntaxNode ParseWhileStatement()
        {
            SyntaxNode whileStatement = new SyntaxNode("whileStatement");

            Match("ENQUANTO");
            SyntaxNode expression = ParseExpression();
            Match("FACA");
            SyntaxNode block = ParseBlock();

            
            whileStatement.AddAttributes("expressionEnqauntoNode", expression);
            whileStatement.AddAttributes("parseBlockFacaNode",block);

            return whileStatement;
        }

        //O código abaixo estará errado também, porque os nomes são valores e não nomes.
        private SyntaxNode ParseCommandStatement()
        {
            SyntaxNode commandStatement = new SyntaxNode("commandStatement");

            if (IsCurrentTokenValue("mostrar"))
            {
                MatchValue("mostrar");
                Match("LPAR");
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");
                commandStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","mostrar"));
                commandStatement.AddAttributes("sumExpressionNode",sumExpression);
            }
            else if (IsCurrentTokenValue("tocar"))
            {
                MatchValue("tocar");
                Match("LPAR");
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");
                commandStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","tocar"));
                commandStatement.AddAttributes("sumExpressionNode",sumExpression);
            }
            else if (IsCurrentToken("esperar"))
            {
                MatchValue("esperar");
                Match("LPAR");
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");
                commandStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","esperar"));
                commandStatement.AddAttributes("sumExpressionNode",sumExpression);
            }
            else if (IsCurrentToken("mostrar_tocar"))
            {
                List<SyntaxNode> sumExpressions = new List<SyntaxNode>();
                MatchValue("mostrar_tocar");
                Match("LPAR");
                while (tokens[currentTokenIndex].Name != "RPAR"){
                    Boolean fisrtExpression = true;
                    if(fisrtExpression){
                        sumExpressions.Add(ParseSumExpression());
                        fisrtExpression = false;
                    }
                    Match("COMMA");
                    sumExpressions.Add(ParseSumExpression());
                    
                }
                
                commandStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","tocar"));
                for (int i = 0; i < sumExpressions.Count; i++)
                {
                    SyntaxNode sumExpression = sumExpressions[i];
                    commandStatement.AddAttributes("sumExpressionNode", sumExpression);
                }
            }

            return commandStatement;
        }

        private SyntaxNode ParseExpression()
        {
            SyntaxNode expression = new SyntaxNode("expression");

            SyntaxNode sumExpression1 = ParseSumExpression();

            if (IsCurrentTokenValue("==", "<>", ">", "<", ">=", "<="))
            {
                string relop = MatchValue("==", "<>", ">", "<", ">=", "<=").Value;
                SyntaxNode sumExpression2 = ParseSumExpression();

                expression.AddAttributes("sumExpressionNode",sumExpression1);
                expression.AddAttributes("oprelNode",new SyntaxNodeLeaf("OPREL", relop));
                expression.AddAttributes("sumExpressionNode",sumExpression2);
            }
            else
            {
                expression.AddAttributes("sumExpressionNode",sumExpression1);
            }

            return expression;
        }

        private SyntaxNode ParseSumExpression()
        {
            SyntaxNode sumExpression = new SyntaxNode("sumExpression");

            SyntaxNode multTerm1 = ParseMultTerm();
            SyntaxNode sumExpression2 = ParseSumExpression2();

            sumExpression.AddAttributes("multTermNode",multTerm1);
            sumExpression.AddAttributes("sumExpressionNode", sumExpression2);

            return sumExpression;
        }

        private SyntaxNode ParseSumExpression2()
        {
            if (IsCurrentTokenValue("+", "-", "ou"))
            {
                SyntaxNode sumExpression2 = new SyntaxNode("SumExpression2");

                string op = Match("+", "-", "ou").Value;
                SyntaxNode multTerm = ParseMultTerm();
                SyntaxNode nextSumExpression2 = ParseSumExpression2();

                sumExpression2.AddAttributes("opsumNode",new SyntaxNodeLeaf("OPSUM", op));
                sumExpression2.AddAttributes("multTermNode", multTerm);
                sumExpression2.AddAttributes("sumExpression2Node", nextSumExpression2);

                return sumExpression2;
            }

            return new SyntaxNode("ε");
        }

        private SyntaxNode ParseMultTerm()
        {
            SyntaxNode multTerm = new SyntaxNode("multTerm");

            SyntaxNode powerTerm = ParsePowerTerm();
            SyntaxNode multTerm2 = ParseMultTerm2();

            multTerm.AddAttributes("powerTermNode",powerTerm);
            multTerm.AddAttributes("multTerm2Node",multTerm2);

            return multTerm;
        }

        private SyntaxNode ParseMultTerm2()
        {
            if (IsCurrentToken("*", "/", "%", "e"))
            {
                SyntaxNode multTerm2 = new SyntaxNode("multTerm2");

                string op = Match("*", "/", "%", "e").Value;
                SyntaxNode powerTerm = ParsePowerTerm();
                SyntaxNode nextMultTerm2 = ParseMultTerm2();

                multTerm2.AddAttributes("opmulNode",new SyntaxNodeLeaf("OPMUL", op));
                multTerm2.AddAttributes("powerTermNode",powerTerm);
                multTerm2.AddAttributes("nextMultTerm2Node",nextMultTerm2);

                return multTerm2;
            }

            return new SyntaxNode("ε");
        }

        private SyntaxNode ParsePowerTerm()
        {
            SyntaxNode powerTerm = new SyntaxNode("powerTerm");

            SyntaxNode factor = ParseFactor();

            if (IsCurrentToken("^"))
            {
                Match("^");
                SyntaxNode nextPowerTerm = ParsePowerTerm();

                powerTerm.AddAttributes("factorNode",factor);
                powerTerm.AddAttributes("oppowNode",new SyntaxNodeLeaf("OPPOW", "^"));
                powerTerm.AddAttributes("nextPowerTermNode",nextPowerTerm);

                return powerTerm;
            }

            powerTerm.AddAttributes("factorNode",factor);

            return powerTerm;
        }

        private SyntaxNode ParseFactor()
        {
            SyntaxNode factor = new SyntaxNode("factor");

            if (IsCurrentToken("ID"))
            {
                factor.AddAttributes("idNode",new SyntaxNodeLeaf("ID", Match("ID").Value));
            }
            else if (IsCurrentToken("INTEGER"))
            {
                factor.AddAttributes("integerNode",new SyntaxNodeLeaf("INTEGER", Match("INTEGER").Value));
            }
            else if (IsCurrentToken("verdade", "falso"))
            {
                factor.AddAttributes("booleanNode",new SyntaxNodeLeaf("BOOLEAN", Match("BOOLEAN").Value));
            }
            else if (IsCurrentToken("+", "-"))
            {
                factor.AddAttributes("opsumNode",new SyntaxNodeLeaf("OPSUM", Match("+", "-").Value));
                factor.AddAttributes("factorNode",ParseFactor());
            }
            else if (IsCurrentToken("NAO"))
            {
                factor.AddAttributes("booleanNaoNode", new SyntaxNodeLeaf("BOOLEAN", Match("verdade", "falso").Value));
            }
            else if (IsCurrentToken("LPAR"))
            {
                Match("LPAR");
                SyntaxNode expression = ParseExpression();
                Match("RPAR");

                factor.AddAttributes("expressionNode",expression);
            }

            return factor;
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
                    throw new Exception($"Expected one of: {string.Join(", ", expectedTokenTypes)}, but found {currentToken.Name} on line {currentToken.Line}");
                }
            }
            else
            {
                throw new Exception("Unexpected end of input.");
            }
        }

        private Token MatchValue(params string[] expectedTokenValues)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                if (Array.Exists(expectedTokenValues, value => value == currentToken.Value))
                {
                    currentTokenIndex++;
                    return currentToken;
                }
                else
                {
                    throw new Exception($"Expected one of: {string.Join(", ", expectedTokenValues)}, but found {currentToken.Value} on line {currentToken.Line}");
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

        private bool IsCurrentTokenValue(params string[] expectedTokenTypes)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                return Array.Exists(expectedTokenTypes, t => t == currentToken.Value);
            }
            return false;
        }

    }


}

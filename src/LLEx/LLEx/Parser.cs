using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Xml;
using LLEx.Tokens;


namespace LLEx
{
    public class Parser
    {
        // List to store parsed tokens
        private List<Token> tokens;

        // XmlDocument to load and parse XML file
        private XmlDocument xmlDoc;

        // Index to keep track of the current token being processed
        private int currentTokenIndex;

        // Token class to represent individual tokens
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

        // Constructor to initialize the parser with an XML file path
        public Parser(string xmlFilePath)
        {
            // Load the XML document from the specified file path
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            // Parse the tokens from the root element of the XML document
            tokens = ParseTokens(xmlDoc.DocumentElement);

            // Set the current token index to the beginning
            currentTokenIndex = 0;
        }

        // Method to parse tokens from an XML node
        private List<Token> ParseTokens(XmlNode node)
        {
            // Iterate through child nodes of the given XML node
            List<Token> tokenList = new List<Token>();

            // Check if the child node is an XML element
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    // Extract information from the XML element and create a token
                    string tokenName = childNode.Name;
                    string tokenValue = childNode.InnerText;
                    int tokenLine = int.Parse(childNode.Attributes["line"].Value);
                    Token token = new Token(tokenName, tokenValue, tokenLine);
                    tokenList.Add(token);
                }
            }
            // Add the token to the list
            return tokenList;
        }

        // Method to initiate parsing of the entire program
        public SyntaxNode ParseProgram()
        {
            // Create a syntax node for the entire program
            SyntaxNode programNode = new SyntaxNode("program");
            programNode.AddAttributes("line", tokens[currentTokenIndex].Line);

            // Initialize the parsing process for the "PROGRAMA" rule
            Match("PROGRAMA");
            Match("DQUOTE");
            string programString = Match("STRING").Value;
            Match("DQUOTE");
            Match("COLON");
            // Parse the block of statements
            SyntaxNode block = ParseBlock();
            // Ensure the program ends with a period
            Match("DOT");


            // Add attributes to the program node
            programNode.AddAttributes("string", programString);
            programNode.AddAttributes("blockNode", block);
            
            

            return programNode;
        }

        // Method to parse a block of statements
        private SyntaxNode ParseBlock()
        {
            // Create a syntax node for the block
            SyntaxNode blockNode = new SyntaxNode("block");
            blockNode.AddAttributes("line", tokens[currentTokenIndex].Line);

            // Ensure the block is enclosed within curly braces
            Match("LBLOCK");
            // Parse the list of statements within the block
            SyntaxNode statementList = ParseStatementList();
            // Ensure the block is closed with a curly brace
            Match("RBLOCK");

            // Add attributes to the block node
            blockNode.AddAttributes("statementListNode", statementList);

            return blockNode;
        }

        // Method to parse a list of statements
        private SyntaxNode ParseStatementList()
        {
            // Create a syntax node for the list of statements
            SyntaxNode statementList = new SyntaxNode("statementList");
            statementList.AddAttributes("line", tokens[currentTokenIndex].Line);
            
            // Continue parsing statements until the end of the block is reached
            while (tokens[currentTokenIndex].Name != "RBLOCK")
            {
                // Check the type and value of the current token to determine the type of statement
                if (IsCurrentToken("ID", "SE", "ENQUANTO") || IsCurrentTokenValue("mostrar", "tocar", "esperar", "mostrar_tocar"))
                {
                    // Parse the current statement
                    SyntaxNode statement = ParseStatement();
                    // Add the statement to the list of statements
                    statementList.AddAttributes("statementNode", statement);
                }
                else
                {
                    // Break the loop if the current token does not match any expected statements
                    break;
                }
            }

            return statementList;
        }

        // Method to parse an individual statement
        private SyntaxNode ParseStatement()
        {   
            // Initialize a variable to store the parsed statement
            SyntaxNode statement = null;

            // Determine the type of statement based on the current token
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

        // Method to parse an assignment statement
        private SyntaxNode ParseAssignStatement()
        {
            // Create a syntax node for the assignment statement
            SyntaxNode assignStatement = new SyntaxNode("assignStatement");

            // Parse the identifier (ID) on the left side of the assignment
            string id = Match("ID").Value;
            // Ensure there is an equal sign indicating the assignment
            Match("ASSIGN");

            // Determine if the assignment is an input statement or a regular expression
            bool isInput = IsCurrentTokenValue("ler", "ler_varios");
            SyntaxNode expression;

            // Add attributes to the assignment statement node
            assignStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
            assignStatement.AddAttributes("idLeftNode",new SyntaxNodeLeaf("ID", id));

            // Parse the appropriate expression based on the assignment type
            if (isInput)
            {
                expression = ParseInputStatement();
                assignStatement.AddAttributes("inputStatemenNode", expression);
            }
            else
            {
                expression = ParseExpression();
                assignStatement.AddAttributes("expressionAssingNode", expression);
            }

            

            return assignStatement;
        }

        // Method to parse an input statement
        private SyntaxNode ParseInputStatement()
        {   
            // Create a syntax node for the input statement
            SyntaxNode inputStatement = new SyntaxNode("InputStatement");
            bool isInput = IsCurrentTokenValue("ler");

            // Determine if it is a single input or multiple inputs
            if (isInput)
            {
                // Parse the single input statement
                MatchValue("ler");
                Match("LPAR");
                Match("RPAR");

                // Add attributes for the single input statement
                inputStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
                inputStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","ler"));
                
            }
            else if (IsCurrentTokenValue("ler_varios"))
            {
                // Parse the multiple input statement
                List<SyntaxNode> sumExpressions = new List<SyntaxNode>();
                Match("ler_varios");
                Match("LPAR");

                // Add attributes for the multiple input statement
                inputStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
                inputStatement.AddAttributes("sumExpressionNode", ParseSumExpression());
                Match("COMMA");
                inputStatement.AddAttributes("sumExpressionNode", ParseSumExpression());
                Match("COMMA");
                inputStatement.AddAttributes("sumExpressionNode", ParseSumExpression());
                inputStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","ler_varios"));

            }

            return inputStatement;
        }

        // Method to parse an if statement
        private SyntaxNode ParseIfStatement()
        {   
            // Create a syntax node for the if statement
            SyntaxNode ifStatement = new SyntaxNode("ifStatement");

            // Ensure the if statement starts with "SE"
            Match("SE");
            // Parse the expression inside the if statement
            SyntaxNode expression = ParseExpression();
            // Ensure the "ENTAO" (THEN) keyword is present
            Match("ENTAO");
            // Parse the block of statements inside the "ENTAO" block
            SyntaxNode parseBlock = ParseBlock();
            
            // Add attributes for the if statement
            ifStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
            ifStatement.AddAttributes("nodeExpressionSeNode", expression);
            ifStatement.AddAttributes("parseBlockEntaoNode",parseBlock);

            // Check if there is an "SENAO" (ELSE) block
            if (IsCurrentToken("SENAO"))
            {
                // Ensure the "SENAO" keyword is present
                Match("SENAO");
                // Parse the block of statements inside the "SENAO" block
                SyntaxNode elseBlock = ParseBlock();
                // Add attributes for the else block
                ifStatement.AddAttributes("parseBlockSenaoNode",parseBlock);
                
            }

            return ifStatement;
        }

        // Method to parse a while statement
        private SyntaxNode ParseWhileStatement()
        {
            // Create a syntax node for the while statement
            SyntaxNode whileStatement = new SyntaxNode("whileStatement");

            // Ensure the while statement starts with "ENQUANTO"
            Match("ENQUANTO");
            // Parse the expression inside the while statement
            SyntaxNode expression = ParseExpression();
            // Ensure the "FACA" (DO) keyword is present
            Match("FACA");
            whileStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
            // Parse the block of statements inside the "FACA" block
            SyntaxNode block = ParseBlock();

            // Add attributes for the while statement
            whileStatement.AddAttributes("expressionEnqauntoNode", expression);
            whileStatement.AddAttributes("parseBlockFacaNode",block);

            return whileStatement;
        }

        // Method to parse command statements (e.g., mostrar, tocar, esperar, mostrar_tocar)
        private SyntaxNode ParseCommandStatement()
        {
            // Create a syntax node for the command statement
            SyntaxNode commandStatement = new SyntaxNode("commandStatement");

            // Check the value of the current token to determine the type of command
            if (IsCurrentTokenValue("mostrar"))
            {
                // Parse the "mostrar" (show) command
                MatchValue("mostrar");
                Match("LPAR");
                commandStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");

                // Add attributes for the "mostrar" command
                commandStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","mostrar"));
                commandStatement.AddAttributes("sumExpressionNode",sumExpression);
            }
            else if (IsCurrentTokenValue("tocar"))
            {   
                // Parse the "tocar" (play) command
                MatchValue("tocar");
                Match("LPAR");
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");

                // Add attributes for the "tocar" command
                commandStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
                commandStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","tocar"));
                commandStatement.AddAttributes("sumExpressionNode",sumExpression);
            }
            else if (IsCurrentToken("esperar"))
            {
                // Parse the "esperar" (wait) command
                MatchValue("esperar");
                Match("LPAR");
                commandStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
                SyntaxNode sumExpression = ParseSumExpression();
                Match("RPAR");

                // Add attributes for the "esperar" command
                commandStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","esperar"));
                commandStatement.AddAttributes("sumExpressionNode",sumExpression);
            }
            
            else if (IsCurrentToken("mostrar_tocar"))
            {
                // Parse the "mostrar_tocar" (show_play) command with multiple expressions
                List<SyntaxNode> sumExpressions = new List<SyntaxNode>();
                MatchValue("mostrar_tocar");
                Match("LPAR");

                
                // Add attributes for the "mostrar_tocar" command
                commandStatement.AddAttributes("line", tokens[currentTokenIndex].Line);
                commandStatement.AddAttributes("sumExpressionNode", ParseSumExpression());
                Match("COMMA");
                commandStatement.AddAttributes("sumExpressionNode", ParseSumExpression());
                Match("COMMA");
                commandStatement.AddAttributes("sumExpressionNode", ParseSumExpression());
                commandStatement.AddAttributes("comandoNode",new SyntaxNodeLeaf("COMANDO","tocar"));
                
            }

            return commandStatement;
        }

        // Method to parse an expression
        private SyntaxNode ParseExpression()
        {
            // Create a syntax node for the expression
            SyntaxNode expression = new SyntaxNode("expression");
            expression.AddAttributes("line", tokens[currentTokenIndex].Line);

            // Parse the first sum expression
            SyntaxNode sumExpression1 = ParseSumExpression();
            

            // Check if there is a relational operator
            if (IsCurrentTokenValue("==", "<>", ">", "<", ">=", "<="))
            {
                // Parse the relational operator and the second sum expression
                string relop = MatchValue("==", "<>", ">", "<", ">=", "<=").Value;
                SyntaxNode sumExpression2 = ParseSumExpression();

                // Add attributes for the expression with a relational operator
                expression.AddAttributes("sumExpressionNode",sumExpression1);
                expression.AddAttributes("oprelNode",new SyntaxNodeLeaf("OPREL", relop));
                expression.AddAttributes("sumExpressionNode",sumExpression2);
            }
            else
            {
                // Add attributes for the expression without a relational operator
                expression.AddAttributes("line", tokens[currentTokenIndex].Line);
                expression.AddAttributes("sumExpressionNode",sumExpression1);
            }

            return expression;
        }

        // Method to parse a sum expression
        private SyntaxNode ParseSumExpression()
        {
            // Create a syntax node for the sum expression
            SyntaxNode sumExpression = new SyntaxNode("sumExpression");
            sumExpression.AddAttributes("line", tokens[currentTokenIndex].Line);
            // Parse the first multiplication term
            SyntaxNode multTerm1 = ParseMultTerm();
            // Parse additional sum expressions if present
            SyntaxNode sumExpression2 = ParseSumExpression2();

            // Add attributes for the sum expression
            sumExpression.AddAttributes("multTermNode",multTerm1);
            sumExpression.AddAttributes("sumExpressionNode", sumExpression2);

            return sumExpression;
        }

        // Parses the rest of the sum expression (handles addition, subtraction, and logical OR operations)
        private SyntaxNode ParseSumExpression2()
        {
    
            if (IsCurrentTokenValue("+", "-", "ou"))
            {
                SyntaxNode sumExpression2 = new SyntaxNode("SumExpression2");
                sumExpression2.AddAttributes("line", tokens[currentTokenIndex].Line);

                // Match the operator (+, -, ou)
                string op = Match("+", "-", "ou").Value;

                // Parse the multiplication term and the rest of the sum expression
                SyntaxNode multTerm = ParseMultTerm();
                SyntaxNode nextSumExpression2 = ParseSumExpression2();

                // Add the operator, multiplication term, and the rest of the sum expression as attributes to the node
                sumExpression2.AddAttributes("opsumNode",new SyntaxNodeLeaf("OPSUM", op));
                sumExpression2.AddAttributes("multTermNode", multTerm);
                sumExpression2.AddAttributes("sumExpression2Node", nextSumExpression2);

                return sumExpression2;
            }

            // Return an epsilon node if no operator is found
            return new SyntaxNode("ε");
        }

        // Parses a multiplication term (handles multiplication, division, modulus, and logical AND operations)
        private SyntaxNode ParseMultTerm()
        {

            // Parse the power term and the rest of the multiplication term
            SyntaxNode multTerm = new SyntaxNode("multTerm");
            multTerm.AddAttributes("line", tokens[currentTokenIndex].Line);

            SyntaxNode powerTerm = ParsePowerTerm();
            SyntaxNode multTerm2 = ParseMultTerm2();

            // Add the power term and the rest of the multiplication term as attributes to the node
            multTerm.AddAttributes("powerTermNode",powerTerm);
            multTerm.AddAttributes("multTerm2Node",multTerm2);

            return multTerm;
        }

        // Parses the rest of the multiplication term (handles multiplication, division, modulus, and logical AND operations)
        private SyntaxNode ParseMultTerm2()
        {   
            
            if (IsCurrentToken("*", "/", "%", "e"))
            {
                SyntaxNode multTerm2 = new SyntaxNode("multTerm2");
                multTerm2.AddAttributes("line", tokens[currentTokenIndex].Line);

                // Match the operator (*, /, %, e)
                string op = Match("*", "/", "%", "e").Value;
                // Parse the power term and the rest of the multiplication term
                SyntaxNode powerTerm = ParsePowerTerm();
                SyntaxNode nextMultTerm2 = ParseMultTerm2();

                // Add the operator, power term, and the rest of the multiplication term as attributes to the node
                multTerm2.AddAttributes("opmulNode",new SyntaxNodeLeaf("OPMUL", op));
                multTerm2.AddAttributes("powerTermNode",powerTerm);
                multTerm2.AddAttributes("nextMultTerm2Node",nextMultTerm2);

                return multTerm2;
            }

            // Return an epsilon node if no operator is found
            return new SyntaxNode("ε");
        }

        // Parses a power term (handles exponentiation)
        private SyntaxNode ParsePowerTerm()
        {
            SyntaxNode powerTerm = new SyntaxNode("powerTerm");
            powerTerm.AddAttributes("line", tokens[currentTokenIndex].Line);

            // Parse the factor
            SyntaxNode factor = ParseFactor();

            // Check if there is an exponentiation operator (^)
            if (IsCurrentToken("^"))
            {
                Match("^");
                // Parse the next power term
                SyntaxNode nextPowerTerm = ParsePowerTerm();

                // Add the factor, exponentiation operator, and the next power term as attributes to the node
                powerTerm.AddAttributes("factorNode",factor);
                powerTerm.AddAttributes("oppowNode",new SyntaxNodeLeaf("OPPOW", "^"));
                powerTerm.AddAttributes("nextPowerTermNode",nextPowerTerm);

                return powerTerm;
            }

            // If no exponentiation operator is found, add only the factor as an attribute to the node
            powerTerm.AddAttributes("line", tokens[currentTokenIndex].Line);
            powerTerm.AddAttributes("factorNode",factor);

            return powerTerm;
        }

        // Parses a factor (handles identifiers, integers, booleans, unary plus/minus, logical NOT, and parentheses)
        private SyntaxNode ParseFactor()
        {
            SyntaxNode factor = new SyntaxNode("factor");
            factor.AddAttributes("line", tokens[currentTokenIndex].Line);

            if (IsCurrentToken("ID"))
            {   
                // If the current token is an identifier (ID), add it as an attribute to the node
                factor.AddAttributes("idNode",new SyntaxNodeLeaf("ID", Match("ID").Value));
            }
            else if (IsCurrentToken("INTEGER"))
            {   
                // If the current token is an integer, add it as an attribute to the node
                factor.AddAttributes("integerNode",new SyntaxNodeLeaf("INTEGER", Match("INTEGER").Value));
            }
            else if (IsCurrentToken("verdade", "falso"))
            {
                // If the current token is a boolean (verdade or falso), add it as an attribute to the node
                factor.AddAttributes("booleanNode",new SyntaxNodeLeaf("BOOLEAN", Match("BOOLEAN").Value));
            }
            else if (IsCurrentToken("+", "-"))
            {   
                // If the current token is a unary plus or minus, add the operator and the next factor as attributes to the node
                factor.AddAttributes("opsumNode",new SyntaxNodeLeaf("OPSUM", Match("+", "-").Value));
                factor.AddAttributes("factorNode",ParseFactor());
            }
            else if (IsCurrentToken("NAO"))
            {   
                // If the current token is the logical NOT (NAO), add it as an attribute to the node
                factor.AddAttributes("booleanNaoNode", new SyntaxNodeLeaf("BOOLEAN", Match("verdade", "falso").Value));
            }
            else if (IsCurrentToken("LPAR"))
            {   
                // If the current token is an opening parenthesis, parse the expression within the parentheses
                Match("LPAR");
                SyntaxNode expression = ParseExpression();
                Match("RPAR");

                // Add the expression within parentheses as an attribute to the node  
                factor.AddAttributes("expressionNode",expression);
            }

            return factor;
        }

        // Matches the current token with the expected token types, and moves to the next token if a match is found
        private Token Match(params string[] expectedTokenTypes)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                if (Array.Exists(expectedTokenTypes, t => t == currentToken.Name))
                {   
                    // Move to the next token and return the current token
                    currentTokenIndex++;
                    return currentToken;
                }
                else
                {   
                    // Throw an exception if the current token does not match the expected types
                    throw new Exception($"Expected one of: {string.Join(", ", expectedTokenTypes)}, but found {currentToken.Name} on line {currentToken.Line}");
                }
            }
            else
            {   
                // Throw an exception if the end of input is reached unexpectedly
                throw new Exception("Unexpected end of input.");
            }
        }

        // Matches the current token with the expected token values, and moves to the next token if a match is found
        private Token MatchValue(params string[] expectedTokenValues)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                if (Array.Exists(expectedTokenValues, value => value == currentToken.Value))
                {   
                    // Move to the next token and return the current token
                    currentTokenIndex++;
                    return currentToken;
                }
                else
                {   
                    // Throw an exception if the current token does not match the expected values
                    throw new Exception($"Expected one of: {string.Join(", ", expectedTokenValues)}, but found {currentToken.Value} on line {currentToken.Line}");
                }
            }
            else
            {   
                // Throw an exception if the end of input is reached unexpectedly
                throw new Exception("Unexpected end of input.");
            }
        }


        // Checks if the current token matches any of the expected token types
        private bool IsCurrentToken(params string[] expectedTokenTypes)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                return Array.Exists(expectedTokenTypes, t => t == currentToken.Name);   
            }
            return false;
        }

        // Checks if the current token matches any of the expected token values
        private bool IsCurrentTokenValue(params string[] expectedTokenTypes)
        {
            if (currentTokenIndex < tokens.Count)
            {
                Token currentToken = tokens[currentTokenIndex];
                return Array.Exists(expectedTokenTypes, t => t == currentToken.Value);
            }
            return false;
        }
        // This method serializes a syntax tree (starting from 'rootNode') into an XML document, 
        // by recursively converting each node and its attributes into XML elements.
        public XmlDocument SerializeSyntaxTreeToXml(SyntaxNode rootNode)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement = rootNode.ToXmlElement(xmlDoc);
            xmlDoc.AppendChild(rootElement);
            return xmlDoc;
        }

    }


}

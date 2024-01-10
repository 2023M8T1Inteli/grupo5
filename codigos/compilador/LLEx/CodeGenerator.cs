using System;
using System.Text;
using System.Collections.Generic;

namespace LLEx
{
    // Class responsible for generating Python code from an abstract syntax tree (AST)
    public class CodeGenerator
    {
        private SyntaxNode ast;
        private StringBuilder code;
        private SemanticAnalyzer semanticAnalyzer;
        private string currentIndentation = "";

        // Mapping of tokens from the LL(1) grammar to their corresponding Python equivalents
        private Dictionary<string, string> tokenMap = new Dictionary<string, string>
        {
            {"<>", "!="},
            {"^", "**"},
            {"e", "and"},
            {"ou", "or"}
            // Other Operator mappings if needed
        };

        private int varNumSum;

        // Constructor for the CodeGenerator class
        public CodeGenerator(SyntaxNode ast)
        {
            this.ast = ast;
            this.code = new StringBuilder();
            this.semanticAnalyzer = new SemanticAnalyzer();
        }

        // Generate Python code from the provided abstract syntax tree
        public string GenerateCode()
        {
            semanticAnalyzer.AnalyzeSyntaxTree(ast);
            ProcessNode(ast);
            return code.ToString();
        }

        // Process different types of nodes in the abstract syntax tree
        private void ProcessNode(SyntaxNode node)
        {
            switch (node.Name)
            {
                case "program":
                    ProcessProgram(node);
                    break;
                case "block":
                    ProcessBlock(node);
                    break;
                case "statementList":
                    ProcessStatementList(node);
                    break;
            }
        }

        // Process a program node in the abstract syntax tree
        private void ProcessProgram(SyntaxNode node)
        {
            var idNode = (SyntaxNodeLeaf)node.GetAttribute("idNode");
            code.AppendLine($"def {idNode.Value}():");
            IncreaseIndentation();
            ProcessBlock((SyntaxNode)node.GetAttribute("blockNode"));
            DecreaseIndentation();
        }

        // Process a block node in the abstract syntax tree
        private void ProcessBlock(SyntaxNode node)
        {
            SyntaxNode statementListNode = (SyntaxNode)node.GetAttribute("statementListNode");
            if(statementListNode == null){
                ProcessStatementList(node);
            }
            else{
                // Process statement list if present
                SyntaxNode nextNode = (SyntaxNode)statementListNode.GetAttribute("nextNode");
                ProcessStatementList(statementListNode);
            }
        }

        // Process a statement list node in the abstract syntax tree
        private void ProcessStatementList(SyntaxNode node)
        {
            SyntaxNode statementNode = (SyntaxNode)node.GetAttribute("statementNode");
            SyntaxNode nextNode = null;
            if(node != null){
                nextNode = (SyntaxNode)node.GetAttribute("nextNode");
            }

            // Process individual statement
            ProcessStatement(statementNode);

            // Recursively process the next statement if present
            if (nextNode.CountAtributtes() != 0)
            {
                ProcessStatementList(nextNode);
            }
        }

        // Process an individual statement node in the abstract syntax tree
        private void ProcessStatement(SyntaxNode node)
        {
            switch (node.Name)
            {
                case "assignStatement":
                    ProcessAssignStatement(node);
                    break;
                case "ifStatement":
                    ProcessIfStatement(node);
                    break;
                case "whileStatement":
                    ProcessWhileStatement(node);
                    break;
                case "commandStatement":
                    ProcessCommandStatement(node);
                    break;
            }
        }

        // Process an assignment statement node in the abstract syntax tree
        private void ProcessAssignStatement(SyntaxNode node)
        {
            // Process the assignment statement and generate Python code
            string codeAssign = "";
            var idNode = (SyntaxNodeLeaf)node.GetAttribute("idNode");
            if (node.VerifyKey("inputStatementNode")){
                var expressionNode = node.GetAttribute("inputStatementNode") as SyntaxNode;
                codeAssign = ProcessInputStatement(expressionNode);
            }else{
                var expressionNode = node.GetAttribute("expressionNode") as SyntaxNode;
                codeAssign = ProcessExpression(expressionNode);
            }
    
            code.AppendLine($"{currentIndentation}{idNode.Value} = {codeAssign}");
        }

        // Process an if statement node in the abstract syntax tree
        private void ProcessIfStatement(SyntaxNode node)
        {
            // Process the if statement and generate Python code
            var expressionNode = node.GetAttribute("expressionNode") as SyntaxNode;
            var blockNode = node.GetAttribute("blockNode") as SyntaxNode;
            var elseBlockNode = node.GetAttribute("ifNotBlockNode") as SyntaxNode;
            string expressionCode = ProcessExpression(expressionNode);
            code.AppendLine($"{currentIndentation}if {expressionCode}:");
            IncreaseIndentation();
            ProcessBlock(blockNode);
            DecreaseIndentation();

            // Process the else block if present
            if (elseBlockNode != null)
            {
                code.AppendLine($"{currentIndentation}else:");
                IncreaseIndentation();
                ProcessBlock(elseBlockNode);
                DecreaseIndentation();
            }
        }

        // Process a while statement node in the abstract syntax tree
        private void ProcessWhileStatement(SyntaxNode node)
        {
            // Process the while statement and generate Python code
            var expressionNode = node.GetAttribute("expressionNode") as SyntaxNode;
            var blockNode = node.GetAttribute("blockNode") as SyntaxNode;
            string expressionCode = ProcessExpression(expressionNode);
            code.AppendLine($"{currentIndentation}while {expressionCode}:");
            IncreaseIndentation();
            ProcessBlock(blockNode);
            DecreaseIndentation();  
        }

        // Process a command statement node in the abstract syntax tree
        private void ProcessCommandStatement(SyntaxNode node)
        {   
            // Process a command statement and generate Python code
            string func = (string)node.GetAttribute("comandoNode");
            string objects =  "";

            // Process different command statements
            if(func == "mostrar_tocar"){

                SyntaxNode sumExpression = (SyntaxNode)node.GetAttribute($"sumExpressionNode1");
                objects += $"{ProcessExpression(sumExpression)}";

                for (int i = 0; i < node.CountAtributtes()-2; i++)
                {   
                    sumExpression = (SyntaxNode)node.GetAttribute($"sumExpressionNode{i+2}");
                    objects += $",{ProcessExpression(sumExpression)}";
                }
            }else{
                SyntaxNode sumExpression = (SyntaxNode)node.GetAttribute("sumExpressionNode");
                objects+=$"{ProcessExpression(sumExpression)}";
            }

            code.AppendLine($"{currentIndentation}{MapToken(func)}({objects})");
        }

        // Process an input statement node in the abstract syntax tree
        private string ProcessInputStatement(SyntaxNode node)
        {
            // Process an input statement and generate Python code
            string inputType = (string)node.GetAttribute("comandoNode");
            
            switch (inputType)
            {
                case "ler":
                    inputType +="()";
                    break;
                 case "ler_varios":
                    SyntaxNode sumExpression = (SyntaxNode)node.GetAttribute($"sumExpressionNode1");
                    inputType += $"({ProcessExpression(sumExpression)}";

                    for (int i = 0; i < node.CountAtributtes()-2; i++)
                    {   
                        sumExpression = (SyntaxNode)node.GetAttribute($"sumExpressionNode{i+2}");
                        inputType += $",{ProcessExpression(sumExpression)}";
                    }
                    inputType = $"{inputType})";
                    break;
            }
            return inputType;
        }

        // Process an expression node in the abstract syntax tree
        private string ProcessExpression(SyntaxNode node)
        {   string temp_var_sum;
            // Process an expression and generate Python code
            if (node.VerifyKey("idNode"))
            {
                SyntaxNodeLeaf value = (SyntaxNodeLeaf)node.GetAttribute("idNode");
                return value.Value;
            }else if (node.VerifyKey("integerNode"))
            {
                SyntaxNodeLeaf value = (SyntaxNodeLeaf)node.GetAttribute("integerNode");
                return value.Value;
            }
            else if (node.VerifyKey("booleanNode"))
            {
                SyntaxNodeLeaf value = (SyntaxNodeLeaf)node.GetAttribute("booleanNode");
                return value.Value;
            }
            else if (node.VerifyKey("expressionNode"))
            {
                SyntaxNode value = (SyntaxNode)node.GetAttribute("expressionNode");
                return $"{ProcessExpression(value)}";
            }
            else{
                string left = ProcessExpression(node.GetAttribute("left") as SyntaxNode);
                varNumSum +=1;

                if(node.VerifyKey("operator")){
                    temp_var_sum = $"_TEMP_VAR_EXP{varNumSum}";
                    string operatorType = node.GetAttribute("operator").ToString();
                    string right = ProcessExpression(node.GetAttribute("right") as SyntaxNode);
                    code.AppendLine($"{currentIndentation}{temp_var_sum} = {left} {MapToken(operatorType)} {right}");
                    return temp_var_sum;
                }else if(node.VerifyKey("comparator")){
                    temp_var_sum = $"_TEMP_VAR_COMP{varNumSum}";
                    string operatorType = node.GetAttribute("comparator").ToString();
                    string right = ProcessExpression(node.GetAttribute("right") as SyntaxNode);
                    code.AppendLine($"{currentIndentation}{temp_var_sum} = {left} {MapToken(operatorType)} {right}");
                    return temp_var_sum;
                }
                varNumSum = 0;
                return $"{left}";
            }
        }

        // Increase the current indentation level
        private void IncreaseIndentation()
        {
            currentIndentation += "    ";
        }

        // Decrease the current indentation level
        private void DecreaseIndentation()
        {
            if (currentIndentation.Length >= 4)
            {
                currentIndentation = currentIndentation.Substring(0, currentIndentation.Length - 4);
            }
        }

        // Process a sum expression node in the abstract syntax tree
        private string ProcessSumExpression(SyntaxNode node)
        {
            // Process a sum expression and generate Python code
            if (node == null)
            {
                return string.Empty;
            }

            string leftExpression = ProcessSumExpression(node.GetAttribute("esq") as SyntaxNode);
            string rightExpression = ProcessSumExpression(node.GetAttribute("dir") as SyntaxNode);

            switch (node.Name)
            {
                case "sumExpression":
                    return $"{leftExpression} + {rightExpression}";
                case "multiplicativeTerm":
                    if (node.GetAttribute("operator").ToString() == "MDC")
                    {
                        return $"Math.Gcd({leftExpression}, {rightExpression})";
                    }
                    return $"{leftExpression} * {rightExpression}";
                case "powerTerm":
                    return $"Math.Pow({leftExpression}, {rightExpression})";
                case "factor":
                    if (node is SyntaxNodeLeaf)
                    {   
                        SyntaxNodeLeaf nodeLeaf = (SyntaxNodeLeaf)node.GetAttribute("idNode");
                        if (node.Name == "num" || node.Name == "id")
                        {
                            return nodeLeaf.Value;
                        }
                        else if (node.Name == "log")
                        {
                            return nodeLeaf.Value == "true" ? "True" : "False";
                        }
                    }
                    else
                    {
                        string innerExpression = ProcessSumExpression(node.GetAttribute("expression") as SyntaxNode);
                        return $"-{innerExpression}";
                    }
                    break;
            }

            return string.Empty;
        }

        // Map LL(1) grammar tokens to their corresponding Python equivalents
        public string MapToken(string originalToken)
        {
            // Check if there is a mapping for the original token
            if (tokenMap.ContainsKey(originalToken))
            {
                // Return the corresponding Python token
                return tokenMap[originalToken];
            }
            // If there is no mapping, return the original token
            return originalToken;
        }
    }
}

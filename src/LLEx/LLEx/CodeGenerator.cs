using System;
using System.Text;
using System.Collections.Generic;

namespace LLEx
{
    public class CodeGenerator
    {
        private SyntaxNode ast;
        private StringBuilder code;
        private SemanticAnalyzer semanticAnalyzer;
        private string currentIndentation = "";

        public CodeGenerator(SyntaxNode ast)
        {
            this.ast = ast;
            this.code = new StringBuilder();
            this.semanticAnalyzer = new SemanticAnalyzer();
        }

        public string GenerateCode()
        {
            semanticAnalyzer.AnalyzeSyntaxTree(ast);
            ProcessNode(ast);
            return code.ToString();
        }

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

        private void ProcessProgram(SyntaxNode node)
        {
            var idNode = (SyntaxNodeLeaf)node.GetAttribute("idNode");
            code.AppendLine($"def {idNode.Value}():");
            IncreaseIndentation();
            ProcessBlock((SyntaxNode)node.GetAttribute("blockNode"));
            DecreaseIndentation();
        }

        private void ProcessBlock(SyntaxNode node)
        {

            
            SyntaxNode statementListNode = (SyntaxNode)node.GetAttribute("statementListNode");
            if(statementListNode == null){
                ProcessStatementList(node);
            }
            else{
                SyntaxNode nextNode = null;
                if(statementListNode != null){
                    nextNode = (SyntaxNode)statementListNode.GetAttribute("nextNode");
                }
                    
                ProcessStatementList(statementListNode);


                
            }
        }

        private void ProcessStatementList(SyntaxNode node)
        {   

            SyntaxNode statementNode = (SyntaxNode)node.GetAttribute("statementNode");
            SyntaxNode nextNode = null;
            if(node != null){
                nextNode = (SyntaxNode)node.GetAttribute("nextNode");
            }

            ProcessStatement(statementNode);

            if (nextNode.CountAtributtes() != 0)
            {
                ProcessStatementList(nextNode);
            }

        }

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

        private void ProcessAssignStatement(SyntaxNode node)
        {   
            
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

        private void ProcessIfStatement(SyntaxNode node)
        {
            var expressionNode = node.GetAttribute("expressionNode") as SyntaxNode;
            var blockNode = node.GetAttribute("blockNode") as SyntaxNode;
            var elseBlockNode = node.GetAttribute("ifNotBlockNode") as SyntaxNode;
            string expressionCode = ProcessExpression(expressionNode);
            code.AppendLine($"{currentIndentation}if ({expressionCode}) {{");
            IncreaseIndentation();
            ProcessBlock(blockNode);
            DecreaseIndentation();

            if (elseBlockNode != null)
            {
                code.AppendLine($"{currentIndentation}else {{");
                IncreaseIndentation();
                ProcessBlock(elseBlockNode);
                DecreaseIndentation();
            }
            code.AppendLine($"{currentIndentation}}}");
        }

        private void ProcessWhileStatement(SyntaxNode node)
        {
            var expressionNode = node.GetAttribute("expressionNode") as SyntaxNode;
            var blockNode = node.GetAttribute("blockNode") as SyntaxNode;
            string expressionCode = ProcessExpression(expressionNode);
            code.AppendLine($"{currentIndentation}while ({expressionCode}) {{");
            IncreaseIndentation();
            ProcessBlock(blockNode);
            DecreaseIndentation();
            code.AppendLine($"{currentIndentation}}}");
        }

        private void ProcessCommandStatement(SyntaxNode node)
        {   
            string func = (string)node.GetAttribute("comandoNode");
            string objects =  "";

            if(func == "mostrar_tocar"){
                SyntaxNode sumExpression1 = (SyntaxNode)node.GetAttribute("sumExpressionNode1");
                objects += $"{ProcessExpression(sumExpression1)},";
                SyntaxNode sumExpression2 = (SyntaxNode)node.GetAttribute("sumExpressionNode2");
                objects += $"{ProcessExpression(sumExpression2)},";
                SyntaxNode sumExpression3 = (SyntaxNode)node.GetAttribute("sumExpressionNode3");
                objects +=$"{ProcessExpression(sumExpression3)},)";
            }else{
                SyntaxNode sumExpression = (SyntaxNode)node.GetAttribute("sumExpressionNode");
                objects+=$"{ProcessExpression(sumExpression)})";
            }

            code.AppendLine($"{currentIndentation}{func}({objects}");
        }

        private string ProcessInputStatement(SyntaxNode node)
        {
            var idNode = (SyntaxNodeLeaf)node.GetAttribute("comandoNode");
            string inputType = idNode.Value;
            switch (inputType)
            {
                case "ler":
                    inputType = "int(input())";
                    break;
                 case "ler_varios":
                    inputType = "int(input())";
                    break;
            

           
            }
            return inputType;
        }

        private string ProcessExpression(SyntaxNode node)
        {   
            
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
                return ProcessExpression(value);
            }
            else{
                string left = ProcessExpression(node.GetAttribute("left") as SyntaxNode);
                if(node.VerifyKey("operator")){
                    string operatorType = node.GetAttribute("operator").ToString();
                    string right = ProcessExpression(node.GetAttribute("right") as SyntaxNode);
                    return $"{left} {operatorType} {right}";
                }else if(node.VerifyKey("comparator")){
                    string operatorType = node.GetAttribute("comparator").ToString();
                    string right = ProcessExpression(node.GetAttribute("right") as SyntaxNode);
                    return $"{left} {operatorType} {right}";
                }
                
                return $"{left}";
            }
        }

        private string ProcessExpressionList(SyntaxNode node)
        {
            List<string> expressionList = new List<string>();
            foreach (var attribute in node.GetAttributes())
            {
                if (attribute.Value is SyntaxNode childNode)
                {
                    expressionList.Add(ProcessExpression(childNode));
                }
            }
            return string.Join(", ", expressionList);
        }

        private void IncreaseIndentation()
        {
            currentIndentation += "    ";
        }

        private void DecreaseIndentation()
        {
            if (currentIndentation.Length >= 4)
            {
                currentIndentation = currentIndentation.Substring(0, currentIndentation.Length - 4);
            }
        }

        private string ProcessSumExpression(SyntaxNode node)
        {
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
    }
}

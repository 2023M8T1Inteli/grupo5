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
            var statementListNode = (SyntaxNode)node.GetAttribute("statementListNode");
            ProcessStatementList(statementListNode);
        }

        private void ProcessStatementList(SyntaxNode node)
        {
            foreach (var attribute in node.GetAttributes())
            {
                if (attribute.Value is SyntaxNode childNode)
                {
                    ProcessStatement(childNode);
                }
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
                case "inputStatement":
                    ProcessInputStatement(node);
                    break;
            }
        }

        private void ProcessAssignStatement(SyntaxNode node)
        {
            var idNode = (SyntaxNodeLeaf)node.GetAttribute("idNode");
            var expressionNode = node.GetAttribute("expressionNode") as SyntaxNode;
            string expressionCode = ProcessExpression(expressionNode);
            code.AppendLine($"{currentIndentation}{idNode.Value} = {expressionCode};");
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
            var commandNode = (SyntaxNodeLeaf)node.GetAttribute("commandNode");
            var expressionListNode = node.GetAttribute("expressionListNode") as SyntaxNode;
            string expressionListCode = ProcessExpressionList(expressionListNode);
            code.AppendLine($"{currentIndentation}{commandNode.Value}({expressionListCode});");
        }

        private void ProcessInputStatement(SyntaxNode node)
        {
            var idNode = (SyntaxNodeLeaf)node.GetAttribute("idNode");
            var inputType = node.GetAttribute("inputType").ToString();
            switch (inputType)
            {
                case "ler":
                    code.AppendLine($"{currentIndentation}{idNode.Value} = int(input());");
                    break;
                 case "ler_varios":
            var quadNode = node.GetAttribute("quadNode") as SyntaxNode;
            var qtdNode = node.GetAttribute("qtdNode") as SyntaxNode;
            var tolNode = node.GetAttribute("tolNode") as SyntaxNode;

            string quadCode = ProcessExpression(quadNode);
            string qtdCode = ProcessExpression(qtdNode);
            string tolCode = ProcessExpression(tolNode);

            code.AppendLine($"{currentIndentation}{quadCode} = input()");
            code.AppendLine($"{currentIndentation}{qtdCode} = input()");
            code.AppendLine($"{currentIndentation}{tolCode} = input()");
            break;
            }
        }

        private string ProcessExpression(SyntaxNode node)
        {
            if (node is SyntaxNodeLeaf leafNode)
            {
                return leafNode.Value;
            }
            else
            {
                string left = ProcessExpression(node.GetAttribute("left") as SyntaxNode);
                string operatorType = node.GetAttribute("operator").ToString();
                string right = ProcessExpression(node.GetAttribute("right") as SyntaxNode);
                return $"({left} {operatorType} {right})";
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
                    return $"({leftExpression} + {rightExpression})";
                case "multiplicativeTerm":
                    if (node.GetAttribute("operator").ToString() == "MDC")
                    {
                        return $"Math.Gcd({leftExpression}, {rightExpression})";
                    }
                    return $"({leftExpression} * {rightExpression})";
                case "powerTerm":
                    return $"Math.Pow({leftExpression}, {rightExpression})";
                case "factor":
                    if (node is SyntaxNodeLeaf leafNode)
                    {
                        if (leafNode.Name == "num" or "id")
                        {
                            return leafNode.Value;
                        }
                        else if (leafNode.Name == "log")
                        {
                            return leafNode.Value == "true" ? "True" : "False";
                        }
                    }
                    else
                    {
                        string innerExpression = ProcessSumExpression(node.GetAttribute("expression") as SyntaxNode);
                        return $"(-{innerExpression})";
                    }
                    break;
            }

            return string.Empty;
        }
    }
}

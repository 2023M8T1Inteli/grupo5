using System;
using System.Collections.Generic;

namespace LLEx
{
    public class SemanticAnalyzer
    {
        private Dictionary<string, VariableInfo> symbolTable;
        private Stack<ScopeInfo> scopeStack;
        private List<string> errors;

        public SemanticAnalyzer()
        {
            symbolTable = new Dictionary<string, VariableInfo>();
            scopeStack = new Stack<ScopeInfo>();
            errors = new List<string>();
        }

        public void AnalyzeSyntaxTree(SyntaxNode programNode)
        {
            EnterScope("global");
            AnalyzeProgram(programNode);
            ExitScope();
            PrintErrors();
        }

        private void AnalyzeProgram(SyntaxNode programNode)
        {
            SyntaxNode idNode = (SyntaxNode)programNode.GetAttribute("idNode");
            AddVariableToSymbolTable(idNode.Name, VariableType.Int);

            SyntaxNode blockNode = (SyntaxNode)programNode.GetAttribute("blockNode");
            AnalyzeBlock(blockNode);
        }

        private void AnalyzeBlock(SyntaxNode blockNode)
        {
            EnterScope("block");
            SyntaxNode statementListNode = (SyntaxNode)blockNode.GetAttribute("statementListNode");
            AnalyzeStatementList(statementListNode);
            ExitScope();
        }

        private void AnalyzeStatementList(SyntaxNode statementListNode)
        {
            SyntaxNode statementNode = (SyntaxNode)statementListNode.GetAttribute("statementNode");
            SyntaxNode nextNode = (SyntaxNode)statementListNode.GetAttribute("nextNode");

            AnalyzeStatement(statementNode);

            if (nextNode != null)
            {
                AnalyzeStatementList(nextNode);
            }
        }

        private void AnalyzeStatement(SyntaxNode statementNode)
        {
            SyntaxNode expressionNode = (SyntaxNode)statementNode.GetAttribute("expressionNode");

            if (expressionNode != null)
            {
                AnalyzeExpression(expressionNode);
            }
        }

        private void AnalyzeExpression(SyntaxNode expressionNode)
        {
            SyntaxNode leftNode = (SyntaxNode)expressionNode.GetAttribute("left");
            string comparator = (string)expressionNode.GetAttribute("comparator");
            SyntaxNode rightNode = (SyntaxNode)expressionNode.GetAttribute("right");

            AnalyzeSumExpression(leftNode);
            AnalyzeSumExpression(rightNode);

            CheckVariableInitialization(leftNode);
            CheckVariableInitialization(rightNode);

            // Adicione verificações de tipo aqui se necessário
            // Exemplo: Verificar se os tipos de leftNode e rightNode são compatíveis
        }

        private void AnalyzeSumExpression(SyntaxNode sumExpressionNode)
        {
            SyntaxNode leftNode = (SyntaxNode)sumExpressionNode.GetAttribute("left");
            string operatorType = (string)sumExpressionNode.GetAttribute("operator");
            SyntaxNode rightNode = (SyntaxNode)sumExpressionNode.GetAttribute("right");

            AnalyzeMultTerm(leftNode);
            AnalyzeMultTerm(rightNode);

            CheckVariableInitialization(leftNode);
            CheckVariableInitialization(rightNode);

            // Adicione verificações de tipo aqui se necessário
        }

        private void AnalyzeMultTerm(SyntaxNode multTermNode)
        {
            SyntaxNode leftNode = (SyntaxNode)multTermNode.GetAttribute("left");
            string operatorType = (string)multTermNode.GetAttribute("operator");
            SyntaxNode rightNode = (SyntaxNode)multTermNode.GetAttribute("right");

            AnalyzePowerTerm(leftNode);
            AnalyzePowerTerm(rightNode);

            CheckVariableInitialization(leftNode);
            CheckVariableInitialization(rightNode);

            // Adicione verificações de tipo aqui se necessário
        }

        private void AnalyzePowerTerm(SyntaxNode powerTermNode)
        {
            SyntaxNode leftNode = (SyntaxNode)powerTermNode.GetAttribute("left");
            string operatorType = (string)powerTermNode.GetAttribute("operator");
            SyntaxNode rightNode = (SyntaxNode)powerTermNode.GetAttribute("right");

            AnalyzeFactor(leftNode);
            AnalyzeFactor(rightNode);

            CheckVariableInitialization(leftNode);
            CheckVariableInitialization(rightNode);

            // Adicione verificações de tipo aqui se necessário
        }

        private void AnalyzeFactor(SyntaxNode factorNode)
        {
            SyntaxNode idNode = (SyntaxNode)factorNode.GetAttribute("idNode");
            SyntaxNode integerNode = (SyntaxNode)factorNode.GetAttribute("integerNode");
            SyntaxNode booleanNode = (SyntaxNode)factorNode.GetAttribute("booleanNode");
            string signal = (string)factorNode.GetAttribute("signal");

            if (idNode != null)
            {
                CheckVariableDeclaration(idNode);
                // Adicione verificações de tipo aqui se necessário
            }
            else if (integerNode != null)
            {
                // Adicione verificações de tipo aqui se necessário
            }
            else if (booleanNode != null)
            {
                // Adicione verificações de tipo aqui se necessário
            }
        }

        private void CheckVariableDeclaration(SyntaxNode idNode)
        {
            string variableName = ((SyntaxNodeLeaf)idNode).Name;

            if (!symbolTable.ContainsKey(variableName))
            {
                AddError($"Erro semântico: Variável '{variableName}' não foi declarada.");
            }
        }

        private void CheckVariableInitialization(SyntaxNode expressionNode)
        {
            if (expressionNode is SyntaxNodeLeaf idNode)
            {
                string variableName = idNode.Name;
                if (!IsVariableInitialized(variableName))
                {
                    AddError($"Erro semântico: Variável '{variableName}' não foi inicializada.");
                }
            }
        }

        private bool IsVariableInitialized(string variableName)
        {
            // Implemente a lógica para verificar se a variável foi inicializada antes de ser utilizada
            // Pode envolver o rastreamento de inicialização em diferentes caminhos do código
            // Por simplicidade, esta implementação assume que todas as variáveis são inicializadas
            return true;
        }

        private void EnterScope(string scopeName)
        {
            ScopeInfo scopeInfo = new ScopeInfo(scopeName);
            scopeStack.Push(scopeInfo);
        }

        private void ExitScope()
        {
            if (scopeStack.Count > 0)
            {
                scopeStack.Pop();
            }
        }

        private void AddVariableToSymbolTable(string variableName, VariableType variableType)
        {
            ScopeInfo currentScope = scopeStack.Peek();

            if (currentScope.SymbolTable.ContainsKey(variableName))
            {
                AddError($"Erro semântico: Variável '{variableName}' já foi declarada neste escopo.");
            }
            else
            {
                VariableInfo variableInfo = new VariableInfo(variableName, variableType);
                currentScope.SymbolTable.Add(variableName, variableInfo);
            }
        }

        private void AddError(string error)
        {
            errors.Add(error);
        }

        private void PrintErrors()
        {
            foreach (string error in errors)
            {
                Console.WriteLine(error);
            }
        }

        private class ScopeInfo
        {
            public string Name { get; }
            public Dictionary<string, VariableInfo> SymbolTable { get; }

            public ScopeInfo(string name)
            {
                Name = name;
                SymbolTable = new Dictionary<string, VariableInfo>();
            }
        }

        private class VariableInfo
        {
            public string Name { get; }
            public VariableType Type { get; }

            public VariableInfo(string name, VariableType type)
            {
                Name = name;
                Type = type;
            }
        }

        private enum VariableType
        {
            Int,
            Bool
        }
    }
}

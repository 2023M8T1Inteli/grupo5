using System;
using System.Collections.Generic;
using System.Reflection;

namespace LLEx
{   
    // Class responsible for performing semantic analysis on the abstract syntax tree (AST)
    public class SemanticAnalyzer
    {
        private Dictionary<string, VariableInfo> symbolTable; // Dictionary to store variable information
        private Stack<ScopeInfo> scopeStack; // Stack to manage different scopes
        private List<string> errors; // List to store semantic errors

        // Constructor for the SemanticAnalyzer class
        public SemanticAnalyzer()
        {
            symbolTable = new Dictionary<string, VariableInfo>();
            scopeStack = new Stack<ScopeInfo>();
            errors = new List<string>();
        }

        // Analyze the syntax tree for semantic errors
        public void AnalyzeSyntaxTree(SyntaxNode programNode)
        {
            EnterScope("global");
            AnalyzeProgram(programNode);
            ExitScope();
        }

        // Analyze a program node in the abstract syntax tree
        private void AnalyzeProgram(SyntaxNode programNode)
        {
            SyntaxNodeLeaf idNode = (SyntaxNodeLeaf)programNode.GetAttribute("idNode");
            AddVariableToSymbolTable(idNode.Value, "String", idNode.Name, idNode.Value, false);

            SyntaxNode blockNode = (SyntaxNode)programNode.GetAttribute("blockNode");
            AnalyzeBlock(blockNode);
            if (blockNode == null)
            {
                throw new Exception($"Semantic error: Empty block; its state will never be altered.");
            }
        }

        // Analyze a block node in the abstract syntax tree
        private void AnalyzeBlock(SyntaxNode blockNode)
        {   
            EnterScope("block");
            SyntaxNode statementListNode = (SyntaxNode)blockNode.GetAttribute("statementListNode");
            if(statementListNode == null){
                AnalyzeStatementList(blockNode);
            }
            else{
                SyntaxNode nextNode = null;
                if(statementListNode != null){
                    nextNode = (SyntaxNode)statementListNode.GetAttribute("nextNode");
                }
                    
                AnalyzeStatementList(statementListNode);

                ExitScope();
            }
        }

        // Analyze a statement list node in the abstract syntax tree
        private void AnalyzeStatementList(SyntaxNode statementListNode)
        {
            SyntaxNode statementNode = (SyntaxNode)statementListNode.GetAttribute("statementNode");
            SyntaxNode nextNode = null;
            if(statementListNode != null){
                nextNode = (SyntaxNode)statementListNode.GetAttribute("nextNode");
            }

            if (statementNode != null)
            {
                AnalyzeStatement(statementNode);
            }else{
                throw new Exception($"Semantic error: Empty block; its state will never be altered.");
            }

            if (nextNode.CountAtributtes() != 0)
            {
                AnalyzeStatementList(nextNode);
            }
        }

        // Analyze an individual statement node in the abstract syntax tree
        private void AnalyzeStatement(SyntaxNode statementNode)
        {
            switch(statementNode.Name){
                case "assignStatement":
                    AnalyzeAssignStatement(statementNode);
                    break;

                case "ifStatement":
                    AnalyzeIfStatement(statementNode);
                    break;
                
                case "whileStatement":
                    AnalyzeWhileStatement(statementNode);
                    break;

                case "commandStatement":
                    AnalyzeCommandStatement(statementNode);
                    break;

            }
        }

        // Analyze an assignment statement node in the abstract syntax tree
        private void AnalyzeAssignStatement(SyntaxNode assignStatementNode)
        {
            Token id;
            String valor = null;
            if(assignStatementNode.VerifyKey("inputStatementNode")){
                SyntaxNode inputStatementNode = (SyntaxNode)assignStatementNode.GetAttribute("inputStatementNode");
                AnalyzeInputStatement(inputStatementNode);
            }else{
                SyntaxNode expressionNode = (SyntaxNode)assignStatementNode.GetAttribute("expressionNode");
                AnalyzeExpression((SyntaxNode)assignStatementNode.GetAttribute("expressionNode"));
            }

            SyntaxNodeLeaf idNode = (SyntaxNodeLeaf)assignStatementNode.GetAttribute("idNode");
            AddVariableToSymbolTable(idNode.Value, "Inteiro", idNode.Name, valor, false);
        }

        // Analyze an input statement node in the abstract syntax tree
        private void AnalyzeInputStatement(SyntaxNode inputStatementNode)
        {
            string func = (string)inputStatementNode.GetAttribute("comandoNode");

            if(func == "ler_varios"){
                // Parse the multiple input statement
                
                for (int i = 0; i < inputStatementNode.CountAtributtes()-1; i++)
                {
                    SyntaxNode sumExpression = (SyntaxNode)inputStatementNode.GetAttribute($"sumExpressionNode{i + 1}");
                    AnalyzeSumExpression(sumExpression);
                }
            }
        }

        // Analyze an if statement node in the abstract syntax tree
        private void AnalyzeIfStatement(SyntaxNode ifStatementNode)
        {   
            SyntaxNode expression = (SyntaxNode)ifStatementNode.GetAttribute("expressionNode");
            AnalyzeExpression(expression);
            SyntaxNode block = (SyntaxNode)ifStatementNode.GetAttribute("blockNode");
            AnalyzeBlock(block);
            if (block == null)
            {
                throw new Exception($"Semantic error: Empty block; its state will never be altered.");
            }
            if(ifStatementNode.VerifyKey("ifNotBlockNode")){
                SyntaxNode ifNotBlock = (SyntaxNode)ifStatementNode.GetAttribute("ifNotBlockNode");
                if (ifNotBlock == null)
                {
                    throw new Exception($"Semantic error: Empty block; its state will never be altered.");
                }
                AnalyzeBlock(ifNotBlock);
            }
        }

        // Analyze a while statement node in the abstract syntax tree
        private void AnalyzeWhileStatement(SyntaxNode whileStatementNode)
        {
            SyntaxNode expression = (SyntaxNode)whileStatementNode.GetAttribute("expressionNode");
            AnalyzeExpression(expression);
            SyntaxNode block = (SyntaxNode)whileStatementNode.GetAttribute("blockNode");

            if (block == null)
            {
                throw new Exception($"Semantic error: Empty block, its state will never be altered.");
            }
            AnalyzeBlock(block);
        }

        // Analyze a command statement node in the abstract syntax tree
        private void AnalyzeCommandStatement(SyntaxNode commandStatementNode)
        {
            string func = (string)commandStatementNode.GetAttribute("comandoNode");

            if(func == "mostrar_tocar"){
                
                for (int i = 0; i < commandStatementNode.CountAtributtes()-1; i++)
                {
                    SyntaxNode sumExpression = (SyntaxNode)commandStatementNode.GetAttribute($"sumExpressionNode{i + 1}");
                    AnalyzeSumExpression(sumExpression);
                }
            }else{
                SyntaxNode sumExpression = (SyntaxNode)commandStatementNode.GetAttribute("sumExpressionNode");
                AnalyzeSumExpression(sumExpression);
            }
        }

        // Analyze an expression node in the abstract syntax tree
        private void AnalyzeExpression(SyntaxNode expressionNode)
        {
            SyntaxNode leftNode = (SyntaxNode)expressionNode.GetAttribute("left");
            string comparator = (string)expressionNode.GetAttribute("comparator");
            SyntaxNode rightNode = (SyntaxNode)expressionNode.GetAttribute("right");

            if(expressionNode.Name != "expression"){
                AnalyzeSumExpression(expressionNode);
            }else{
                AnalyzeSumExpression(leftNode);
                if(rightNode != null){
                    AnalyzeSumExpression(rightNode);
                }
            }
        }

        // Analyze a sum expression node in the abstract syntax tree
        private void AnalyzeSumExpression(SyntaxNode sumExpressionNode)
        {
            SyntaxNode leftNode = (SyntaxNode)sumExpressionNode.GetAttribute("left");
            string operatorType = (string)sumExpressionNode.GetAttribute("operator");
            SyntaxNode rightNode = (SyntaxNode)sumExpressionNode.GetAttribute("right");

            if(sumExpressionNode.Name != "sumExpression"){
                AnalyzeMultTerm(sumExpressionNode);
            }else{
                AnalyzeMultTerm(leftNode);
                AnalyzeMultTerm(rightNode);
            }
        }

        // Analyze a multiplication term node in the abstract syntax tree
        private void AnalyzeMultTerm(SyntaxNode multTermNode)
        {
            SyntaxNode leftNode = (SyntaxNode)multTermNode.GetAttribute("left");
            string operatorType = (string)multTermNode.GetAttribute("operator");
            SyntaxNode rightNode = (SyntaxNode)multTermNode.GetAttribute("right");

            if(multTermNode.Name != "multTerm"){
                AnalyzePowerTerm(multTermNode);
            }else{
                AnalyzePowerTerm(leftNode);
                AnalyzePowerTerm(rightNode);
            }
        }

        // Analyze a power term node in the abstract syntax tree
        private void AnalyzePowerTerm(SyntaxNode powerTermNode)
        {
            SyntaxNode leftNode = (SyntaxNode)powerTermNode.GetAttribute("left");
            string operatorType = (string)powerTermNode.GetAttribute("operator");
            SyntaxNode rightNode = (SyntaxNode)powerTermNode.GetAttribute("right");

            if(powerTermNode.Name != "powerTerm"){
                AnalyzeFactor(powerTermNode);
            }else{
                AnalyzeFactor(leftNode);
                AnalyzeFactor(rightNode);
            }
        }

        // Analyze a factor node in the abstract syntax tree
        private void AnalyzeFactor(SyntaxNode factorNode)
        {   
            if((SyntaxNode)factorNode.GetAttribute("expressionNode") != null){
                AnalyzeExpression((SyntaxNode)factorNode.GetAttribute("expressionNode"));
            }
            SyntaxNodeLeaf idNode = (SyntaxNodeLeaf)factorNode.GetAttribute("idNode");
            SyntaxNodeLeaf integerNode = (SyntaxNodeLeaf)factorNode.GetAttribute("integerNode");
            SyntaxNodeLeaf booleanNode = (SyntaxNodeLeaf)factorNode.GetAttribute("booleanNode");
            string signal = (string)factorNode.GetAttribute("signal");

            if (idNode != null)
            {   
                CheckVariableDeclaration(idNode);
                CheckVariableInitialization(idNode);
            }
            else if (integerNode != null)
            {
                // Additional type checks can be added here if needed
            }
            else if (booleanNode != null)
            {
                // Additional type checks can be added here if needed
            }
        }

        // Check if two variable types are the same
        public static bool AreSameType(VariableType type1, VariableType type2)
        {
            if (type1 == null && type2 == null)
            {
                return true;
            }

            if (type1 == null || type2 == null)
            {
                return false;
            }

            return type1.Equals(type2);
        }

        // Check if a variable has been declared in the current scope
        private void CheckVariableDeclaration(SyntaxNodeLeaf idNode)
        {
            string variableName = idNode.Value;

            foreach (ScopeInfo currentScope in scopeStack)
            {
                if (currentScope.SymbolTable.ContainsKey(variableName))
                {
                    return;
                }
                if (!currentScope.SymbolTable.ContainsKey(variableName) && currentScope.Name == "global")
                {
                    throw new Exception($"Semantic error: Variable '{variableName}' has not been declared.");
                }
            }
        }

        // Check if a variable has been initialized before use
        private void CheckVariableInitialization(SyntaxNodeLeaf idNode)
        {
            string variableName = idNode.Value;

            foreach (ScopeInfo currentScope in scopeStack)
            {   
                if(currentScope.SymbolTable.ContainsKey(variableName)){
                    if (currentScope.SymbolTable[variableName].Value == null && currentScope.Name == "global")
                    {
                        throw new Exception($"Semantic error: Variable '{variableName}' has not been initialized.");
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        // Placeholder function for checking if a variable is initialized (not fully implemented)
        private bool IsVariableInitialized(string variableName)
        {
            return true;
        }

        // Enter a new scope in the symbol table
        private void EnterScope(string scopeName)
        {
            ScopeInfo scopeInfo = new ScopeInfo(scopeName);
            scopeStack.Push(scopeInfo);
        }

        // Exit the current scope in the symbol table
        private void ExitScope()
        {
            if (scopeStack.Count > 0)
            {
                scopeStack.Pop();
            }
        }

        // Add a variable to the symbol table
        private void AddVariableToSymbolTable(string variableName, string variableType, string token, string value, bool used)
        {
            ScopeInfo currentScope = scopeStack.Peek();

            if (currentScope.SymbolTable.ContainsKey(variableName))
            {
                throw new Exception($"Semantic error: Variable '{variableName}' has already been declared in this scope.");
            }
            else
            {
                VariableInfo variableInfo = new VariableInfo(variableName, variableType, token, value, used);
                currentScope.SymbolTable.Add(variableName, variableInfo);
            }
        }

        // Class representing information about a scope in the symbol table
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

        // Class representing information about a variable
        private class VariableInfo
        {
            public string Name { get; }
            public string Type { get; }
            public string Token { get; }

            public string Value { get; }
            public bool Used { get; }

            public VariableInfo(string name, string type, string token, string value, bool used)
            {
                Name = name;
                Type = type;
                Token = token;
                Value = value;
                Used = used;
            }
        }

        // Enumeration representing variable types
        public enum VariableType
        {
            Int,
            Bool,
            String
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;

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
        }

        private void AnalyzeProgram(SyntaxNode programNode)
        {
            SyntaxNodeLeaf idNode = (SyntaxNodeLeaf)programNode.GetAttribute("idNode");
            AddVariableToSymbolTable(idNode.Value, "String", idNode.Name,idNode.Value, false);

            SyntaxNode blockNode = (SyntaxNode)programNode.GetAttribute("blockNode");
            AnalyzeBlock(blockNode);
            if (blockNode == null)
            {
                 throw new Exception($"Erro semântico: Bloco vazio seu estado jamais será alterado");
            }
        }

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

        private void AnalyzeStatementList(SyntaxNode statementListNode)
        {
            SyntaxNode statementNode = (SyntaxNode)statementListNode.GetAttribute("statementNode");
            SyntaxNode nextNode = null;
            if(statementListNode != null){
                nextNode = (SyntaxNode)statementListNode.GetAttribute("nextNode");
            }

            if (statementNode.CountAtributtes() != 0)
            {
                AnalyzeStatement(statementNode);
            }else{
                throw new Exception($"Erro semântico: Bloco vazio seu estado jamais será alterado");
            }

            if (nextNode.CountAtributtes() != 0)
            {
                AnalyzeStatementList(nextNode);
            }
        }

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

        private void AnalyzeIfStatement(SyntaxNode ifStatementNode)
        {   
            SyntaxNode expression = (SyntaxNode)ifStatementNode.GetAttribute("expressionNode");
            AnalyzeExpression(expression);
            SyntaxNode block = (SyntaxNode)ifStatementNode.GetAttribute("blockNode");
            AnalyzeBlock(block);
            if (block == null)
            {
                 throw new Exception($"Erro semântico: Bloco vazio seu estado jamais será alterado");
            }
            if(ifStatementNode.VerifyKey("ifNotBlockNode")){
                SyntaxNode ifNotBlock = (SyntaxNode)ifStatementNode.GetAttribute("ifNotBlockNode");
                if (ifNotBlock == null)
                    {
                        throw new Exception($"Erro semântico: Bloco vazio seu estado jamais será alterado");
                    }
                AnalyzeBlock(ifNotBlock);
            }
        }

        private void AnalyzeWhileStatement(SyntaxNode whileStatementNode)
        {
            SyntaxNode expression = (SyntaxNode)whileStatementNode.GetAttribute("expressionNode");
            AnalyzeExpression(expression);
            SyntaxNode block = (SyntaxNode)whileStatementNode.GetAttribute("blockNode");

            if (block == null)
            {
                 throw new Exception($"Erro semântico: Bloco vazio seu estado jamais será alterado");
            }
            AnalyzeBlock(block);
            
        }

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

            
            // AreSameType(id1, id2);


            // Adicione verificações de tipo aqui se necessário
            // Exemplo: Verificar se os tipos de leftNode e rightNode são compatíveis
        }

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

            // Adicione verificações de tipo aqui se necessário
        }

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

            // Adicione verificações de tipo aqui se necessário
        }

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

            // Adicione verificações de tipo aqui se necessário
        }

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

        public static bool AreSameType(VariableType type1, VariableType type2)
        {
            // Verifica se ambos os tipos são nulos
            if (type1 == null && type2 == null)
            {
                return true;
            }

            // Verifica se pelo menos um dos tipos é nulo
            if (type1 == null || type2 == null)
            {
                return false;
            }

            // Compara os tipos
            return type1.Equals(type2);
        }


        private void CheckVariableDeclaration(SyntaxNodeLeaf idNode)
        {
            string variableName = idNode.Value;

            foreach (ScopeInfo currentScope in scopeStack)
            {
                if (!currentScope.SymbolTable.ContainsKey(variableName) && currentScope.Name == "global")
                {
                    // A variável foi encontrada em um escopo, então podemos sair da função
                    throw new Exception($"Erro semântico: Variável '{variableName}' não foi declarada.");
                }else{
                    return;
                }
            }
        }

        private void CheckVariableInitialization(SyntaxNodeLeaf idNode)
        {
            string variableName = idNode.Value;

            foreach (ScopeInfo currentScope in scopeStack)
            {   
                if(currentScope.SymbolTable.ContainsKey(variableName)){
                    if (currentScope.SymbolTable[variableName].Value != null && currentScope.Name == "global")
                    {
                        // A variável foi encontrada em um escopo, então podemos sair da função
                        throw new Exception($"Erro semântico: Variável '{variableName}' não foi inicializada.");
                    }else{
                        return;
                    }
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

        private void AddVariableToSymbolTable(string variableName, string variableType, string token, string value, bool used)
        {
            ScopeInfo currentScope = scopeStack.Peek();

            if (currentScope.SymbolTable.ContainsKey(variableName))
            {
                throw new Exception($"Erro semântico: Variável '{variableName}' já foi declarada neste escopo.");
            }
            else
            {
                VariableInfo variableInfo = new VariableInfo(variableName, variableType, token, value, used);
                currentScope.SymbolTable.Add(variableName, variableInfo);
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

        public enum VariableType
        {
            Int,
            Bool,
            String
        }
    }
}

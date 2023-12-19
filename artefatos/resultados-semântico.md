# Resultados: Semântico
O analisador semântico é responsável por identificar erros relacionados ao significado do código, como problemas no escopo dos nomes, inconsistências entre declarações e uso de nomes, e incompatibilidade de tipos em expressões e comandos. Abaixo, apresentam-se alguns exemplos de entradas e os erros semânticos associados que seriam detectados pelo analisador:

## Sprint 3 - Exemplo 1:
```
programa "teste semântico 1":
inicio
    x = y
fim.
```
### Erro gerado:
```
An unhandled exception of type 'System.Exception' occurred in LLEx.dll: 'Semantic error: Variable 'y' has not been declared.'
```

## Exemplo 2:
```
programa "teste semântico 2":
inicio
    x = 1
    y = 2
    mostrar(z)
    z = y
fim.
```
### Erro gerado:
```
An unhandled exception of type 'System.Exception' occurred in LLEx.dll: 'Semantic error: Variable 'z' has not been declared.'
```
## Sprint 4 - Exemplo 1:
```
programa "exemplo_semantico1":
    inicio
    valor = ler()
    se valor >= 10 entao
    inicio
        x = x + 1
        mostrar(x)
    fim
fim.
```
### Erro gerado:
```
An unhandled exception of type 'System.Exception' occurred in LLEx.dll: 'Semantic error: Variable 'x' has not been declared.'
```

## Exemplo 2:
```
programa "exemplo_semantico2":
inicio
    x = 1
    y = x * 2 + (z / 5)
    z = 2
fim.
```
### Erro gerado:
```
An unhandled exception of type 'System.Exception' occurred in LLEx.dll: 'Semantic error: Variable 'z' has not been declared.'
```

## Exemplo 3:
```
programa "exemplo_semantico3":
inicio
    i = 0
    enquanto i <= 10 faca
    inicio

    fim
fim.
```
### Erro gerado:
```
An unhandled exception of type 'System.Exception' occurred in LLEx.dll: 'Semantic error: Empty block; its state will never be altered.'
```
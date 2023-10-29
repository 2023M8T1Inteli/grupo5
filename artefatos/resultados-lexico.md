# Resultados: Léxico

Aqui estão contidos os testes do analisador léxico, localizado em [/src/LLEx](../src/LLEx/).

Ao executar o analisador léxico, o resultado será escrito no arquivo [Output.llex](../src/LLEx/LLEx/bin/Debug/net6.0/Output.llex). Todos os resultados esperados correspondem aos resultados obtidos, mostrados nas sessões _output_ de cada exemplo. Já o código de input está no arquivo [Program.cgn](../src/LLEx/LLEx/Program.cgn).

> Formato de cada token: `<TOKEN_NAME line=n>value</TOKEN_NAME>`

## Exemplo 1

Código a ser testado:

```
a = 1
b = 12
c = (12+3)
```

Output:

```
<tokens>
	<ID line=1>a</ID>
	<OPREL line=1>=</OPREL>
	<INTEGER line=1>1</INTEGER>
	<ID line=2>b</ID>
	<OPREL line=2>=</OPREL>
	<INTEGER line=2>12</INTEGER>
	<ID line=3>c</ID>
	<OPREL line=3>=</OPREL>
	<LPAR line=3>(</LPAR>
	<INTEGER line=3>12</INTEGER>
	<OPSUM line=3>+</OPSUM>
	<INTEGER line=3>3</INTEGER>
	<RPAR line=3>)</RPAR>
</tokens>
```

## Exemplo 2

Código a ser testado:

```
inicio
    z = -1234
fim
```

Output:

```
<tokens>
	<LBLOCK line=1>inicio</LBLOCK>
	<ID line=2>z</ID>
	<OPREL line=2>=</OPREL>
	<OPSUM line=2>-</OPSUM>
	<INTEGER line=2>1234</INTEGER>
	<RBLOCK line=3>fim</RBLOCK>
</tokens>
```

## Exemplo 3

Código a ser testado:

```
teste = 1+2 -3 *

40/5 ^ 6 %


987
```

Output:

```
<tokens>
	<ID line=1>teste</ID>
	<OPREL line=1>=</OPREL>
	<INTEGER line=1>1</INTEGER>
	<OPSUM line=1>+</OPSUM>
	<INTEGER line=1>2</INTEGER>
	<OPSUM line=1>-</OPSUM>
	<INTEGER line=1>3</INTEGER>
	<OPMUL line=1>*</OPMUL>
	<INTEGER line=3>40</INTEGER>
	<OPMUL line=3>/</OPMUL>
	<INTEGER line=3>5</INTEGER>
	<OPPOW line=3>^</OPPOW>
	<INTEGER line=3>6</INTEGER>
	<OPMUL line=3>%</OPMUL>
	<INTEGER line=6>987</INTEGER>
</tokens>
```

## Exemplo 4

Código a ser testado:

```
se abc <> xyz entao
inicio
    x=(verdade)
    y= ler ( )
fim
```

Output:

```
<tokens>
	<SE line=1>se</SE>
	<ID line=1>abc</ID>
	<OPREL line=1><></OPREL>
	<ID line=1>xyz</ID>
	<ENTAO line=1>entao</ENTAO>
	<LBLOCK line=2>inicio</LBLOCK>
	<ID line=3>x</ID>
	<OPREL line=3>=</OPREL>
	<BOOLEAN line=3>verdade</BOOLEAN>
	<RPAR line=3>)</RPAR>
	<ID line=4>y</ID>
	<OPREL line=4>=</OPREL>
	<COMANDO line=4>ler</COMANDO>
	<LPAR line=4>(</LPAR>
	<RPAR line=4>)</RPAR>
	<RBLOCK line=5>fim</RBLOCK>
</tokens>
```

## Exemplo 5

Código a ser testado:

```
programa :
inicio
    programas = verdade
    verdades = 0
    se entao inicio
        ses = verdades
        programas = ler()
        x = ler_varios(11, 4, 1)
    fim

fim.
```

Output:

```
<tokens>
	<PROGRAMA line=1>programa</PROGRAMA>
	<COLON line=1>:</COLON>
	<LBLOCK line=2>inicio</LBLOCK>
	<ID line=3>programas</ID>
	<OPREL line=3>=</OPREL>
	<BOOLEAN line=3>verdade</BOOLEAN>
	<ID line=4>verdades</ID>
	<OPREL line=4>=</OPREL>
	<INTEGER line=4>0</INTEGER>
	<SE line=5>se</SE>
	<ENTAO line=5>entao</ENTAO>
	<LBLOCK line=5>inicio</LBLOCK>
	<ID line=6>ses</ID>
	<OPREL line=6>=</OPREL>
	<ID line=6>verdades</ID>
	<ID line=7>programas</ID>
	<OPREL line=7>=</OPREL>
	<COMANDO line=7>ler</COMANDO>
	<LPAR line=7>(</LPAR>
	<RPAR line=7>)</RPAR>
	<ID line=8>x</ID>
	<OPREL line=8>=</OPREL>
	<COMANDO line=8>ler_varios</COMANDO>
	<LPAR line=8>(</LPAR>
	<INTEGER line=8>11</INTEGER>
	<COMMA line=8>,</COMMA>
	<INTEGER line=8>4</INTEGER>
	<COMMA line=8>,</COMMA>
	<INTEGER line=8>1</INTEGER>
	<RPAR line=8>)</RPAR>
	<RBLOCK line=9>fim</RBLOCK>
	<RBLOCK line=11>fim</RBLOCK>
	<DOT line=11>.</DOT>
</tokens>
```

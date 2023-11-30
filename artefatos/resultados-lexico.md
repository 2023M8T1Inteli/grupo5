# Resultados do analisador léxico

Aqui estão contidos os testes do analisador léxico, localizado em [/src/LLEx](../src/LLEx/).

Ao executar o analisador léxico, o resultado será escrito no arquivo [Output.llex](../src/LLEx/LLEx/bin/Debug/net6.0/Output.llex). Todos os resultados esperados correspondem aos resultados obtidos, mostrados nas sessões _output_ de cada exemplo. Já o código de input está no arquivo [Program.cgn](../src/LLEx/LLEx/Program.cgn).

> Formato de cada token: `<TOKEN_NAME line=n>value</TOKEN_NAME>`

## Resultados artefato 1

### Exemplo 1

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

### Exemplo 2

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

### Exemplo 3

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

### Exemplo 4

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

### Exemplo 5

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

## Resultados artefato 2

### Exemplo 1

Código a ser testado:

```
programa "cores":
inicio
	x = ler_varios(5, 3, 1)
	y = ler()
	z = ler()
	se ((y == 11) e (z > 12)) ou x entao
	inicio
		mostrar(3)
	fim
	y = ler()
	esperar(1*1000)
	mostrar_tocar(y%10, (y+1) % 10)
fim.
```

Output:

```
<tokens>
	<PROGRAMA line="1">programa</PROGRAMA>
	<DQUOTE line="1">"</DQUOTE>
	<STRING line="1">cores</STRING>
	<DQUOTE line="1">"</DQUOTE>
	<COLON line="1">:</COLON>
	<LBLOCK line="2">inicio</LBLOCK>
	<ID line="3">x</ID>
	<ASSIGN line="3">=</ASSIGN>
	<COMANDO line="3">ler_varios</COMANDO>
	<LPAR line="3">(</LPAR>
	<INTEGER line="3">5</INTEGER>
	<COMMA line="3">,</COMMA>
	<INTEGER line="3">3</INTEGER>
	<COMMA line="3">,</COMMA>
	<INTEGER line="3">1</INTEGER>
	<RPAR line="3">)</RPAR>
	<ID line="4">y</ID>
	<ASSIGN line="4">=</ASSIGN>
	<COMANDO line="4">ler</COMANDO>
	<LPAR line="4">(</LPAR>
	<RPAR line="4">)</RPAR>
	<ID line="5">z</ID>
	<ASSIGN line="5">=</ASSIGN>
	<COMANDO line="5">ler</COMANDO>
	<LPAR line="5">(</LPAR>
	<RPAR line="5">)</RPAR>
	<SE line="6">se</SE>
	<LPAR line="6">(</LPAR>
	<LPAR line="6">(</LPAR>
	<ID line="6">y</ID>
	<OPREL line="6">==</OPREL>
	<INTEGER line="6">11</INTEGER>
	<RPAR line="6">)</RPAR>
	<OPMUL line="6">e</OPMUL>
	<LPAR line="6">(</LPAR>
	<ID line="6">z</ID>
	<OPREL line="6">></OPREL>
	<INTEGER line="6">12</INTEGER>
	<RPAR line="6">)</RPAR>
	<RPAR line="6">)</RPAR>
	<OPSUM line="6">ou</OPSUM>
	<ID line="6">x</ID>
	<ENTAO line="6">entao</ENTAO>
	<LBLOCK line="7">inicio</LBLOCK>
	<COMANDO line="8">mostrar</COMANDO>
	<LPAR line="8">(</LPAR>
	<INTEGER line="8">3</INTEGER>
	<RPAR line="8">)</RPAR>
	<RBLOCK line="9">fim</RBLOCK>
	<ID line="10">y</ID>
	<ASSIGN line="10">=</ASSIGN>
	<COMANDO line="10">ler</COMANDO>
	<LPAR line="10">(</LPAR>
	<RPAR line="10">)</RPAR>
	<COMANDO line="11">esperar</COMANDO>
	<LPAR line="11">(</LPAR>
	<INTEGER line="11">1</INTEGER>
	<OPMUL line="11">*</OPMUL>
	<INTEGER line="11">1000</INTEGER>
	<RPAR line="11">)</RPAR>
	<COMANDO line="12">mostrar_tocar</COMANDO>
	<LPAR line="12">(</LPAR>
	<ID line="12">y</ID>
	<OPMUL line="12">%</OPMUL>
	<INTEGER line="12">10</INTEGER>
	<COMMA line="12">,</COMMA>
	<LPAR line="12">(</LPAR>
	<ID line="12">y</ID>
	<OPSUM line="12">+</OPSUM>
	<INTEGER line="12">1</INTEGER>
	<RPAR line="12">)</RPAR>
	<OPMUL line="12">%</OPMUL>
	<INTEGER line="12">10</INTEGER>
	<RPAR line="12">)</RPAR>
	<RBLOCK line="13">fim</RBLOCK>
	<DOT line="13">.</DOT>
</tokens>
```

### Exemplo 2

Código a ser testado:

```
// Comentários são ignorados, mas quebras de linha não são
programa "peixes do mar": // os tokens devem ser classificados na linha 2
inicio
	x = ler_varios(5, 3, 1)
	y = ler()
	z = ler()/*
	se ((y == 11) e (z == 12)) ou x entao
	inicio
		mostrar(3)
	fim*/
	Enquanto = ler() // Enquanto é ID, e os tokens classificados na linha 11
	enquanto (Enquanto <> 54) e (Enquanto >= 23) faca
	inicio
		tocar(Enquanto % 10)
		Enquanto = ler()
	fim
	mostrar_tocar(0, 8)
fim.
```

Output:

```
<tokens>
	<PROGRAMA line="2">programa</PROGRAMA>
	<DQUOTE line="2">"</DQUOTE>
	<STRING line="2">peixes do mar</STRING>
	<DQUOTE line="2">"</DQUOTE>
	<COLON line="2">:</COLON>
	<LBLOCK line="3">inicio</LBLOCK>
	<ID line="4">x</ID>
	<ASSIGN line="4">=</ASSIGN>
	<COMANDO line="4">ler_varios</COMANDO>
	<LPAR line="4">(</LPAR>
	<INTEGER line="4">5</INTEGER>
	<COMMA line="4">,</COMMA>
	<INTEGER line="4">3</INTEGER>
	<COMMA line="4">,</COMMA>
	<INTEGER line="4">1</INTEGER>
	<RPAR line="4">)</RPAR>
	<ID line="5">y</ID>
	<ASSIGN line="5">=</ASSIGN>
	<COMANDO line="5">ler</COMANDO>
	<LPAR line="5">(</LPAR>
	<RPAR line="5">)</RPAR>
	<ID line="6">z</ID>
	<ASSIGN line="6">=</ASSIGN>
	<COMANDO line="6">ler</COMANDO>
	<LPAR line="6">(</LPAR>
	<RPAR line="6">)</RPAR>
	<ID line="11">Enquanto</ID>
	<ASSIGN line="11">=</ASSIGN>
	<COMANDO line="11">ler</COMANDO>
	<LPAR line="11">(</LPAR>
	<RPAR line="11">)</RPAR>
	<ENQUANTO line="12">enquanto</ENQUANTO>
	<LPAR line="12">(</LPAR>
	<ID line="12">Enquanto</ID>
	<OPREL line="12"><></OPREL>
	<INTEGER line="12">54</INTEGER>
	<RPAR line="12">)</RPAR>
	<OPMUL line="12">e</OPMUL>
	<LPAR line="12">(</LPAR>
	<ID line="12">Enquanto</ID>
	<OPREL line="12">>=</OPREL>
	<INTEGER line="12">23</INTEGER>
	<RPAR line="12">)</RPAR>
	<FACA line="12">faca</FACA>
	<LBLOCK line="13">inicio</LBLOCK>
	<COMANDO line="14">tocar</COMANDO>
	<LPAR line="14">(</LPAR>
	<ID line="14">Enquanto</ID>
	<OPMUL line="14">%</OPMUL>
	<INTEGER line="14">10</INTEGER>
	<RPAR line="14">)</RPAR>
	<ID line="15">Enquanto</ID>
	<ASSIGN line="15">=</ASSIGN>
	<COMANDO line="15">ler</COMANDO>
	<LPAR line="15">(</LPAR>
	<RPAR line="15">)</RPAR>
	<RBLOCK line="16">fim</RBLOCK>
	<COMANDO line="17">mostrar_tocar</COMANDO>
	<LPAR line="17">(</LPAR>
	<INTEGER line="17">0</INTEGER>
	<COMMA line="17">,</COMMA>
	<INTEGER line="17">8</INTEGER>
	<RPAR line="17">)</RPAR>
	<RBLOCK line="18">fim</RBLOCK>
	<DOT line="18">.</DOT>
</tokens>

```
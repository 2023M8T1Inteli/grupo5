# CareAPI

Esta pasta contém a API do projeto, construída com o framework open-source ASP.NET Core 7 utilizando modelo de Models, Services, Controller e DI (Injeção de dependência) e o banco de dados MongoDB. Para rodar a API em ambiente de desenvolvimento, deve-se abrir o terminal na pasta [CareApi](../CareApi/) do repositório e executar o comando:

```
dotnet run
```

Ao rodar o comando e acessar a URL http://localhost:5087 (ambiente de desenvolvimento), a mensagem `API running` é impressa. A mensagem também é impressa ao rodar a aplicação pelo Visual Studio em um container Docker, porém com porta [32770](https://localhost:32770/).

## Banco de dados

Por conta da aplicação ter diversas mudanças no banco de dados no decorrer de seu desenvolvimento, MongoDB foi escolhido por conta de sua estruturação orientada a documentos, o que facilita em um ambiente de crescimento de dados horizontal.

O MongoDB utiliza _collections_ para agrupamento de documentos em um mesmo banco de dados, que estão estruturados da seguinte maneira:

Coleção `User`:

```
{
    Name: string
    Crefito: string
    Email: string
    Role: string
    Password: string
}
```

Coleção `Pacient`:

```
{
    Name: string
    BirthDate: string
    Desease: string
    Cif: string
    Sessions: [
        {
            StartedAt: string (timestamp)
            EndedAt: string (timestamp)
            TherapyName: string
            Results: string
        }
    ]
}
```

Coleção `Therapy`:

```
{
    Name: string
    CreatedByUser: string
    Command: [
        {
            Name: string
            Code: string
            ImageUrl: string
            SoundUrl: string
        }
    ]
}
```

## Documentação

A página Swagger pode ser acessada ao executar o comando para rodar a API. Para acessá-la, basta rodar a aplicação em ambiente de desenvolvimento e navegar para a URL `localhost:5087/sawagger/index.html`. Lá, podem ser acessadas informações sobre os endpoints e modelos de referência. Para mais informações sobre docummentação Swagger, acesse [https://swagger.io/](https://swagger.io/).

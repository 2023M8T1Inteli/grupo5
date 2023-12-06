// program.cs

using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        const string url = "https://localhost:7266/ws";

        // Criando uma conexão SignalR
        await using var connection = new HubConnectionBuilder().WithUrl(url).Build();

        // Lidando com o evento quando um número é recebido do servidor
        connection.On<int>("ReceiveNumber", receivedNumber =>
        {
            Console.WriteLine($"O quadrante selecionado é o {receivedNumber}");
        });

        // Iniciando a conexão SignalR
        await connection.StartAsync();

        // Loop infinito para simular eventos de movimento do mouse
        while (true)
        {
            // Obtendo as coordenadas atuais do cursor do mouse
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            // Identificando o quadrante com base nas coordenadas do mouse
            int quadrante = RandomNumber.IdentificarQuadranteDirecao(x, y);

            // Enviando o número do quadrante identificado para o servidor
            await connection.SendAsync("SendNumber", quadrante);

            // Aguardando um pouco antes de enviar a próxima atualização
            await Task.Delay(3000);
        }
    }
}

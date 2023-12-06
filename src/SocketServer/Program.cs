using Microsoft.AspNetCore.SignalR;

// Creating a web application builder
var builder = WebApplication.CreateBuilder(args);

// Adding SignalR services to the application
builder.Services.AddSignalR();

var app = builder.Build();

// Mapping the hub endpoint to the "/ws" URL
app.MapHub<MyHub>("/ws");

app.Run();

// Defining a SignalR hub named MyHub
class MyHub : Hub
{
    // Method to send a number to all connected clients
    public async Task SendNumber(int number)
    {
        await Clients.All.SendAsync("ReceiveNumber", number); // All substituido pelo ID do cliente web socket
    }
}

// criar projeto windowsForm com websocket do cliente e outro projeto do servidor web socket
using Microsoft.AspNetCore.SignalR;

// Creating a web application builder
var builder = WebApplication.CreateBuilder(args);

// Adding SignalR services to the application
builder.Services.AddSignalR();

var app = builder.Build();

// Mapping the hub endpoint to the "/ws" URL
app.MapHub<MyHub>("/ws");

// Mapping a simple endpoint for testing purposes
app.MapGet("/", () => "Hello World!");

app.Run();

// Defining a SignalR hub named MyHub
class MyHub : Hub
{
    // Method to send a number to all connected clients
    public async Task SendNumber(int number)
    {
        await Clients.All.SendAsync("ReceiveNumber", number);
    }
}

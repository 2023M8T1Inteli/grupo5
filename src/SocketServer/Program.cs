using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<MyHub>("/ws");

app.MapGet("/", () => "Hello World!");

app.Run();

class MyHub : Hub
{
    public async Task SendNumber(int number)
    {
        await Clients.All.SendAsync("ReceiveNumber", number);
    }
}

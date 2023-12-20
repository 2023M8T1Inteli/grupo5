using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using System;

//url from the server (SocketServer)
const string url = "http://localhost:5000/ws";

// Creating a SignalR connection
await using var connection = new HubConnectionBuilder().WithUrl(url).Build();

// Handling the event when a number is received from the server
connection.On<int>("ReceiveNumber", receivedNumber =>
{
    Console.WriteLine($"O quadrante selecionado é o {receivedNumber}");
});

// Starting the SignalR connection
await connection.StartAsync();

// Infinite loop to continuously send numbers from GregMaker
while (true)
{
    //CHANGE THIS FOR THE GREG MAKER OUTPUT
    int myNumber = RandomNumer.Generate();

    // Sending the generated number to the server
    await connection.SendAsync("SendNumber", myNumber);

    // Delaying for 3 seconds before sending the next number
    await Task.Delay(3000);
}

// Keeping the application running to receive messages
Console.ReadLine();


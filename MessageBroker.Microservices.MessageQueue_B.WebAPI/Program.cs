using Grpc.Net.Client;
using MessageBroker;
using MessageBroker.Microservices.MessageQueue_B.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.Map("/add-pool", () =>
{
    var section = builder.Configuration.GetSection("GrpcConfiguration");
    var grpcConfiguration = section.Get<GrpcConfiguration>();
    for (int i = 0; i < 50; i++)
    {
        using var channel = GrpcChannel.ForAddress(grpcConfiguration.Url);
        var client = new MessageService.MessageServiceClient(channel);
        Random rnd = new Random();
        client.Add(new MessageRequest
        {
            Content = Guid.NewGuid().ToString(),
            Priority = rnd.Next(0, 30)
        });
    }
    
});

app.Run();
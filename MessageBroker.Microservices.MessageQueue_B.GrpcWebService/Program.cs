using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Implementations;
using MessageBroker.Microservices.MessageQueue_B.DataAccess;
using MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.Domain.Implementation;
using MessageBroker.Microservices.MessageQueue_B.GrpcWebService.BackgroundServices;
using MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Extensions;
using MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

/*builder.Services.AddDbContext<MessageQueueDbContext>(option => 
    option.UseNpgsql(builder.Configuration.GetConnectionString("MessageQueueDatabase"), 
        x => x.MigrationsAssembly("MessageBroker.Microservices.MessageQueue_B.DataAccess")));*/
// Automapper
builder.Services.AddWebAutoMapper();
// Context
builder.Services.AddDbContext<MessageQueueDbContext>(options => options.UseInMemoryDatabase(databaseName: "message_queue_db"));
// Services
builder.Services.AddSingleton<IConcurrentPriorityQueue<IMessageCore>, ConcurrentPriorityQueue<IMessageCore>>();
builder.Services.AddScoped<IDomainService, DomainService>();
builder.Services.AddHostedService<BackgroundQueueVisualizer>();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<MessageGrpcService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
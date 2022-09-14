using System.Diagnostics.CodeAnalysis;

namespace MessageBroker.Microservices.MessageQueue_B.WebAPI.Models;

[ExcludeFromCodeCoverage]
public class GrpcConfiguration
{
    public GrpcConfiguration(string url)
    {
        Url = url;
    }

    public string Url { get; init; }
}
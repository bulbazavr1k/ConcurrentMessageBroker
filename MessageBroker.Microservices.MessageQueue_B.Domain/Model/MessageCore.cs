using MessageBroker.Domain.Contract;
using MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;

namespace MessageBroker.Microservices.MessageQueue_B.Domain.Model;

public class MessageCore : IMessageCore
{
    public Guid? Id { get; set; }
    public string Content { get; set; }
    public int Priority { get; set; }
}
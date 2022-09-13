using MessageBroker.Domain.Contract;
using MessageBroker.Domain.Contract.Abstractions;

namespace MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;

/// <summary>
/// Message interface
/// </summary>
public interface IMessageCore : IPriority
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid? Id { get; set; }
    /// <summary>
    /// Content message
    /// </summary>
    public string Content { get; init; }
}
using MessageBroker.Domain;

namespace MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;

/// <summary>
/// Domain service 
/// </summary>
public interface IDomainService
{
    /// <summary>
    /// Add message in broker
    /// </summary>
    /// <param name="message">message</param>
    /// <returns>Task</returns>
    Task AddMessage(Message message);

    /// <summary>
    /// show messages
    /// </summary>
    /// <returns></returns>
    Task ShowLastMessages();
}
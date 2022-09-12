namespace MessageBroker.Microservices.MessageQueue_B.DataAccess.Models;

/// <summary>
/// Message
/// </summary>
public class MessageNode
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Content message
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// Priority message
    /// </summary>
    public MessagePriority Priority { get; set; }
    /// <summary>
    /// DateTime update message
    /// </summary>
    public DateTime DateUpdate { get; set; } = DateTime.Now;
}
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
    /// message content
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// message priority
    /// </summary>
    public MessagePriority Priority { get; set; }
    /// <summary>
    /// message update DateTime
    /// </summary>
    public DateTime DateUpdate { get; set; } = DateTime.Now;

    /// <summary>
    /// message status
    /// </summary>
    public MessageNodeStatus MessageNodeStatus { get; set; } = MessageNodeStatus.Created;
}
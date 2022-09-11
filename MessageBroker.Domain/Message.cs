namespace MessageBroker.Domain;

/// <summary>
/// Domain message model
/// </summary>
public class Message
{
    /// <summary>
    /// Text message
    /// </summary>
    /// <example>Hey!</example>
    public string Content { get; init; }
    
    /// <summary>
    /// Priority message
    /// </summary>
    /// <example>Medium</example>
    public Priority Priority { get; init; }        
}
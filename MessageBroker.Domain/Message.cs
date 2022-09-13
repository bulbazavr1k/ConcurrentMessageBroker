using MessageBroker.Domain.Contract;
using MessageBroker.Domain.Contract.Abstractions;

namespace MessageBroker.Domain;

/// <summary>
/// Domain message model
/// </summary>
public class Message : IPriority
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
    public int Priority { get; init; }        
}
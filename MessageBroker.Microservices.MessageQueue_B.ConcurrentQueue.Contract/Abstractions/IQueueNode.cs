using MessageBroker.Domain.Contract.Abstractions;

namespace MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;

/// <summary>
/// Queue node
/// </summary>
public interface IQueueNode<T> : IComparable<int>
{
    /// <summary>
    /// priority value
    /// </summary>
    public int PriorityCtl { get; set; }
    /// <summary>
    /// node lock object
    /// </summary>
    public Mutex NodeLock { get; }
    /// <summary>
    /// Content
    /// </summary>
    public T Content { get; set; }
}
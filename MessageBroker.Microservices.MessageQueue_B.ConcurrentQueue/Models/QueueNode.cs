using System.Diagnostics;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;

namespace MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Models;

/// <inheritdoc cref="MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions.IQueueNode" />
public class QueueNode<T> : IQueueNode<T>, IComparable<QueueNode<T>>
{
    /// <inheritdoc />
    public int PriorityCtl { get; set; }

    /// <inheritdoc />
    public Mutex NodeLock { get; }

    public T Content { get; set; }

    /// <inheritdoc />
    public int CompareTo(QueueNode<T>? other)
    {
        Debug.Assert(other != null, nameof(other) + " != null");
        return PriorityCtl.CompareTo(other.PriorityCtl);
    }

    /// <inheritdoc />
    public int CompareTo(int other)  => PriorityCtl.CompareTo(other);

    /// <summary>
    /// queue node constructor
    /// </summary>
    public QueueNode(T content, int priority)
    {
        Content = content;
        NodeLock = new Mutex();
        PriorityCtl = priority;
    }
}
using System.Collections.Concurrent;
using MessageBroker.Domain.Contract.Abstractions;

namespace MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;

/// <summary>
/// Concurrent (fine grained b-tree) priority queue
/// </summary>
/// <typeparam name="TElement">Content</typeparam>
/// <typeparam name="TPriority">Priority</typeparam>
public interface IConcurrentPriorityQueue<T>
{
    bool TryAdd(T item, int priority);
    bool TryTake(out T item);
}
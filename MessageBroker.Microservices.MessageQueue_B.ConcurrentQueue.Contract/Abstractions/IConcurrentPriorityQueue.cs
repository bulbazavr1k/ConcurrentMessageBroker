using System.Collections.Concurrent;
using MessageBroker.Domain.Contract.Abstractions;

namespace MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;

/// <summary>
/// Concurrent priority queue
/// </summary>
/// <typeparam name="T">Priority status</typeparam>
public interface IConcurrentPriorityQueue<T> : IProducerConsumerCollection<T>, IReadOnlyCollection<T>
where T : IPriority
{
}
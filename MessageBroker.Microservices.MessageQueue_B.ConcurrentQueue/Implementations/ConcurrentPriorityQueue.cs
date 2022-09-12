using System.Collections;
using System.Collections.Concurrent;
using MessageBroker.Domain.Contract;
using MessageBroker.Domain.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;

namespace MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Implementations;

/// <inheritdoc />
public class ConcurrentPriorityQueue<T> : IConcurrentPriorityQueue<T> where T : IPriority
{
    private int _count;
    //TODO: maybe implement through linked list
    private ConcurrentQueue<T> _high = new();
    private ConcurrentQueue<T> _medium = new();
    private ConcurrentQueue<T> _low = new();
    
    public IEnumerator<T> GetEnumerator()
    {
        return _high.Concat(_medium).Concat(_low).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

    public void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }

    int ICollection.Count =>  _high.Count + _medium.Count + _low.Count;

    public bool IsSynchronized { get; }
    public object SyncRoot { get; }
    
    public void CopyTo(T[] array, int index)
    {
        throw new NotImplementedException();
    }

    public T[] ToArray()
    {
        return _high.Concat(_medium).Concat(_low).ToArray();
    }

    public bool TryAdd(T item) => item.Priority switch
    {
        Priority.Low => ((IProducerConsumerCollection<T>) _low).TryAdd(item),
        Priority.Medium => ((IProducerConsumerCollection<T>) _medium).TryAdd(item),
        Priority.High => ((IProducerConsumerCollection<T>) _high).TryAdd(item),
        _ => throw new ArgumentOutOfRangeException()
    };

    public bool TryTake(out T item)
    {
        if (((IProducerConsumerCollection<T>) _high).TryTake(out item)) return true;
        if (((IProducerConsumerCollection<T>) _medium).TryTake(out item)) return true;
        if (((IProducerConsumerCollection<T>) _low).TryTake(out item)) return true;
        return false;
    }

    int IReadOnlyCollection<T>.Count =>  ((ICollection)this).Count;
}
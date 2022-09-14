using System.Collections;
using System.Collections.Concurrent;
using MessageBroker.Domain.Contract;
using MessageBroker.Domain.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Models;

namespace MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Implementations;

/// <inheritdoc />
public class ConcurrentPriorityQueue<T> : IConcurrentPriorityQueue<T>
{
    private IQueueNode<T>[] _nodes = new QueueNode<T>[100];
    private int[] _statuses = new int[100];
    private int _count = 0;
    private int _tailIndex = 0;
    private int _headIndex = 0;

    public int Count => _count;


    /// <summary>
    /// Try add item on queue
    /// </summary>
    /// <param name="item"></param>
    /// <param name="priority"></param>
    /// <returns></returns>
    public bool TryAdd(T item, int priority)
    {
        var node = new QueueNode<T>(item, priority);
        var countCurrent = _count;
        Interlocked.Increment(ref _count);
        if (countCurrent == _nodes.Length)
        {
            lock (_nodes)
            {
                _nodes[countCurrent + 20] = default;
                lock (_statuses)
                {
                    _statuses[countCurrent + 20] = 0;
                }
            }
        }

        MoveUpComparer(node, countCurrent);

        return true;
    }

    /// <summary>
    /// Try take queue
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool TryTake(out T item)
    {
        if (_count == 0)
        {
            item = default;
            return false;
        }

        var lastNodeIndex = Interlocked.Decrement(ref _count);

        var lastNode = _nodes[lastNodeIndex];
        item = _nodes[0].Content;
        MoveDownComparer(lastNode, 0);
        _nodes[lastNodeIndex] = default;

        return true;
    }

    /// <summary>
    /// The binary logarithm of <see cref="Arity" />.
    /// </summary>
    private const int Log2Arity = 2;

    /// <summary>
    /// Gets the index of an element's parent.
    /// </summary>
    private int GetParentIndex(int index) => (index - 1) >> Log2Arity;

    /// <summary>
    /// Gets the index of the first child of an element.
    /// </summary>
    private int GetFirstChildIndex(int index) => (index << Log2Arity) + 1;

    /// <summary>
    /// Specifies the arity of the d-ary heap, which here is quaternary.
    /// It is assumed that this value is a power of 2.
    /// </summary>
    private const int Arity = 4;

    private void MoveUpComparer(IQueueNode<T> node, int nodeIndex)
    {
        while (nodeIndex > 0)
        {
            var parentIndex = GetParentIndex(nodeIndex);

            while (_nodes[parentIndex] == null)
            {
            }

            var parent = _nodes[parentIndex];
            lock (parent)
            {
                if (node.CompareTo(parent.PriorityCtl) < 0)
                {
                    _nodes[nodeIndex] = parent;
                    Interlocked.Exchange(ref nodeIndex, parentIndex);
                }
                else
                {
                    break;
                }
            }
        }

        _nodes[nodeIndex] = node;
    }

    /// <summary>
    /// Moves a node down in the tree to restore heap order.
    /// </summary>
    private void MoveDownComparer(IQueueNode<T> node, int nodeIndex)
    {
        int i;
        while ((i = GetFirstChildIndex(nodeIndex)) < _count)
        {
            lock (_nodes[i])
            {
                var minChild = _nodes[i];
                while (minChild != _nodes[i])
                {
                    var newMinChild = _nodes[i];
                    newMinChild.NodeLock.WaitOne();
                    minChild.NodeLock.ReleaseMutex();
                    minChild = newMinChild;
                }

                var minChildIndex = i;

                var childIndexUpperBound = Math.Min(i + Arity, _count);
                while (++i < childIndexUpperBound)
                {
                    lock (_nodes[i])
                    {
                        var nextChild = _nodes[i];

                        // nextChild.NodeLock.WaitOne();

                        while (nextChild != _nodes[i])
                        {
                            var newNextChild = _nodes[i];
                            newNextChild.NodeLock.WaitOne();
                            nextChild.NodeLock.ReleaseMutex();
                            nextChild = newNextChild;
                        }

                        if (nextChild.CompareTo(minChild.PriorityCtl) >= 0) continue;
                        minChild = nextChild;
                        minChildIndex = i;
                    }
                }

                // Heap property is satisfied; insert node in this location.
                if (node.CompareTo(minChild.PriorityCtl) <= 0)
                {
                    break;
                }

                // Move the minimal child up by one node and
                // continue recursively from its location.
                _nodes[nodeIndex] = minChild;
                nodeIndex = minChildIndex;
            }
        }

        _nodes[nodeIndex] = node;
    }
}
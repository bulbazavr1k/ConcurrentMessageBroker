using AutoMapper;
using MessageBroker.Domain;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.DataAccess;
using MessageBroker.Microservices.MessageQueue_B.DataAccess.Models;
using MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.Domain.Model;
using Microsoft.Extensions.Logging;

namespace MessageBroker.Microservices.MessageQueue_B.Domain.Implementation;

/// <inheritdoc />
public class DomainService : IDomainService
{
    private readonly MessageQueueDbContext _messageQueueDbContext;
    private readonly IMapper _mapper;
    private readonly IConcurrentPriorityQueue<IMessageCore> _concurrentPriorityQueue;
    private readonly ILogger<DomainService> _logger;

    /// <summary>
    /// domain service constructor
    /// </summary>
    /// <param name="messageQueueDbContext">db context</param>
    /// <param name="mapper">mapper</param>
    /// <param name="concurrentPriorityQueue">priority queue</param>
    /// <param name="logger">logger</param>
    public DomainService(MessageQueueDbContext messageQueueDbContext, 
        IMapper mapper, 
        IConcurrentPriorityQueue<IMessageCore> concurrentPriorityQueue,
        ILogger<DomainService> logger)
    {
        _messageQueueDbContext = messageQueueDbContext;
        _mapper = mapper;
        _concurrentPriorityQueue = concurrentPriorityQueue;
        _logger = logger;
    }
    /// <inheritdoc />
    public async Task AddMessage(Message message)
    {
        var messageNode = _mapper.Map<MessageNode>(message);
        _messageQueueDbContext.MessageNodes.Add(messageNode);
        await _messageQueueDbContext.SaveChangesAsync();
        var messageCore = _mapper.Map<MessageCore>(messageNode);
        _concurrentPriorityQueue.TryAdd(messageCore, messageCore.Priority);
    }

    /// <inheritdoc />
    public Task ShowLastMessages()
    {
        if (_concurrentPriorityQueue.TryTake(out var messageCore))
            _logger.LogInformation("Priority: {p1}| Content: {p2}", messageCore.Content, messageCore.Priority);
        else
            _logger.LogInformation("Message queue is empty");
        
        return Task.CompletedTask;
    }
}
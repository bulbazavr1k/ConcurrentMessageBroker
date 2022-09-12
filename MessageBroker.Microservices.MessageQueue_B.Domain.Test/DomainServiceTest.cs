using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.DataAccess;
using MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessageBroker.Microservices.MessageQueue_B.Domain.Test;

[ExcludeFromCodeCoverage]
public class DomainServiceTest
{
    
    private readonly Mock<IMapper> _mapperMock;
    private readonly Fixture _fixture;

    /// <summary>
    /// Constructor of domain service
    /// </summary>
    public DomainServiceTest()
    {
        _mapperMock = new Mock<IMapper>();
        _fixture = new Fixture();
    }
    
    [Theory, AutoData]
    public async Task AddQueue(
        IMapper mapper,
        IConcurrentPriorityQueue<IMessageCore> concurrentPriorityQueue,
        ILogger<DomainService> logger)
    {
        var dbContextOptions = new DbContextOptionsBuilder<MessageQueueDbContext>()
            .UseInMemoryDatabase(_fixture.Create<string>())
            .Options;

        await using var dbContext = new MessageQueueDbContext(dbContextOptions);
        var service = new DomainService(dbContext, mapper, concurrentPriorityQueue, logger);
    }
}
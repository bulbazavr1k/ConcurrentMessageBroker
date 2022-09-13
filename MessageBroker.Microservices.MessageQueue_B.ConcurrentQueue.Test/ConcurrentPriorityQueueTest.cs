using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.Xunit2;
using MessageBroker.Domain.Contract;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Implementations;
using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Test.Extensions;
using MessageBroker.Microservices.MessageQueue_B.DataAccess.Models;
using MessageBroker.Microservices.MessageQueue_B.Domain.Model;
using Xunit;

namespace MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Test;

[ExcludeFromCodeCoverage]
public class ConcurrentPriorityQueueTest
{
    // private readonly MapperConfiguration _mapperConfiguration;
    private readonly Fixture _fixture;

    /// <summary>
    /// Constructor of domain service
    /// </summary>
    public ConcurrentPriorityQueueTest()
    {
        // _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MessageDomainProfile>());
        _fixture = new Fixture();
    }

    [Theory, AutoData]
    public async Task AddQueue()
    {
        var concurrentQueue = new ConcurrentPriorityQueue<MessageCore>();
        for (var i = 0; i < 5; i++)
        {
            var pr = new MessageCore
            {
                Id = Guid.NewGuid(),
                Priority = _fixture.CreateInt(0, 10),
                Content = _fixture.Create<string>()
            };
            concurrentQueue.TryAdd(pr, pr.Priority);
        }

        var t = concurrentQueue;
        var cnt = concurrentQueue.Count;
        for (var i = 0; i < cnt; i++)
        {
            if (concurrentQueue.TryTake(out var item))
            {
                Console.WriteLine(item.Priority);
            }
            else
            {
                Console.WriteLine("ERR");
            }
        }
        /*var list = new List<Task>();
        for (var i = 0; i < 50; i++)
        {
           list.Add(Task.Run(() =>
           {
               var pr = new MessageCore
               {
                   Id = new Guid(),
                   Priority = _fixture.Create<Priority>(),
                   Content = _fixture.Create<string>()
               };
               concurrentQueue.TryAdd(pr, (int)pr.Priority);
           }));
        }*/
        
    }
    
 
}
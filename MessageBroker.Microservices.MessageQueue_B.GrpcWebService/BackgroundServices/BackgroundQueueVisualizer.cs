using MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.Domain.Model;

namespace MessageBroker.Microservices.MessageQueue_B.GrpcWebService.BackgroundServices;

/// <summary>
/// Background service queue visualizer
/// </summary>
public class BackgroundQueueVisualizer : BackgroundService
{
    private readonly IConcurrentPriorityQueue<IMessageCore> _queue;
    private readonly ILogger<BackgroundQueueVisualizer> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="queue">queue singleton</param>
    /// <param name="logger"></param>
    public BackgroundQueueVisualizer(IConcurrentPriorityQueue<IMessageCore> queue, ILogger<BackgroundQueueVisualizer> logger)
    {
        _queue = queue;
        _logger = logger;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_queue.TryTake(out var item))
                {
                    _logger.LogInformation("Priority: {p1} | Message: {p2}", item.Priority, item.Content);
                }
                else
                {
                    _logger.LogInformation("Queue empty");
                }

                Thread.Sleep(1000);
            }
        }, stoppingToken);
    }
}
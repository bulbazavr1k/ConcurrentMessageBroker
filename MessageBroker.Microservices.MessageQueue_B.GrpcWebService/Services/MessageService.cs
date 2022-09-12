using AutoMapper;
using Grpc.Core;
using MessageBroker.Domain;

namespace MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Services;

/// <summary>
/// Grpc message service
/// </summary>
public class MessageGrpcService : MessageService.MessageServiceBase
{
    private readonly ILogger<MessageGrpcService> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// Grpc message service constructor DI
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public MessageGrpcService(ILogger<MessageGrpcService> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }
    /// <summary>
    /// Add a message to queue
    /// </summary>
    /// <param name="request">request model</param>
    /// <param name="context">context</param>
    /// <returns>response empty</returns>
    public override async Task<EmptyReply> Add(MessageRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Content: {p1} Priority: {p2}", request.Content, request.Priority);
        var model = _mapper.Map<Message>(request);
        
        return new EmptyReply();
    }
}
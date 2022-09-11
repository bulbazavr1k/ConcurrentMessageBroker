using Grpc.Core;

namespace MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Services;

public class MessageGrpcService : MessageService.MessageServiceBase
{
    private readonly ILogger<MessageGrpcService> _logger;

    public MessageGrpcService(ILogger<MessageGrpcService> logger)
    {
        _logger = logger;
    }
    public override async Task<EmptyReply> Add(MessageRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Content: {p1} Priority: {p2}", request.Content, request.Priority);
        return new EmptyReply();
    }
}
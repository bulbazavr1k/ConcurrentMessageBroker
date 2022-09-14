using AutoMapper;
using Grpc.Core;
using MessageBroker.Domain;
using MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;

namespace MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Services;

/// <summary>
/// Grpc message service
/// </summary>
public class MessageGrpcService : MessageService.MessageServiceBase
{
    private readonly ILogger<MessageGrpcService> _logger;
    private readonly IDomainService _domainService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Grpc message service constructor DI
    /// </summary>
    /// <param name="logger">logger</param>
    /// <param name="domainService">domain service</param>
    /// <param name="mapper">auto mapper</param>
    public MessageGrpcService(ILogger<MessageGrpcService> logger, IDomainService domainService, IMapper mapper)
    {
        _logger = logger;
        _domainService = domainService;
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
        var model = _mapper.Map<Message>(request);
        await _domainService.AddMessage(model);
        return new EmptyReply();
    }
}
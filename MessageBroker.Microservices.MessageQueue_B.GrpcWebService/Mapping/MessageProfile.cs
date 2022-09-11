using AutoMapper;
using MessageBroker.Domain;

namespace MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Mapping;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageRequest>();
        CreateMap<MessageRequest, Message>();
    }
}
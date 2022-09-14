using AutoMapper;
using MessageBroker.Domain;
using MessageBroker.Microservices.MessageQueue_B.DataAccess.Models;
using MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.Domain.Model;

namespace MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Mapping;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageRequest>(MemberList.None);
        CreateMap<MessageRequest, Message>(MemberList.None);
        CreateMap<Message, MessageNode>(MemberList.None);
        CreateMap<MessageNode, Message>(MemberList.None);
        
        CreateMap<MessageCore, MessageNode>(MemberList.None);
        CreateMap<MessageNode, MessageCore>(MemberList.None);
        CreateMap<MessageNode, IMessageCore>(MemberList.None);
        
        CreateMap<Message, MessageCore>(MemberList.None);
        CreateMap<MessageCore, Message>(MemberList.None);
    }
}
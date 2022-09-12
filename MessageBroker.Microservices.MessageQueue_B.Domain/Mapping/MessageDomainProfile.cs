using AutoMapper;
using MessageBroker.Domain;
using MessageBroker.Microservices.MessageQueue_B.DataAccess.Models;
using MessageBroker.Microservices.MessageQueue_B.Domain.Contract.Abstractions;
using MessageBroker.Microservices.MessageQueue_B.Domain.Model;

namespace MessageBroker.Microservices.MessageQueue_B.Domain.Mapping;

/// <summary>
/// Message mapping profile
/// </summary>
public class MessageDomainProfile : Profile
{
    public MessageDomainProfile()
    {
        CreateMap<Message, MessageNode>();
        CreateMap<MessageNode, Message>();
        
        CreateMap<IMessageCore, MessageNode>();
        CreateMap<MessageNode, IMessageCore>();
        
        CreateMap<Message, IMessageCore>();
        CreateMap<IMessageCore, Message>();
        
    }
}
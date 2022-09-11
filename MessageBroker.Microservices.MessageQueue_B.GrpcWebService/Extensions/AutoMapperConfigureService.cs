using AutoMapper;
using MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Mapping;

namespace MessageBroker.Microservices.MessageQueue_B.GrpcWebService.Extensions;

/// <summary>
/// Add configuration AutoMapper
/// </summary>
public static class AutoMapperConfigureService
{
    /// <summary>
    /// Add Profiles DI
    /// </summary>
    /// <param name="services"></param>
    public static void AddProfilesAutoMapper(this IServiceCollection services)
    {
        var profiles = typeof(MessageProfile).Assembly.GetTypes().Where(t => t.BaseType == typeof(Profile));
        var mappingConfig = new MapperConfiguration(mc =>
        {
            foreach (var profile in profiles)
            {
                mc.AddProfile(profile);
            }
        });

        mappingConfig.AssertConfigurationIsValid();
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
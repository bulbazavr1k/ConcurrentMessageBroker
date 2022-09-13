using AutoFixture;

namespace MessageBroker.Microservices.MessageQueue_B.ConcurrentQueue.Test.Extensions;

public static class AutoFixtureExtensions
{
    public static int CreateInt(this IFixture fixture, int min, int max)
    {
        return fixture.Create<int>() % (max - min + 1) + min;
    }
}
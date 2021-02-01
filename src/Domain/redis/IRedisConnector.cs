using StackExchange.Redis;

namespace Domain
{
    public interface IRedisConnector
    {

        // attributes
        ConnectionMultiplexer Connection { get; }

        // behavior
        void CacheResults(IRedditData data, int page);
        bool Initialize();

    }
}

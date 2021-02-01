using Domain;
using StackExchange.Redis;

namespace TestEnvironment
{
    public class MockRedisConnector : IRedisConnector
    {

        public ConnectionMultiplexer Connection { get; set; }

        public void CacheResults(IRedditData data, int page)
        {
            // do nothing
        }

        public bool Initialize()
        {
            return false;
        }

    }
}

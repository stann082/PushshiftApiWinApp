using StackExchange.Redis;

namespace Domain
{
    public class NullRedisConnector : IRedisConnector
    {

        public static IRedisConnector Singleton = new NullRedisConnector();

        private NullRedisConnector()
        {

        }

        public ConnectionMultiplexer Connection { get; }

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

using StackExchange.Redis;

namespace Domain
{
    public class RedisConnection : IRedisConnection
    {

        #region Constructors

        public RedisConnection(ConnectionMultiplexer connection)
        {
            Connection = connection;
        }

        #endregion

        #region Properties

        private ConnectionMultiplexer Connection { get; set; }

        #endregion

    }
}

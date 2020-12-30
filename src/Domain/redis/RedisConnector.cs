using StackExchange.Redis;
using System;

namespace Domain
{
    public class RedisConnector : IRedisConnector
    {

        public static IRedisConnector Singleton = new RedisConnector();

        #region Constructors

        private RedisConnector()
        {
            Connection = null;
        }

        #endregion

        #region Properties

        public IRedisConnection Connection { get; private set; }
        private IApplicationLogger Logger => ApplicationLogger.Singleton;

        #endregion

        #region Public Methods

        public void Initialize()
        {
            Lazy<ConnectionMultiplexer> lazyConnection = new(() => ConnectionMultiplexer.Connect("localhost"));

            try
            {
                ConnectionMultiplexer multiplexer = lazyConnection.Value;
                Connection = new RedisConnection(multiplexer);
            }
            catch (RedisConnectionException rce)
            {
                Connection = NullRedisConnection.Singleton;
                Logger.LogError(rce.Message);
            }
            catch (Exception e)
            {
                Connection = NullRedisConnection.Singleton;
                Logger.LogError(e);
            }
        }

        #endregion

    }
}

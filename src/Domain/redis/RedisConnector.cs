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

        public ConnectionMultiplexer Connection { get; private set; }

        private IDatabase Cache => Connection.GetDatabase();
        private IApplicationLogger Logger => ApplicationLogger.Singleton;

        #endregion

        #region Public Methods

        public void CacheResults(IRedditData data, int page)
        {
            RedisKey key = new(page.ToString());
            RedisValue value = new(data.OriginalResponse);
            Cache.StringSet(key, value);
        }

        public bool Initialize()
        {
            Lazy<ConnectionMultiplexer> lazyConnection = new(() => ConnectionMultiplexer.Connect("localhost"));

            try
            {
                Connection = lazyConnection.Value;
                return true;
            }
            catch (RedisConnectionException rce)
            {
                Logger.LogError(rce.Message);
                return false;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
        }

        #endregion

    }
}

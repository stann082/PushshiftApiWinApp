namespace Domain
{
    public class NullRedisConnection : IRedisConnection
    {

        public static IRedisConnection Singleton = new NullRedisConnection();

        private NullRedisConnection()
        {

        }

    }
}

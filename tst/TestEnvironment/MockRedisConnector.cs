using Domain;

namespace TestEnvironment
{
    public class MockRedisConnector : IRedisConnector
    {

        public IRedisConnection Connection { get; set; }

        public void Initialize()
        {
            // do nothing
        }

    }
}

namespace Domain
{
    public interface IRedisConnector
    {

        // attributes
        IRedisConnection Connection { get; }

        // behavior
        void Initialize();

    }
}

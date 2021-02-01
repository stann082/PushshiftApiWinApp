namespace Domain
{
    public interface IRedditData
    {

        IContent[] Contents { get; }
        string OriginalResponse { get; set; }

    }
}

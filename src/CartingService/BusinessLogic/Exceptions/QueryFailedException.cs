namespace CartingService.BusinessLogic.Exceptions;


[Serializable]
public class QueryFailedException : Exception
{
    public QueryFailedException() { }
    public QueryFailedException(string message) : base(message) { }
    public QueryFailedException(string message, Exception inner) : base(message, inner) { }
    protected QueryFailedException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
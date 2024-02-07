namespace CartingService.BusinessLogic.Exceptions;

[System.Serializable]
public class CartLookupException : System.Exception
{
    public CartLookupException() { }
    public CartLookupException(string message) : base(message) { }
    public CartLookupException(string message, System.Exception inner) : base(message, inner) { }
    protected CartLookupException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
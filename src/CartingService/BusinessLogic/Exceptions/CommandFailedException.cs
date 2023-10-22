namespace CartingService.BusinessLogic.Exceptions;

[Serializable]
public class CommandFailedException : Exception
{
    public CommandFailedException() { }
    public CommandFailedException(string message) : base(message) { }
    public CommandFailedException(string message, Exception inner) : base(message, inner) { }
    protected CommandFailedException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

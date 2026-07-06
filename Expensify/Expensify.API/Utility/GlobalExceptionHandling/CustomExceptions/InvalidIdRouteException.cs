using System.Runtime.Serialization;

namespace Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;

public class InvalidIdRouteException : Exception
{
    public InvalidIdRouteException(string message)
        : base(message) { }

    protected InvalidIdRouteException(SerializationInfo info, StreamingContext ctxt)
        : base(info, ctxt) { }
}

using System.Runtime.Serialization;

namespace Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;

public class InvalidIdRouteException(string message) : Exception(message)
{
}

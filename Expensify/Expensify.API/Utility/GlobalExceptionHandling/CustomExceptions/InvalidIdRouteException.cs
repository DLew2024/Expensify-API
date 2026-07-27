namespace Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;

[Serializable]
public class InvalidIdRouteException(string message) : Exception(message) { }

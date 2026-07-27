namespace Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;

[Serializable]
public class RequiredPropertyException(string message) : Exception(message) { }

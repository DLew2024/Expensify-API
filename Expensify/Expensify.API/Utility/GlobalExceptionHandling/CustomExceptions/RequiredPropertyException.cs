using System.Runtime.Serialization;

namespace Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;

[Serializable]
public class RequiredPropertyException(string message) : Exception(message)
{
}

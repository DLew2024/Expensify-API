using System.Runtime.Serialization;

namespace Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;

[Serializable]
public class EntityNotFoundException(string message) : Exception(message)
{
}

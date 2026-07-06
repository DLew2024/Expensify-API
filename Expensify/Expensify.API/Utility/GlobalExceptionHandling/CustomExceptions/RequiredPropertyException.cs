using System.Runtime.Serialization;

namespace Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;

[Serializable]
public class RequiredPropertyException : Exception
{
	public RequiredPropertyException(string message)
		: base(message) { }

	protected RequiredPropertyException(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt) { }
}

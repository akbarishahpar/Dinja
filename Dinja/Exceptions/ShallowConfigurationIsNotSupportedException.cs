namespace Dinja.Exceptions;

public class ShallowConfigurationIsNotSupportedException : Exception
{
    public ShallowConfigurationIsNotSupportedException(string key) : base(message: $"Key = {key}")
    {
    }
}
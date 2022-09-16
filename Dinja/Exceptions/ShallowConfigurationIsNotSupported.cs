namespace Dinja.Exceptions;

public class ShallowConfigurationIsNotSupported : Exception
{
    public ShallowConfigurationIsNotSupported(string key) : base(message: $"Key = {key}")
    {
    }
}
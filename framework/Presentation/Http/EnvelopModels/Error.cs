// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Octopus.Presentation.Http.EnvelopModels;

public sealed record Error
{
    private Error(string code, string message, string property = null, object attemptedValue = null)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Property = property;
        AttemptedValue = attemptedValue;
    }

    public string Code { get; }

    public string Message { get; }

    public string Property { get; }

    public object AttemptedValue { get; }

    public static Error Create(string code, string message, string property = null, object attemptedValue = null)
    {
        return new Error(code, message, property, attemptedValue);
    }
}

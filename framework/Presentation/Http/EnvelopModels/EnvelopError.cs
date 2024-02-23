namespace Octopus.Presentation.Http.EnvelopModels;

public sealed class EnvelopError
{
    private EnvelopError(string code, string message,
        string property = null, object attemptedValue = null)
    {
        if (code == null)
            throw new ArgumentNullException(nameof(code));

        if (message == null)
            throw new ArgumentNullException(nameof(message));

        Code = code;
        Message = message;
        Property = property;
        AttemptedValue = attemptedValue;
    }

    public string Code { get; }
    public string Message { get; }
    public string Property { get; }
    public object AttemptedValue { get; }

    public static EnvelopError Create(string code, string message, string property = null, object attemptedValue = null)
    {
        return new EnvelopError(code, message, property, attemptedValue);
    }
}

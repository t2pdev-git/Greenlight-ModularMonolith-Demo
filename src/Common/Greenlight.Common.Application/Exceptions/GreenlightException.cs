using Greenlight.Common.Domain;

namespace Greenlight.Common.Application.Exceptions;

public sealed class GreenlightException(string requestName, Error? error = default, Exception? innerException = default)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}

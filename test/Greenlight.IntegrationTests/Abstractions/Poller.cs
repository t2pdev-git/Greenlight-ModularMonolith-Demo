using Greenlight.Common.Domain;

namespace Greenlight.IntegrationTests.Abstractions;

internal static class Poller
{
    internal static async Task<Result<T>> WaitAsync<T>(TimeSpan timeout, Func<Task<Result<T>>> func)
    {
        var timeoutError = Error.Failure(
            "Poller.TestTimeout",
            $"Poller for test Result<{typeof(T).Name}> timed out after {timeout.Seconds} seconds");

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        DateTime stopPollingAtUtc = DateTime.UtcNow.Add(timeout);
        while (DateTime.UtcNow < stopPollingAtUtc && await timer.WaitForNextTickAsync())
        {
            Result<T> result = await func();

            if (result.IsSuccess)
            {
                return result;
            }
        }

        return Result.Failure<T>(timeoutError);
    }
}

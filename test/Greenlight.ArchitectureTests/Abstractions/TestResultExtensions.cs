using NetArchTest.Rules;
using Shouldly;

namespace Greenlight.ArchitectureTests.Abstractions;

internal static class TestResultExtensions
{
    internal static void ShouldBeSuccessful(this TestResult testResult)
    {
        testResult.FailingTypes?.ShouldBeEmpty();
    }
}

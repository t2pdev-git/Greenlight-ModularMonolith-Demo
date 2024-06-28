﻿using NetArchTest.Rules;
using Shouldly;

namespace Greenlight.Modules.Users.ArchitectureTests.Abstractions;

internal static class TestResultExtensions
{
    internal static void ShouldBeSuccessful(this TestResult testResult)
    {
        testResult.FailingTypes?.ShouldBeEmpty();
    }
}

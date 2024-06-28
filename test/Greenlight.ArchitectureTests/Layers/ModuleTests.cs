using System.Reflection;
using Greenlight.ArchitectureTests.Abstractions;
using Greenlight.Modules.Initiatives.Infrastructure;
using Greenlight.Modules.Users.Domain.Users;
using Greenlight.Modules.Users.Infrastructure;
using MassTransit;
using NetArchTest.Rules;

namespace Greenlight.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void UsersModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [
            InitiativesNamespace
        ];
        string[] integrationEventsModules =
        [
            InitiativesIntegrationEventsNamespace,
        ];

        List<Assembly> usersAssemblies =
        [
            typeof(User).Assembly,
            Modules.Users.Application.AssemblyReference.Assembly,
            Modules.Users.Presentation.AssemblyReference.Assembly,
            typeof(UsersModule).Assembly
        ];

        Types.InAssemblies(usersAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void InitiativesModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace];
        string[] integrationEventsModules =
        [
            UsersIntegrationEventsNamespace
        ];

        List<Assembly> initiativesAssemblies =
        [
            typeof(Event).Assembly,
            Modules.Initiatives.Application.AssemblyReference.Assembly,
            Modules.Initiatives.Presentation.AssemblyReference.Assembly,
            typeof(InitiativesModule).Assembly
        ];

        Types.InAssemblies(initiativesAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}

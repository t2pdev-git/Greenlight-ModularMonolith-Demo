using System.Reflection;
using Greenlight.Modules.Initiatives.Domain.Initiatives;
using Greenlight.Modules.Initiatives.Infrastructure;

namespace Greenlight.Modules.Initiatives.ArchitectureTests.Abstractions;


public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Initiatives.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Initiative).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(InitiativesModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Initiatives.Presentation.AssemblyReference).Assembly;
}

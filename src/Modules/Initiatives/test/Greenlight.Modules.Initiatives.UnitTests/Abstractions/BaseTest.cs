using Bogus;
using Greenlight.Common.Domain;
using Greenlight.Modules.Initiatives.Domain.Initiatives;

namespace Greenlight.Modules.Initiatives.UnitTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Faker Faker = new();

    public static T AssertDomainEventWasPublished<T>(Entity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} was not published");
        }

        return domainEvent;
    }

    public static void AssertDomainEventWasNotPublished<T>(Entity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

        if (domainEvent is not null)
        {
            throw new Exception($"{typeof(T).Name} was published");
        }
    }

}

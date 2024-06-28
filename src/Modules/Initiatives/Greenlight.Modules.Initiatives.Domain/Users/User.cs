using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Users;

public sealed class User : Entity
{
    public override Guid Id { get; protected set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    private User()
    {
    }

    public static User Create(Guid id, string email, string firstName, string lastName)
    {
        var user = new User
        {
            Id = id,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
        };

        return user;
    }

    public void Update(string firstName, string lastName)
    {
        if (FirstName == firstName && LastName == lastName)
        {
            return;
        }
        // :DLO:0 Need to support updating the user's email address. Also for the User Module and KeyCloak 
        FirstName = firstName;
        LastName = lastName;
    }
}

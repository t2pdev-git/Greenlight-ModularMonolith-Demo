﻿using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Initiatives.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName)
    : ICommand;

﻿using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;

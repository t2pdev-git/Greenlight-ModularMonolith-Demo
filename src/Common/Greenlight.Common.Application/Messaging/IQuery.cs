using Greenlight.Common.Domain;
using MediatR;

namespace Greenlight.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

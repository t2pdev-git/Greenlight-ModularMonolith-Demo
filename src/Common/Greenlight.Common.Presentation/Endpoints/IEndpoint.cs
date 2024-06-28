using Microsoft.AspNetCore.Routing;

namespace Greenlight.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

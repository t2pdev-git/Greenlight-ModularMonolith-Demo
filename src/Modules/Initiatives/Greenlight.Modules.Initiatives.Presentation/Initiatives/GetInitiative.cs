using Greenlight.Common.Domain;
using Greenlight.Common.Presentation.ApiResults;
using Greenlight.Common.Presentation.Endpoints;
using Greenlight.Modules.Initiatives.Application.Initiatives.GetInitiative;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Greenlight.Modules.Initiatives.Presentation.Initiatives;

internal sealed class GetInitiative : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("initiatives/{id}", async (Guid id, ISender sender) =>
            {
                Result<InitiativeResponse> result = await sender.Send(new GetInitiativeQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization(Permissions.GetInitiatives)
            .WithTags(Tags.Initiatives);
    }
}

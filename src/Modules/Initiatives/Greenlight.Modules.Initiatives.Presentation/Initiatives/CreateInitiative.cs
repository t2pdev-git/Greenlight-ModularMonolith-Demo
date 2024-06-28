using Greenlight.Common.Domain;
using Greenlight.Common.Presentation.ApiResults;
using Greenlight.Common.Presentation.Endpoints;
using Greenlight.Modules.Initiatives.Application.Initiatives.CreateInitiative;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Greenlight.Modules.Initiatives.Presentation.Initiatives;

internal sealed class CreateInitiative : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("initiatives", async (Request request, ISender sender) =>
            {
                var command = new CreateInitiativeCommand(
                    request.Title,
                    request.Description);

                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization(Permissions.ModifyInitiatives)
            .WithTags(Tags.Initiatives);
    }

    internal sealed class Request
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}

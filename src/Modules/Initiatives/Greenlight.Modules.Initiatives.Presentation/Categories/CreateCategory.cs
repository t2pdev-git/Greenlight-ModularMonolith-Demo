using Greenlight.Common.Domain;
using Greenlight.Common.Presentation.ApiResults;
using Greenlight.Common.Presentation.Endpoints;
using Greenlight.Modules.Initiatives.Application.Categories.CreateCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Greenlight.Modules.Initiatives.Presentation.Categories;

internal sealed class CreateCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", async (CreateCategoryRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateCategoryCommand(request.Name));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization(Permissions.ModifyCategories)
            .WithTags(Tags.Categories);
    }

    internal sealed class CreateCategoryRequest
    {
        public string Name { get; init; }
    }
}


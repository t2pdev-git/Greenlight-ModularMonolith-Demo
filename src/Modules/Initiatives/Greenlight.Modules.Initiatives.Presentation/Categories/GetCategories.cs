using Greenlight.Common.Domain;
using Greenlight.Common.Presentation.ApiResults;
using Greenlight.Common.Presentation.Endpoints;
using Greenlight.Modules.Initiatives.Application.Categories.GetCategories;
using Greenlight.Modules.Initiatives.Application.Categories.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Greenlight.Modules.Initiatives.Presentation.Categories;

internal sealed class GetCategories : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async (ISender sender) =>
            {
                Result<IReadOnlyCollection<CategoryResponse>> result = await sender.Send(new GetCategoriesQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization(Permissions.GetCategories)
            .WithTags(Tags.Categories);
    }
}

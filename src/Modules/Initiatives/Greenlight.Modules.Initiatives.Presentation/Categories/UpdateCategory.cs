using Greenlight.Common.Domain;
using Greenlight.Common.Presentation.ApiResults;
using Greenlight.Common.Presentation.Endpoints;
using Greenlight.Modules.Initiatives.Application.Categories.UpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Greenlight.Modules.Initiatives.Presentation.Categories;

internal sealed class UpdateCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("categories/{id}", async (Guid id, UpdateCategoryRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateCategoryCommand(id, request.Name));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            .RequireAuthorization(Permissions.ModifyCategories)
            .WithTags(Tags.Categories);
    }

    internal sealed class UpdateCategoryRequest
    {
        public string Name { get; init; }
    }
}

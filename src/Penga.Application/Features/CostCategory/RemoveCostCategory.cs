using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class RemoveCostCategory
    {
        public record RemoveCostCategoryRequest(int Id);

        public class Feature : IFeatureSlice
        {
            public static async Task<IResult> Handler(PengaDbContext pengaDbContext, [FromBody] RemoveCostCategoryRequest request)
            {
                var costCategory = pengaDbContext.CostCategories.Find(request.Id);
                if (costCategory == null)
                {
                    return Results.NotFound($"Cost category with id = {request.Id} not found");
                }

                pengaDbContext.CostCategories.Remove(costCategory);
                await pengaDbContext.SaveChangesAsync();

                return Results.Ok();
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapDelete("/cost-category", Handler)
                    .WithName("RemoveCostCategory")
                    .WithTags("Cost Categories");
            }
        }
    }
}

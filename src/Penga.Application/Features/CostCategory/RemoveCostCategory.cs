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
        public record RemoveCostCategoryResponse();

        public class Validator : AbstractValidator<RemoveCostCategoryRequest>
        {

        }

        public class Feature : IFeatureSlice
        {
            public static IResult Handler(PengaDbContext pengaDbContext, [FromBody] RemoveCostCategoryRequest request)
            {
                var costCategory = pengaDbContext.CostCategories.Find(request.Id);
                if (costCategory == null)
                {
                    return Results.NotFound($"Cost category with id = {request.Id} not found");
                }

                pengaDbContext.CostCategories.Remove(costCategory);
                pengaDbContext.SaveChanges();

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

using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using Penga.Domain;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class UpdateCostCategory
    {
        public record UpdateCostCategoryRequest(int Id, string Name);
        public record UpdateCostCategoryResponse();

        public class Validator : AbstractValidator<UpdateCostCategoryRequest>
        {

        }

        public class Feature : IFeatureSlice
        {
            public static IResult Handler(PengaDbContext pengaDbContext, [FromBody] UpdateCostCategoryRequest request)
            {
                var costCategory = pengaDbContext.CostCategories.Find(request.Id);
                if (costCategory == null)
                {
                    return Results.NotFound($"Cost category with id = {request.Id} not found");
                }

                costCategory.UpdateName(request.Name);
                pengaDbContext.SaveChanges();

                return Results.Ok();
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapPut("/cost-category", Handler)
                    .WithName("UpdateCostCategory")
                    .WithTags("Cost Categories");
            }
        }
    }
}

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
    public class AddCostCategory
    {
        public record AddCostCategoryRequest(string name);
        public record AddCostCategoryResponse();

        public class Validator : AbstractValidator<AddCostCategoryRequest>
        {

        }

        public class Feature : IFeatureSlice
        {
            public static IResult Handler(PengaDbContext pengaDbContext, [FromBody] AddCostCategoryRequest request)
            {
                var costCategory = new CostCategory(request.name);
                pengaDbContext.CostCategories.Add(costCategory);
                pengaDbContext.SaveChanges();

                return Results.Ok();
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapPost("/cost-category", Handler)
                    .WithName("AddCostCategory")
                    .WithTags("Cost Categories");
            }
        }
    }
}

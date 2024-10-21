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
        public record AddCostCategoryRequest(string Name);

        public class Validator : AbstractValidator<AddCostCategoryRequest>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(100);
            }
        }

        public class Feature : IFeatureSlice
        {
            public static async Task<IResult> Handler(PengaDbContext pengaDbContext, [FromBody] AddCostCategoryRequest request, IValidator<AddCostCategoryRequest> validator)
            {
                validator.ValidateAndThrow(request);

                var costCategory = new CostCategory(request.Name);
                await pengaDbContext.CostCategories.AddAsync(costCategory);
                await pengaDbContext.SaveChangesAsync();

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

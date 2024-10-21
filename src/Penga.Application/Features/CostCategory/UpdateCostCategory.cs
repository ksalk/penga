using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class UpdateCostCategory
    {
        public record UpdateCostCategoryRequest(int Id, string Name);

        public class Validator : AbstractValidator<UpdateCostCategoryRequest>
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
            public static async Task<IResult> Handler(PengaDbContext pengaDbContext, [FromBody] UpdateCostCategoryRequest request, IValidator<UpdateCostCategoryRequest> validator)
            {
                validator.ValidateAndThrow(request);

                var costCategory = pengaDbContext.CostCategories.Find(request.Id);
                if (costCategory == null)
                {
                    return Results.NotFound($"Cost category with id = {request.Id} not found");
                }

                costCategory.UpdateName(request.Name);
                await pengaDbContext.SaveChangesAsync();

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

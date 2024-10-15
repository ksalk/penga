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
    public class AddCost
    {
        public record AddCostRequest(string Name, string Description, DateOnly? Date, decimal Amount, int? CostCategoryId);

        public class Validator : AbstractValidator<AddCostRequest>
        {

        }

        public class Feature : IFeatureSlice
        {

            public static async Task<IResult> Handler(PengaDbContext pengaDbContext, [FromBody] AddCostRequest request)
            {
                var cost = new Cost(request.Name, request.Description, request.Date, request.Amount, request.CostCategoryId);
                await pengaDbContext.Costs.AddAsync(cost);
                await pengaDbContext.SaveChangesAsync();
                return Results.Ok();
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapPost("/cost", Handler)
                    .WithName("AddCost")
                    .WithTags("Costs");
            }
        }
    }
}

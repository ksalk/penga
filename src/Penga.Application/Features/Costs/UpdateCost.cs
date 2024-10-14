using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class UpdateCost
    {
        public record UpdateCostRequest(int Id, string Name, string Description, DateOnly? Date, decimal Amount, int? CostCategoryId);

        public class Validator : AbstractValidator<UpdateCostRequest>
        {

        }

        public class Feature : IFeatureSlice
        {

            public static IResult Handler(PengaDbContext pengaDbContext, [FromBody] UpdateCostRequest request)
            {
                var cost = pengaDbContext.Costs.Find(request.Id);
                if (cost == null)
                {
                    return Results.NotFound($"Cost with id = {request.Id} not found");
                }

                cost.Update(request.Name, request.Description, request.Date, request.Amount, request.CostCategoryId);
                pengaDbContext.SaveChanges();
                return Results.Ok();
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapPut("/cost", Handler)
                    .WithName("UpdateCost")
                    .WithTags("Costs");
            }
        }
    }
}

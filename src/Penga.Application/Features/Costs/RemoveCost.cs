using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class RemoveCost
    {
        public record RemoveCostRequest(int Id);

        public class Validator : AbstractValidator<RemoveCostRequest>
        {

        }

        public class Feature : IFeatureSlice
        {

            public static IResult Handler(PengaDbContext pengaDbContext, [FromBody] RemoveCostRequest request)
            {
                var cost = pengaDbContext.Costs.Find(request.Id);
                if (cost == null)
                {
                    return Results.NotFound($"Cost with id = {request.Id} not found");
                }
                pengaDbContext.Costs.Remove(cost);
                pengaDbContext.SaveChanges();
                return Results.Ok();
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapDelete("/cost", Handler)
                    .WithName("RemoveCost")
                    .WithTags("Costs");
            }
        }
    }
}

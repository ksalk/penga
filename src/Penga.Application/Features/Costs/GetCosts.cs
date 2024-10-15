using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class GetCosts
    {
        public record GetCostsResponse(int Id, string Name);

        public class Feature : IFeatureSlice
        {
            public static IResult Handler(PengaDbContext pengaDbContext)
            {
                var costs = pengaDbContext.Costs
                    .Select(x => new GetCostsResponse(x.Id, x.Name))
                    .ToArray();

                return Results.Ok();
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapGet("/cost", Handler)
                    .WithName("GetCosts")
                    .WithTags("Costs");
            }
        }
    }
}

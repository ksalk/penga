using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Penga.Contracts.Features;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class GetCosts
    {
        public record GetCostsResponse(int Id, string Name, string CategoryName);

        public class Feature : IFeatureSlice
        {
            public static IResult Handler(PengaDbContext pengaDbContext)
            {
                var costs = pengaDbContext.Costs
                    .Include(x => x.CostCategory)
                    .Select(x => new GetCostsResponse(x.Id, x.Name, x.CostCategory != null ? x.CostCategory.Name : string.Empty))
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

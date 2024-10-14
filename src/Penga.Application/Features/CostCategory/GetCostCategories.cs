using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class GetCostCategories
    {
        public record GetCostCategoriesResponse(int Id, string Name);

        public class Feature : IFeatureSlice
        {
            public static IResult Handler(PengaDbContext pengaDbContext)
            {
                var costCategories = pengaDbContext.CostCategories
                    .Select(x => new GetCostCategoriesResponse(x.Id, x.Name))
                    .ToArray();

                return Results.Ok(costCategories);
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapGet("/cost-category", Handler)
                    .WithName("GetCostCategories");
            }
        }
    }
}

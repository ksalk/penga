using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using Penga.Domain;
using Penga.Infrastructure;

namespace Penga.Application.Features.Costs
{
    public class AddCostCategory
    {
        public record Request();
        public record Response();

        public class Validator : AbstractValidator<Request>
        {

        }

        public class Feature : IFeatureSlice
        {
            public static IResult Handler(PengaDbContext pengaDbContext)
            {
                var costCategory = new CostCategory("Apteka");
                pengaDbContext.CostCategories.Add(costCategory);
                pengaDbContext.SaveChanges();

                return Results.Ok("Success");
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapPost("/cost-category", Handler)
                    .WithName("AddCostCategory");
            }
        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Penga.Contracts.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penga.Application.Features.Costs
{
    public class AddCost
    {
        public record Request();
        public record Response();

        public class Validator : AbstractValidator<Request>
        {

        }

        public class Feature : IFeatureSlice
        {

            public static IResult Handler()
            {
                return Results.Ok("Success");
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

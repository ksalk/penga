using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using System.Security.Claims;

namespace Penga.Application.Features.Costs
{
    public class SaveUserInfo
    {
        public record Request();
        public record Response();

        public class Validator : AbstractValidator<Request>
        {

        }

        public class Feature : IFeatureSlice
        {

            public static IResult Handler(HttpContext httpContext)
            {
                const string objectIdClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";

                var objectId = httpContext.User.FindFirst(objectIdClaimType)?.Value;
                var email = httpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var name = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                return Results.Ok("Success");
            }

            public IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder)
            {
                return routeBuilder.MapPost("/user", Handler)
                    .WithName("SaveUserInfo");
            }
        }
    }
}

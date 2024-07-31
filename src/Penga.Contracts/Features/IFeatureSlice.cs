using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Penga.Contracts.Features
{
    public interface IFeatureSlice
    {
        IEndpointConventionBuilder Register(IEndpointRouteBuilder routeBuilder);
    }
}

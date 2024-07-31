using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Penga.Contracts.Features;
using System.Reflection;

namespace Penga.Application
{
    public static class FeatureRegistrar
    {
        public static void Register(IEndpointRouteBuilder routeBuilder, Action<IEndpointConventionBuilder> endpointAction)
        {
            var featureInterfaceType = typeof(IFeatureSlice);
            var features = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => featureInterfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                .ToList();

            foreach (var feature in features)
            {
                var featureInstance = Activator.CreateInstance(feature) as IFeatureSlice;
                if (featureInstance == null)
                    throw new InvalidOperationException($"Could not instantiate feature type: {feature.FullName}");

                endpointAction(featureInstance.Register(routeBuilder));
            }
        }
    }
}

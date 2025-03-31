using Microsoft.Extensions.DependencyInjection;
using XmlConverter.Application.Contracts.Factories;
using XmlConverter.Application.Contracts.Strategies;
using XmlConverter.Persistance.Factories;
using XmlConverter.Persistance.Strategies;

namespace XmlConverter.Persistance
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileConvertionFactory, FileConvertionFactory>();
            services.AddScoped<IXmlToJsonConvertionStrategy, XmlToJsonConvertionStrategy>();
        }
    }
}

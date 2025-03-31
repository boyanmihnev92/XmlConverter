using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using XmlConverter.UI.Infrastructure.Refit.APIs;

namespace XmlConverter.UI.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddUiInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRefitClient<IXmlConvertApi>()
                .ConfigureHttpClient(v => v.BaseAddress = new Uri("https://localhost:7000"));
        }
    }
}

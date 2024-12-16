using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace core.services
{
    public class ConfigurationHelper
    {
        public static IConfiguration ResolveConfiguration(IHostEnvironment environment)
        {
            var reportingConfigFileName = System.IO.Path.Combine(environment.ContentRootPath, "reportingAppSettings.json");
            return new ConfigurationBuilder()
                .AddJsonFile(reportingConfigFileName, true)
                .Build();
        }
    }
}
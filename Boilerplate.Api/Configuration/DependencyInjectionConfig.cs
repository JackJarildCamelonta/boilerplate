using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.WebApi.Configuration
{
    /// <summary>
    /// Dependency injection configuration
    /// </summary>
    public class DependencyInjectionConfig
    {
        /// <summary>
        /// Register DI configuration
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="config">Configuration</param>
        public static void Register(IServiceCollection services, IConfiguration config)
        {
        }
    }
}

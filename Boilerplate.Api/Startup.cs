using AutoMapper;
using Boilerplate.WebApi.Configuration;
using Boilerplate.WebApi.Configuration.Swagger;
using Boilerplate.WebApi.Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Boilerplate.WebApi
{
    /// <summary>
    /// API Startup
    /// </summary>
    public class Startup
    {
        private readonly IHostEnvironment _env;

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the startup
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="env">Environment</param>
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        /// <summary>
        /// Application Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(options =>
            {
                //Don't cache as default
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
                //Use 
                options.CacheProfiles.Add("AppSpecific",
                    new CacheProfile()
                    {
                        Duration = 600 // 10 minutes
                    });

            });


            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add("alphanumeric", typeof(AlphaNumericConstraint));
            });

            services.AddApiVersioning(o => o.ReportApiVersions = true);

            // format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        
            if (!_env.IsProduction())
            {
                services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
                services.AddSwaggerGen();
            }

            DependencyInjectionConfig.Register(services, Configuration);

            //services.AddDbContext<ApplicationDbContext>(options =>
            //      options.UseSqlServer(
            //          Configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application</param>
        /// <param name="provider">API Version Description Provider</param>
        /// <param name="logger">Logger</param>
        public void Configure(
            IApplicationBuilder app, 
            IApiVersionDescriptionProvider provider,
            ILogger<Startup> logger)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            logger.LogInformation($"Environment {_env.EnvironmentName}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (!_env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.DocExpansion(DocExpansion.None);
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }
        }
    }
}

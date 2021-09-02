using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Store.Webservice.Application.Handlers;
using Store.Webservice.Application.Interfaces.Repository;
using Store.Webservice.Persistence.Context;
using Store.Webservice.Persistence.Repository;

namespace Store.Webservice.WebService
{
    /// <summary>
    /// Configures services and the application's request pipeline.
    /// </summary>
    public class Startup
    {
        private const string ApplicationName = "store";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. It's used to add services to the container.
        /// </summary>
        /// <param name="services">Collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add this assembly (Webservice.dll) because the mapping profile is in this project
            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly);

            // Add MediatR: The HandlerLoader is a dummy class to get the assembly info for the project with handlers
            services.AddMediatR(typeof(HandlerLoader).GetTypeInfo().Assembly);

            services.AddDbContext<StoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(Constants.AppSettings.StoreDatabase)));

            services.AddMemoryCache();

            services.AddCors(o => o.AddPolicy("Allow specific hosts", builder =>
            {
                builder.SetIsOriginAllowed(IsOriginAllowed)
                    .WithOrigins(Constants.CorsSettings.CorsOriginAllowed)
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Location")
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(1800));
            }));

            services.AddControllersWithViews().AddFluentValidation();

            AddSwaggerConfiguration(services, Constants.ProjectName, "Web API service", "v1", true);

            RegisterDependencies(services);
        }

        /// <summary>
        /// Validates whether the provided host can access our webservice (CORS).
        /// </summary>
        /// <param name="host">The accessing host.</param>
        /// <returns>A boolean indicating whether access is permitted.</returns>
        private static bool IsOriginAllowed(string host)
        {
            return Constants.CorsSettings.CorsOriginAllowed.Any(origin =>
                Regex.IsMatch(host, $@"^http(s)?://.*{origin}(:[0-9]+)?$", RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// This method gets called by the runtime. It's used to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="application">Configuration of the application's request pipeline.</param>
        /// <param name="env">Hosting environment information.</param>
        /// <param name = "mediator" > The mediator object.</param>
        public void Configure(IApplicationBuilder application, IWebHostEnvironment env, IMediator mediator)
        {
            if (mediator == null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            if (env.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                application.UseHsts();
            }

            application.UseRouting();

            AddSwagger(application, ApplicationName, Constants.ProjectName, Configuration[Constants.AppSettings.SwaggerFilePath]);

            application.UseStatusCodePages();
            application.UseHttpsRedirection();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            // Application Dependencies
            services.AddScoped<IUnitOfWork, UnitOfWork<StoreContext>>();
            services.AddScoped<IUnitOfWork<StoreContext>, UnitOfWork<StoreContext>>();
        }

        /// <summary>
        /// Enables Swagger.
        /// </summary>
        /// <param name="application">The application container.</param>
        /// <param name="applicationName">The application name in the URL, e.g. Omgevingen.</param>
        /// <param name="displayName">The Swagger page display name, e.g. Ssb.Services.Omgevingen.</param>
        /// <param name="swaggerFilePath">The Swagger file path, e.g. /swagger/index.html</param>
        /// <returns>A boolean indicating whether Swagger will be enabled, useful for health checks.</returns>
        public bool AddSwagger(IApplicationBuilder application, string applicationName, string displayName, string swaggerFilePath)
        {
            application.UseSwagger(c => { c.RouteTemplate = $"{applicationName}/{c.RouteTemplate}"; });
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{applicationName}{swaggerFilePath}",
                    $"{displayName}");
                c.RoutePrefix = $"{applicationName}/{c.RoutePrefix}";
            });

            return true;
        }

        /// <summary>
        /// Adds Swagger configuration to project.
        /// Enable it with Application.UseSwagger()
        /// </summary>
        /// <param name="services">The services collection of the requesting application.</param>
        /// <param name="projectName">The project name, e.g. Omgevingen Service.</param>
        /// <param name="projectDescription">The description of the API on which Swagger runs, e.g. v1.</param>
        /// <param name="version">The version of the API on which Swagger runs, e.g. v1.</param>
        /// <param name="secure">JWT security must be shown/used in HTTP requests.</param>
        public static void AddSwaggerConfiguration(IServiceCollection services, string projectName,
            string projectDescription, string version, bool secure)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"{version}",
                    new OpenApiInfo
                    {
                        Title = projectName,
                        Version = $"{version}",
                        Description = $"{projectDescription}"
                    });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{projectName}.xml"));
            });
        }
    }
}
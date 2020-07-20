using Webtrovert.API.Filters;
using Webtrovert.API.Services;
using Webtrovert.Application;
using Webtrovert.Application.Common.Interfaces;
using Webtrovert.Infrastructure;
using Webtrovert.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

namespace Webtrovert.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(_configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddControllersWithViews(options =>
                options.Filters.Add(new ApiExceptionFilter()));

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Webtrovert API";

                configure.AddSecurity("JWT Bearer token", new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Bearer {your JWT token}."
                });
                configure.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Bearer token"));

                configure.AddSecurity("OAuth2", new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        ClientCredentials = new OpenApiOAuthFlow()
                        {
                            // TODO: Move to config
                            AuthorizationUrl = "http://localhost:5000/connect/authorize",
                            TokenUrl = "http://localhost:5000/connect/token"
                        }
                    }
                });
                configure.OperationProcessors.Add(new OperationSecurityScopeProcessor("OAuth2"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseHealthChecks("/health");

            app.UseStaticFiles();

            app.UseSwaggerUi3(options =>
            {
                options.Path = "/api";
                options.DocumentPath = "/api/specification.json";
                options.OAuth2Client = new OAuth2ClientSettings()
                {
                    ClientId = "swagger",
                    ClientSecret = "Sw@ggerrr", // TODO: Move to config
                    AppName = "Webtrovert API - Swagger"
                };
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

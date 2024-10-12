using Asp.Versioning;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BtgPactual.Back.Api.Extensions;
using BtgPactual.Back.Core;
using BtgPactual.Back.Infrastructure;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace BtgPactual.Back.Api
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfiguration(Configuration);
            services.AddHttpContextAccessor();
            services.AddAuthorization();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
                options.Conventions.Add(new CamelCaseControllerNameConvention());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new CustomStringEnumConverter());
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BTG PACTUAL API", Version = "v1" });

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.EnableAnnotations();
                options.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddSwaggerGenNewtonsoftSupport();

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            }); ;

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutofac();

        }
    }
}

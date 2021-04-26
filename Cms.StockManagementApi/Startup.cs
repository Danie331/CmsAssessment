using Cms.Services.Core.AuthenticationService;
using Cms.Services.Core.StockManagementService;
using Cms.StockManagementApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cms.StockManagementApi
{
    public class Startup
    {
        private const string _corsDefault = "CorsDefault";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterAuthenticationService(Configuration);
            services.RegisterStockManagementService(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(_corsDefault, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddControllers()
                   .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
                   .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Secret"))),
                    ValidateIssuer = false,       // Defaults for test environment
                    ValidateAudience = false,     // Defaults for test environment
                    RequireExpirationTime = false,// Defaults for test environment
                    ValidateLifetime = false      // Defaults for test environment
                };
            });

            services.AddAutoMapper(GetType().Assembly, typeof(Data.DAL.Mapping.AutoMapperProfile).Assembly);

            services.AddSwaggerDocument(settings => settings.Title = "CMS Assessment");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(_corsDefault);

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

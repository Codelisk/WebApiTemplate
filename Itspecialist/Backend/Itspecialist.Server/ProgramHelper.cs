using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using Itspecialist.Database;
using Microsoft.EntityFrameworkCore;
using Itspecialist.Server.Extensions;
using Itspecialist.Foundation;

namespace Itspecialist.Server
{
    public static class ProgramHelper
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.AddProfile<DtoEntityProfile>();
                });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddAllServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureModules();
            services.AddIdentity();

            // Configure the RouteOptions to use lowercase URLs
            //services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddSwagger();
        }

        private static void ConfigureModules(this IServiceCollection services)
        {
            services.AddManager(); 
            services.AddRepositoryServices();
            services.AddRepositories();
        }

        public static void Configure(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.AddIdentity();

            //app.UseHttpsRedirection();

            //app.UseRouting();

            app.MapControllers();
            // app.UseAuthorization();

            //_ = app.UseEndpoints(endpoints =>
            //{
            //    _ = endpoints.MapControllers();
            //});


            //app.UseStaticFiles();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.Run();
        }

        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ItspecialistDbContext>(opt =>
            {
                opt.UseSqlite(connectionString, x => x.MigrationsAssembly("Itspecialist.Server"));
                //opt.UseSqlite(connectionString);
            });
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            // SwaggerGen setup
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt => {
                opt.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Name = "Bearer",
            In = ParameterLocation.Header,
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        },
        new List<string>()
    }
});
            });
        }
    }
}

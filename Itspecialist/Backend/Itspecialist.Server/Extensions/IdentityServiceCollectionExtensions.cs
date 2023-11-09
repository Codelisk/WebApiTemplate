using Itspecialist.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Itspecialist.Server.Extensions
{
    public static class IdentityServiceCollectionExtensions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<UserDto>()
                .AddEntityFrameworkStores<ItspecialistDbContext>()
                .AddApiEndpoints();

            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);
            services.AddAuthorizationBuilder();
        }

        public static void AddIdentity(this WebApplication app)
        {
            app.MapIdentityApi<UserDto>();
        }
    }
}

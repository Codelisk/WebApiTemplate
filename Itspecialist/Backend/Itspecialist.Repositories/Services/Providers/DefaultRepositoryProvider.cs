using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itspecialist.Foundation.Dtos.User;
using Microsoft.AspNetCore.Http;

namespace Itspecialist.Repositories.Services.Providers
{
    public record DefaultRepositoryProvider(ItspecialistDbContext PrintingContext, UserManager<UserDto> UserManager, IHttpContextAccessor HttpContextAccessor);
}

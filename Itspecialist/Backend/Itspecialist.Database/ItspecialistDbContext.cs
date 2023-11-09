using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Itspecialist.Foundation.Dtos.User;
using Codelisk.GeneratorAttributes.WebAttributes.Database;
using Microsoft.EntityFrameworkCore;

namespace Itspecialist.Database
{
    [BaseContext]
    public partial class ItspecialistDbContext : IdentityDbContext<UserDto, IdentityRole<Guid>, Guid>
    {
        public ItspecialistDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

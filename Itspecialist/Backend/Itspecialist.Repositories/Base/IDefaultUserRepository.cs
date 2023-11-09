using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itspecialist.Foundation.Dtos.Base;

namespace Itspecialist.Repositories.Base
{
    public interface IDefaultUserRepository<TEntity, TKey> : IDefaultRepository<TEntity, TKey> where TEntity : BaseUserDto
    {
    }
}

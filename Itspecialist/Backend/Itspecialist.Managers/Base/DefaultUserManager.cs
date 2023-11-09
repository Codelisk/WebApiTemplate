using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codelisk.GeneratorAttributes.WebAttributes.Dto;
using Codelisk.GeneratorAttributes.WebAttributes.Manager;

namespace Itspecialist.Managers.Base
{
    [DefaultManager]
    [UserDto]
    public class DefaultUserManager<TDto, TKey, TEntity> : DefaultManager<TDto, TKey, TEntity>, IDefaultUserManager<TDto, TKey, TEntity> where TDto : BaseUserDto where TEntity : BaseUserDto
    {
        public DefaultUserManager(IDefaultUserRepository<TEntity, TKey> Repo, IMapper mapper) : base(Repo, mapper)
        {
        }
    }
}

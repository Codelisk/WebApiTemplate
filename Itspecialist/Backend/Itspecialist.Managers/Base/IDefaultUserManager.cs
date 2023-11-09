using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itspecialist.Managers.Base
{
    public interface IDefaultUserManager<TDto, TKey, TEntity> : IDefaultManager<TDto, TKey, TEntity> where TDto : class where TEntity : class
    {
    }
}

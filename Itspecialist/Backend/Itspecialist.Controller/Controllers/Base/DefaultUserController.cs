

namespace Itspecialist.Controller.Controllers.Base
{
    [DefaultController]
    [UserDto]
    public class DefaultUserController<T, TKey, TEntity> : DefaultController<T, TKey, TEntity>
        where T : BaseDto
        where TEntity : class
        where TKey : IComparable
    {
        protected new readonly IDefaultUserManager<T, TKey, TEntity> _manager;
        public DefaultUserController(IDefaultUserManager<T, TKey, TEntity> manager) : base(manager)
        {
            _manager = manager;
        }
    }
}

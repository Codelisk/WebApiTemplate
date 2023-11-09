
namespace Itspecialist.Controller.Controllers.Base
{
    [DefaultController]
    [Authorize]
    [Route("[controller]")]
    [Produces("application/json")]
    public class DefaultController<T, TKey, TEntity> : Microsoft.AspNetCore.Mvc.Controller
        where T : BaseDto
        where TEntity : class
        where TKey : IComparable
    {
        protected readonly IDefaultManager<T, TKey, TEntity> _manager;

        public DefaultController(IDefaultManager<T, TKey, TEntity> manager)
        {
            _manager = manager;
        }
    }
}

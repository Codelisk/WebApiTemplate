

namespace Itspecialist.Repositories.Base
{
    [DefaultRepository]
    [UserDto]
    public class DefaultUserRepository<T, TKey> : DefaultRepository<T, TKey>, IDefaultUserRepository<T, TKey> where T : BaseUserDto
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly UserManager<UserDto> UserManager;
        public DefaultUserRepository(DefaultRepositoryProvider defaultRepositoryProvider) : base(defaultRepositoryProvider.PrintingContext)
        {
            HttpContextAccessor = defaultRepositoryProvider.HttpContextAccessor;
            UserManager = defaultRepositoryProvider.UserManager;
        }
        [Save]
        public override async Task<T> Save(T t)
        {
            t.UserId = GetUserIdGuid();
            return await base.Save(t);
        }
        [Get]
        public override async Task<T> Get(TKey id)
        {
            return await base.Get(id);
        }
        [GetAll]
        public override async Task<List<T>> GetAll()
        {
            var uid = GetUserId();
            var result = await base.GetAll();
            return result.Where(x => x.IsUser(uid)).ToList();
        }
        [Delete]
        public override async Task Delete(T t)
        {
            await base.Delete(t);
        }

        private TKey GetUserId()
        {
            string id = null;

            if (typeof(Guid) == typeof(TKey))
            {
                object parsedGuid = Guid.Parse(id);
                return (TKey)parsedGuid;
            }
            else
            {
                return (TKey)Convert.ChangeType(id, typeof(TKey));
            }
        }
        private Guid GetUserIdGuid()
        {
            object result = this.GetUserId();
            return (Guid)result;
        }
    }
}

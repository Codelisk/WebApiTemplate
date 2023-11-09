
namespace Itspecialist.Repositories.Base
{
    [DefaultRepository]
    public class DefaultRepository<T, TKey> : IDefaultRepository<T, TKey> where T : class
    {
        private readonly ItspecialistDbContext _context;
        public DefaultRepository(ItspecialistDbContext context)
        {
            _context = context;
        }
        [Save]
        public virtual async Task<T> Save(T t)
        {
            EntityEntry<T> result;
            result = _context.Add(t);

            await _context.SaveChangesAsync();
            return result.Entity;
        }
        [Get]
        public virtual async Task<T> Get(TKey id)
        {
            return await _context.FindAsync<T>(id);
        }
        [GetAll]
        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        [Delete]
        public virtual async Task Delete(T t)
        {
            _context.Remove(t);
            await _context.SaveChangesAsync();
        }
    }
}

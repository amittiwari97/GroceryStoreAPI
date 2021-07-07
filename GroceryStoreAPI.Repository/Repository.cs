using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected List<T> list = new List<T>();

        public Repository(DbContext context)
        {
            _context = context;
        }
        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }      

        public async Task<List<T>> GetAll()
        {
            var list = await _context.Set<T>().ToListAsync();
            return list;
        }

        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity, int id)
        {          
            T exist = await _context.Set<T>().FindAsync(id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return exist;
        }
    }
}

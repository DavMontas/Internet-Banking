using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly AppDbContext _db;
        public GenericRepository(AppDbContext db)
        {
            _db = db;
        }
        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _db.Set<Entity>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _db.Set<Entity>().Remove(entity);
            await _db.SaveChangesAsync();

        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _db.Set<Entity>().ToListAsync();
        }

        public virtual async Task<List<Entity>> GetAllWithIncludeAsync(List<string> props)
        {
            var query = _db.Set<Entity>().AsQueryable();

            foreach (string prop in props)
            {
                query = query.Include(prop);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _db.Set<Entity>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(Entity entity, int id)
        {
            Entity entry = await _db.Set<Entity>().FindAsync(id);
            _db.Entry(entry).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync();
        }
    }
}

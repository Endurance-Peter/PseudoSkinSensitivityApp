using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private DbSet<T> _dbContext;
        public Repository(DbContextStore dbContext)
        {
            _dbContext = dbContext.Set<T>();
        }
        public Task Add(T model)
        {
            _dbContext.Add(model);
            return Task.CompletedTask;
        }

        public Task Delete(T model)
        {
            _dbContext.Remove(model);

            return Task.CompletedTask;
        }

        public async Task<List<T>> FetchAll()
        {
            var results = _dbContext.AsQueryable();

            return await results.ToListAsync();
        }

        public async Task<T> FetchById(Guid id)
        {
            var results = await _dbContext.FindAsync(id);

            return results;
        }

        public Task Update(T model)
        {
            _dbContext.Update(model);

            return Task.CompletedTask;
        }
    }
}

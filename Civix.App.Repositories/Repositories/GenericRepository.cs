using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Civix.App.Core.Repositories.Contracts;
using Civix.App.Core.Specifications;
using Civix.App.Repositories.Data;
using Civix.App.Repositories.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Civix.App.Repositories.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly CivixDbContext _context;

        public GenericRepository(CivixDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await _context.Set<TEntity>().ToListAsync();
        }


        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }


        public async Task AddAsync(TEntity entity)
        {
           await  _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }


        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }



        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await IssueSpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await IssueSpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).CountAsync();
        }

        public async Task<TEntity?> GetEntityWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await IssueSpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }
    }
}

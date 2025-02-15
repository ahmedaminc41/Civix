using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core;
using Civix.App.Core.Entities;
using Civix.App.Core.Repositories.Contracts;
using Civix.App.Repositories.Data;
using Civix.App.Repositories.Repositories;

namespace Civix.App.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CivixDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(CivixDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();


        public void Dispose() => _context.DisposeAsync();


        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity, TKey>(_context);
                _repositories.Add(type,repository);
            }
                return _repositories[type] as IGenericRepository<TEntity, TKey>;
        }
    }
}

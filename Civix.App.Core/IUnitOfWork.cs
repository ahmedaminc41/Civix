using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Civix.App.Core.Repositories.Contracts;

namespace Civix.App.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync();
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;

    }
}

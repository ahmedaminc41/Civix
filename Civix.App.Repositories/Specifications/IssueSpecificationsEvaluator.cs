using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Civix.App.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Civix.App.Repositories.Specifications
{
    public static class IssueSpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery;

            // Apply Filtering Criteria
            if (spec.Criteira is not null)
            {
                query = query.Where(spec.Criteira);
            }

            // Apply Sorting (OrderBy & OrderByDescending)
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            // Apply Pagination
            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // Apply Includes (for related entities like Category)
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }

}

using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "<Pending>")]
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> sourceData, ISpecifications<TEntity> spec)
        {
            var finalQuery = sourceData;

            if (spec.Criteria != null)
            {
                finalQuery = finalQuery.Where(spec.Criteria);
            }

            if (spec.OrderByDescendingExpression != null)
            {
                finalQuery = finalQuery.OrderByDescending(spec.OrderByDescendingExpression);
            }

            if (spec.OrderByExpression != null)
            {
                finalQuery = finalQuery.OrderBy(spec.OrderByExpression);
            }

            if (spec.IsPaginationActivated)
            {
                finalQuery = finalQuery.Skip(spec.Skip).Take(spec.Take);
            }

            finalQuery = spec.Includes
                .Aggregate(finalQuery,
                    (currentQuery, currentIncludeExpression) => currentQuery.Include(currentIncludeExpression));

            return finalQuery;
        }
    }
}

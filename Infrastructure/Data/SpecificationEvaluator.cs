using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

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

            finalQuery = spec.Includes
                .Aggregate(finalQuery,
                    (currentQuery, currentIncludeExpression) => currentQuery.Include(currentIncludeExpression));

            return finalQuery;
        }
    }
}

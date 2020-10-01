using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByExpression { get; private set; }
        public Expression<Func<T, object>> OrderByDescendingExpression { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeProperty)
        {
            Includes.Add(includeProperty);
        }

        protected void AddOrderByDescendingExpression(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescendingExpression = orderByDescExpression;
        }

        protected void AddOrderByExpression(Expression<Func<T, object>> orderByExpression)
        {
            OrderByExpression = orderByExpression;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecifications<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get;  }

        Expression<Func<T, object>> OrderByExpression { get;  }

        Expression<Func<T, object>> OrderByDescendingExpression { get;  }

    }
}

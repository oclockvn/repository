using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace oclockvn.QueryExtenstions
{
    public static class QueryExtensionMethod
    {
        public static IQueryable<T> Including<T>(this IQueryable<T> self, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            return includeProperties.Aggregate(self, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}

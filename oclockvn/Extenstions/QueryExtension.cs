using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace oclockvn.Extenstions
{
    public static class QueryExtension
    {
        /// <summary>
        /// Includings multiple related items into query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public static IQueryable<T> Including<T>(this IQueryable<T> self, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            return includeProperties.Aggregate(self, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// Pagings the specified current query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="current">The current.</param>
        /// <param name="take">The take.</param>
        /// <returns>The IQueryable after paging</returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int current, int take) where T : class
        {
            if (current < 1)
                current = 1;

            var skip = (current - 1) * take;

            return query.Skip(skip).Take(take);
        }
    }
}

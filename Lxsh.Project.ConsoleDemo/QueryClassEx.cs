using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lxsh.Project.ConsoleDemo
{
    public  static class QueryClassEx
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query,bool flag, Expression<Func<T, bool>> expression)
        {

            return flag ? query.Where(expression) : query;
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, string flag, Expression<Func<T, bool>> expression)
        {

            return string.IsNullOrEmpty(flag) ? query : query.Where(expression);
        }
    }
}

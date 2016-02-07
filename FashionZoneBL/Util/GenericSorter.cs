using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace FashionZone.BL.Util
{
    public class GenericSorter<T, PT>
    {
        public IQueryable<T> Sort(IQueryable<T> source, string sortExpression, SortDirection sortDirection)
        {
            var param = Expression.Parameter(typeof(T), "item");

            var sortLambda = Expression.Lambda<Func<T, PT>>(Expression.Convert(Expression.Property(param, sortExpression), typeof(PT)), param);

            if (sortDirection == SortDirection.Ascending)
            {
                return source.OrderBy<T, PT>(sortLambda);
            }
            else
            {
                return source.OrderByDescending<T, PT>(sortLambda);
            }
        }
    }

    public class GenericSorterCaller<T>
    {
        public IQueryable<T> Sort(IQueryable<T> source, string sortExpression, SortDirection sortDirection)
        {
            PropertyInfo property = typeof(T).GetProperty(sortExpression);

            Type sorterType = typeof(GenericSorter<,>).MakeGenericType(typeof(T), property.PropertyType);
            object sorterObject = Activator.CreateInstance(sorterType);

            MethodInfo method = sorterType.GetMethod("Sort", new Type[] { typeof(IQueryable<T>), typeof(String), typeof(SortDirection) });

            return method.Invoke(sorterObject, new object[] { source, sortExpression, sortDirection }) as IQueryable<T>;
        }
    }
}

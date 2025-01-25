using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Dynamically applies ordering to an IQueryable based on a property name and direction.
        /// </summary>
        /// <typeparam name="T">The type of elements in the queryable.</typeparam>
        /// <param name="source">The source queryable to apply ordering to.</param>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="direction">The direction of sorting ("Ascending" or "Descending").</param>
        /// <returns>The queryable with the specified ordering applied.</returns>
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string propertyName, string direction)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
            }

            var type = typeof(T);
            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{type.Name}'.", nameof(propertyName));
            }

            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var methodName = direction.Equals("Descending", StringComparison.OrdinalIgnoreCase)
                ? "OrderByDescending"
                : "OrderBy";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression)
            );

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}

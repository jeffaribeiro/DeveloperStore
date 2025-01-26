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

        /// <summary>
        /// Dynamically applies filtering to an IQueryable based on query parameters.
        /// </summary>
        /// <typeparam name="T">The type of elements in the queryable.</typeparam>
        /// <param name="source">The source queryable to apply filtering to.</param>
        /// <param name="filters">A dictionary of filters where keys are property names and values are filter values.</param>
        /// <returns>The queryable with the specified filters applied.</returns>
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> source, IDictionary<string, string?> filters)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? predicate = null;

            foreach (var filter in filters)
            {
                var propertyPath = filter.Key; // Exemplo: "name.firstname"
                var filterValue = filter.Value;

                if (string.IsNullOrEmpty(filterValue))
                    continue;

                // Resolve a propriedade aninhada
                var propertyAccess = ResolvePropertyAccess(parameter, propertyPath);
                if (propertyAccess == null)
                    continue;

                Expression condition;

                // Partial matches (*)
                if (filterValue.Contains("*"))
                {
                    var value = filterValue.Replace("*", string.Empty);
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);

                    var propertyToLower = Expression.Call(propertyAccess, toLowerMethod!);
                    var valueExpression = Expression.Constant(value.ToLower());
                    condition = Expression.Call(propertyToLower, containsMethod!, valueExpression);
                }
                // Exact matches
                else
                {
                    var value = Convert.ChangeType(filterValue, propertyAccess.Type);
                    var valueExpression = Expression.Constant(value);
                    condition = Expression.Equal(propertyAccess, valueExpression);
                }

                // Combine conditions (AND logic)
                predicate = predicate == null ? condition : Expression.AndAlso(predicate, condition);
            }

            // Retorna todos os registros se não houver filtros válidos
            if (predicate == null)
                return source;

            var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
            return source.Where(lambda);
        }

        private static MemberExpression? ResolvePropertyAccess(Expression parameter, string propertyPath)
        {
            var properties = propertyPath.Split('.'); // Divide o caminho em partes (e.g., "name.firstname")
            Expression? propertyAccess = parameter;

            foreach (var propertyName in properties)
            {
                var type = propertyAccess.Type;
                var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                    return null; // Retorna nulo se a propriedade não existir

                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }

            return propertyAccess as MemberExpression;
        }
    }
}

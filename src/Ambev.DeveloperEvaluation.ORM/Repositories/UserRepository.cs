using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a paginated list of users from the repository, applying optional ordering.
    /// </summary>
    /// <param name="page">The page number for pagination (default: 1).</param>
    /// <param name="size">The number of items per page (default: 10).</param>
    /// <param name="order">
    /// A string specifying the ordering of results in the format "property direction" (e.g., "username asc, email desc").
    /// If null or empty, results are ordered by the default property "Id".
    /// </param>
    /// <returns>
    /// A tuple containing:
    /// - Users: A list of users matching the specified criteria.
    /// - TotalItems: The total number of users before pagination.
    /// </returns>
    public async Task<(IEnumerable<User> Users, int TotalItems)> ListUsersAsync(int page, int size, string? order)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(order))
        {
            var orderClauses = order.Split(',');
            foreach (var clause in orderClauses)
            {
                var trimmedClause = clause.Trim();
                var parts = trimmedClause.Split(' ');
                var propertyName = parts[0];
                var direction = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? "Descending"
                    : "Ascending";

                query = query.OrderByDynamic(propertyName, direction);
            }
        }
        else
        {
            query = query.OrderBy(u => u.Id);
        }

        var totalItems = await query.CountAsync();

        var users = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return (users, totalItems);
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<User> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }

    /// <summary>
    /// Updates an existing user in the database
    /// </summary>
    /// <param name="user">The user with updated values</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user if found and updated, null otherwise</returns>
    public async Task<User?> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}

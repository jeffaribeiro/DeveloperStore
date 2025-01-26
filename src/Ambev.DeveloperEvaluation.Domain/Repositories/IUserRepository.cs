using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates a new user in the repository
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);

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
    Task<(IEnumerable<User> Users, int TotalItems)> ListUsersAsync(int page, int size, string? order, IDictionary<string, string?> filters);

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user from the repository
    /// </summary>
    /// <param name="user">The user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user was deleted</returns>
    Task<User> DeleteAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user in the database
    /// </summary>
    /// <param name="user">The user with updated values</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user if found and updated, null otherwise</returns>
    Task<User?> UpdateAsync(User user, CancellationToken cancellationToken = default);
}

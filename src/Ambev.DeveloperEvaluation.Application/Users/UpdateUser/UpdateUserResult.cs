using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

public class UpdateUserResult
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's username
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The user's password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// The user's name
    /// </summary>
    public Name Name { get; set; }

    /// <summary>
    /// The user's address
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the user
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public UserRole Role { get; set; }
}

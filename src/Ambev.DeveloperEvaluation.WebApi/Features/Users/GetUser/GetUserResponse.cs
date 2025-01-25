using Ambev.DeveloperEvaluation.WebApi.Responses;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// API response model for GetUser operation
/// </summary>
public class GetUserResponse
{
    /// <summary>
    /// The unique identifier of the created user
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
    public NameResponse Name { get; set; }

    /// <summary>
    /// The user's address
    /// </summary>
    public AddressResponse Address { get; set; }

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the user
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public string Role { get; set; } = string.Empty;
}

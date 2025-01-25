using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersResult
{
    public IEnumerable<User> Users { get; set; }
    public int TotalItems { get; set; }
}

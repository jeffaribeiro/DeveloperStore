using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Query to retrieve a paginated list of users
    /// </summary>
    public class ListUsersQuery : IRequest<ListUsersResult>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? Order { get; set; }
    }
}

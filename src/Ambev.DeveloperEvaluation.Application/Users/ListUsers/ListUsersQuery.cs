using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Query to retrieve a paginated list of users
    /// </summary>
    public class ListUsersQuery : IRequest<ListUsersResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Order { get; set; }
        public IDictionary<string, string?> Filters { get; set; } = new Dictionary<string, string?>();
    }
}

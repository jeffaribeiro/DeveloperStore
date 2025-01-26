using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    /// <summary>
    /// Request to retrieve a paginated list of users
    /// </summary>
    public class ListUsersRequest
    {
        private int _page = 1;
        private int _size = 10;

        /// <summary>
        /// The page number for pagination
        /// </summary>
        public int Page
        {
            get => _page;
            set => _page = value <= _page ? _page : value;
        }

        /// <summary>
        /// The size of the page for pagination
        /// </summary>
        public int Size
        {
            get => _size;
            set => _size = value <= _size ? _size : value;
        }

        /// <summary>
        /// The ordering criteria
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        /// Filters to apply to the query as key-value pairs.
        /// </summary>
        [ModelBinder(BinderType = typeof(FiltersModelBinder))]
        public IDictionary<string, string?> Filters { get; set; } = new Dictionary<string, string?>();
    }
}

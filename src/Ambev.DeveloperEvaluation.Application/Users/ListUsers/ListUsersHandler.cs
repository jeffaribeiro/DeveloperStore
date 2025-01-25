using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Handles the ListUsersQuery to retrieve a paginated list of users
    /// </summary>
    public class ListUsersHandler : IRequestHandler<ListUsersQuery, ListUsersResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ListUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ListUsersResult> Handle(ListUsersQuery query, CancellationToken cancellationToken)
        {
            var validator = new ListUsersValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var (users, totalItems) = await _userRepository.ListUsersAsync(query.Page, query.Size, query.Order);

            return _mapper.Map<ListUsersResult>((users, totalItems));
        }
    }
}

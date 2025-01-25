﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    /// <summary>
    /// Validator for ListUsersRequest
    /// </summary>
    public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
    {
        public ListUsersRequestValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0.");

            RuleFor(x => x.Size).InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100.");
            
            RuleFor(x => x.Order).Matches(@"^[a-zA-Z0-9_,\s]+$").When(x => !string.IsNullOrEmpty(x.Order))
                .WithMessage("Order contains invalid characters.");
        }
    }
}

using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UpdateUserResult?> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUserWithId = await _userRepository.GetByIdAsync(command.Id, cancellationToken);

        if (existingUserWithId == null)
            throw new InvalidOperationException($"User with id {command.Id} not exists");

        var existingUserWithEmail = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (existingUserWithEmail != null && existingUserWithEmail.Id != command.Id)
            throw new InvalidOperationException($"User with email {command.Email} already exists");

        existingUserWithId.Username = command.Username;
        existingUserWithId.Email = command.Email;
        existingUserWithId.Password = _passwordHasher.HashPassword(command.Password);
        existingUserWithId.Name = command.Name;
        existingUserWithId.Address = command.Address;
        existingUserWithId.Phone = command.Phone;
        existingUserWithId.Status = command.Status;
        existingUserWithId.Role = command.Role;

        var updatedUser = await _userRepository.UpdateAsync(existingUserWithId, cancellationToken);

        var result = _mapper.Map<UpdateUserResult>(updatedUser);

        return result;
    }
}

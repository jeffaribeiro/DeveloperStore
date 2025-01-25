using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands
/// </summary>
public class DeleteUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteUser feature
    /// </summary>
    public DeleteUserProfile()
    {
        CreateMap<Guid, DeleteUserCommand>()
            .ConstructUsing(id => new DeleteUserCommand(id));

        CreateMap<DeleteUserResult, DeleteUserResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameResponse { Firstname = src.Name.FirstName, Lastname = src.Name.LastName }))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressResponse
            {
                City = src.Address.City,
                Street = src.Address.Street,
                Number = src.Address.Number,
                ZipCode = src.Address.ZipCode,
                Geolocation = new GeolocationResponse { Lat = src.Address.Geolocation.Latitude, Long = src.Address.Geolocation.Longitude }
            }))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}

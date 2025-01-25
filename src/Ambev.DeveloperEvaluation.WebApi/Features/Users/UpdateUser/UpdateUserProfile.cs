using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserRequest, UpdateUserCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name(src.Name.FirstName, src.Name.LastName)))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(
                src.Address.City,
                src.Address.Street,
                src.Address.Number,
                src.Address.ZipCode,
                new Geolocation(src.Address.Geolocation.Latitude, src.Address.Geolocation.Longitude)
            )))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        CreateMap<UpdateUserResult, UpdateUserResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameResponse
            {
                Firstname = src.Name.FirstName,
                Lastname = src.Name.LastName
            }))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressResponse
            {
                City = src.Address.City,
                Street = src.Address.Street,
                Number = src.Address.Number,
                ZipCode = src.Address.ZipCode,
                Geolocation = new GeolocationResponse
                {
                    Lat = src.Address.Geolocation.Latitude,
                    Long = src.Address.Geolocation.Longitude
                }
            }))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}

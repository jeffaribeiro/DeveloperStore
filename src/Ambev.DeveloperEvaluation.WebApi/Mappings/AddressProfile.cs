using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressResponse>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.Geolocation, opt => opt.MapFrom(src => src.Geolocation));
    }
}
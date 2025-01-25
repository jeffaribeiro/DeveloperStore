using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class GeolocationProfile : Profile
{
    public GeolocationProfile()
    {
        CreateMap<Geolocation, GeolocationResponse>()
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Longitude));
    }
}

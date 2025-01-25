using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class NameProfile : Profile
{
    public NameProfile()
    {
        CreateMap<Name, NameResponse>()
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName));
    }
}
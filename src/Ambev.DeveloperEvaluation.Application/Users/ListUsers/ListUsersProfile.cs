using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Profile for mapping entities and DTOs related to ListUsers
    /// </summary>
    public class ListUsersProfile : Profile
    {
        public ListUsersProfile()
        {
            CreateMap<(IEnumerable<User> Users, int TotalItems), ListUsersResult>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems));
        }
    }
}
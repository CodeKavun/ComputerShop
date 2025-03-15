using AutoMapper;
using ComputerShopApp.Models.DTO.Roles;
using Microsoft.AspNetCore.Identity;

namespace ComputerShopApp.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using ComputerShopApp.Data;
using ComputerShopApp.Models.DTO.Users;

namespace ComputerShopApp.Profiles
{
    public class ShopUserProfile : Profile
    {
        public ShopUserProfile()
        {
            CreateMap<ShopUser, ShopUserDTO>().ReverseMap();
        }
    }
}

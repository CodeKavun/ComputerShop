using AutoMapper;
using ComputerShopApp.Models.DTO.Shop;
using ComputerShopDomainLibrary;

namespace ComputerShopApp.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDTO>().ReverseMap();
        }
    }
}

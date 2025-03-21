using AutoMapper;
using ComputerShopApp.Models.DTO.Shop;
using ComputerShopDomainLibrary;

namespace ComputerShopApp.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}

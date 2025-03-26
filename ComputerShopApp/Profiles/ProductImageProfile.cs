using AutoMapper;
using ComputerShopApp.Models.DTO.Shop;
using ComputerShopDomainLibrary;

namespace ComputerShopApp.Profiles
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
        }
    }
}

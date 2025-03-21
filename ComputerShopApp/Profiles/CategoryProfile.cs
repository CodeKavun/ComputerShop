using AutoMapper;
using ComputerShopApp.Models.DTO.Shop;
using ComputerShopDomainLibrary;

namespace ComputerShopApp.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}

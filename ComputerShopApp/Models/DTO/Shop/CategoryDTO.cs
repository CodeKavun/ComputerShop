using ComputerShopDomainLibrary;
using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.DTO.Shop
{
    public class CategoryDTO
    {
        //public int Id { get; set; }
        //[Display(Name = "Category Name")]
        //public string Name { get; set; } = default!;
        //public int? ParentCategoryId { get; set; }
        //[Display(Name = "Parent Category")]
        //public CategoryDTO? ParentCategoryDTO { get; set; }
        //[Display(Name = "Subcategories")]
        //public ICollection<Category> ChildCategories { get; set; } = default!;
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? ParentCategoryId { get; set; }
        public CategoryDTO? ParentCategory { get; set; }
        public ICollection<CategoryDTO>? ChildCategories { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}

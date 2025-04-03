using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerShopApp.Data;
using ComputerShopDomainLibrary;
using ComputerShopApp.Models.ViewModels.Shop;

namespace ComputerShopApp.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly ShopContext _context;

        public ProductImagesController(ShopContext context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductImages.Include(item => item.Product).ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: ProductImages/Create
        public async Task<IActionResult> Create(int? selectedCategoryId, int? selectedBrandId)
        {
            IQueryable<Product> products = _context.Products;

            if (selectedCategoryId != null) products = products.Where(p => p.CategoryId == selectedCategoryId);
            if (selectedBrandId != null) products = products.Where(p => p.BrandId == selectedBrandId);

            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.ToListAsync();

            CreateImageViewModel viewModel = new CreateImageViewModel
            {
                BrandsList = new SelectList(brands, "Id", nameof(Brand.Name), selectedBrandId),
                CategoriesList = new SelectList(categories, "Id", nameof(Category.Name), selectedCategoryId),
                SelectedBrandId = selectedBrandId,
                SelectedCategoryId = selectedCategoryId,
                ProductsList = new SelectList(products, "Id", nameof(Product.Name))
            };

            return View(viewModel);
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (IFormFile file in viewModel.Images)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);

                        ProductImage image = new ProductImage
                        {
                            ImageData = ms.ToArray(),
                            ProductId = viewModel.SelectedProductId
                        };

                        _context.ProductImages.Add(image);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            IQueryable<Product> products = _context.Products;

            if (viewModel.SelectedCategoryId != null) products = products.Where(p => p.CategoryId == viewModel.SelectedCategoryId);
            if (viewModel.SelectedBrandId != null) products = products.Where(p => p.BrandId == viewModel.SelectedBrandId);

            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.ToListAsync();

            //CreateImageViewModel viewModel = new CreateImageViewModel
            //{
            viewModel.BrandsList = new SelectList(brands, "Id", nameof(Brand.Name), viewModel.SelectedBrandId);
            viewModel.CategoriesList = new SelectList(categories, "Id", nameof(Category.Name), viewModel.SelectedCategoryId);
            viewModel.SelectedBrandId = viewModel.SelectedBrandId;
            viewModel.SelectedCategoryId = viewModel.SelectedCategoryId;
            viewModel.ProductsList = new SelectList(products, "Id", nameof(Product.Name));
            //};

            return View(viewModel);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageData,PeoductId")] ProductImage productImage)
        {
            if (id != productImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImageExists(productImage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage != null)
            {
                _context.ProductImages.Remove(productImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetProducts(int? brandId, int? categoryId)
        {
            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.ToListAsync();
            IQueryable<Product> products = _context.Products;

            if (categoryId != null) products = products.Where(p => p.CategoryId == categoryId);
            if (brandId != null) products = products.Where(p => p.BrandId == brandId);

            CreateImageViewModel viewModel = new CreateImageViewModel
            {
                BrandsList = new SelectList(brands, "Id", "Name", brandId),
                CategoriesList = new SelectList(categories, "Id", "Name", categoryId),
                SelectedBrandId = brandId,
                SelectedCategoryId = categoryId,
                ProductsList = new SelectList(products, "Id", "Name")
            };

            return PartialView("_SelectProductBlock", viewModel);
        }
    }
}

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
using AutoMapper;

namespace ComputerShopApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ShopContext _context;
        private readonly IMapper mapper;

        public CategoriesController(ShopContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Categories.Include(c => c.ParentCategory);
            return View(await shopContext.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.Where(c => c.ParentCategoryId == null).ToListAsync();

            CreateCategoryViewModel viewModel = new CreateCategoryViewModel
            {
                ParentCategories = new SelectList(categories, "Id", "Name")
            };

            return View(viewModel);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel viewModel, int?[] parentCategoryId)
        {
            if (ModelState.IsValid)
            {
                parentCategoryId = parentCategoryId.Where(item => item.HasValue).ToArray();

                Category category = mapper.Map<Category>(viewModel.CategoryDTO);

                if (parentCategoryId.Length > 0)
                    category.ParentCategoryId = parentCategoryId[parentCategoryId.Length - 1];

                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            viewModel.ParentCategories = new SelectList(_context.Categories, "Id", "Name", viewModel.CategoryDTO.ParentCategoryId);
            return View(viewModel);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Name", category.ParentCategoryId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ParentCategoryId")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Name", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetChildCategories(int? id)
        {
            if (id == null) return NotFound();

            var categories = await _context.Categories.Where(c => c.ParentCategoryId == id).ToListAsync();
            if (!categories.Any()) return NotFound();

            return PartialView("_ChildCategories", categories);
        }
    }
}

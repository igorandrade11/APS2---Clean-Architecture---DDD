using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service) => _service = service;

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create() => View(new CategoryCreateUpdateDto());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();
            var dto = new CategoryCreateUpdateDto { Name = category.Name, Description = category.Description };
            ViewBag.Id = id;
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoryCreateUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(dto);
            }

            await _service.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
    }
}

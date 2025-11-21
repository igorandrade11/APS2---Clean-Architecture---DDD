using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services;

namespace ProductManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string q)
        {
            var results = await _productService.SearchAsync(q);
            return PartialView("_ProductListPartial", results);
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategories();
            return View(new CreateProductDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto createDto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategories();
                return View(createDto);
            }

            try
            {
                await _productService.CreateAsync(createDto);
                TempData["Success"] = "Produto criado com sucesso!";
                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await PopulateCategories();
                return View(createDto);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var updateDto = new UpdateProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId
            };

            await PopulateCategories();
            ViewBag.ProductId = id;
            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateProductDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategories();
                ViewBag.ProductId = id;
                return View(updateDto);
            }

            try
            {
                await _productService.UpdateAsync(id, updateDto);
                TempData["Success"] = "Produto atualizado com sucesso!";
                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await PopulateCategories();
                ViewBag.ProductId = id;
                return View(updateDto);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                TempData["Success"] = "Produto excluído com sucesso!";
                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Products");
            }
        }

        private async Task PopulateCategories()
        {
            var cats = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(cats, "Id", "Name");
        }
    }
}

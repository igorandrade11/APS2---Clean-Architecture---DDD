using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services;

namespace ProductManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", createDto);
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
                return View("Create", createDto);
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
                Category = product.Category
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateProductDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", updateDto);
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
                return View("Edit", updateDto);
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
    }
}

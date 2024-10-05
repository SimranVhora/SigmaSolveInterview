using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.DataAccess.Repository.IRepository;
using ProductManagement.Models.Entities;

namespace ProductManagement.Controllers;
public class ProductController(IRepository<Product> _productRepository,IRepository<Category> _categoryRepository) : Controller
{
	public async Task<IActionResult> Index()
	{
        var products = await _productRepository.GetAllAsync(includeProperties: "Category");
        return View(modal);
	}
	[HttpGet]
	public async Task<IActionResult> CreateProduct()
	{
        var categories = await _categoryRepository.GetAllAsync();
        ViewBag.Categories = categories.Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(), 
            Text = c.CategoryName 
        }).ToList(); 
        return View();
	}
	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] Product product)
	{
		if (ModelState.IsValid)
		{
			_productRepository.AddAsync(product);
			TempData["SuccessMessage"] = "Product Created successfully";
			return RedirectToAction("Index");
		}
		return View(product);
	}
	public async Task<IActionResult> UpdateProduct( [FromBody] Product product)
	{
		await _productRepository.UpdateAsync(product);
		return NoContent();
	}
	public async Task<IActionResult> DeleteProduct(int id)
	{
		if (id == 0)
        {
            return NotFound();
        }
        var category = await _productRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        await _productRepository.DeleteAsync(id);
        TempData["SuccessMessage"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}


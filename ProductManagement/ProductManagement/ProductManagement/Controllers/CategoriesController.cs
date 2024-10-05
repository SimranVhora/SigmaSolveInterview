using Microsoft.AspNetCore.Mvc;
using ProductManagement.DataAccess.Repository;
using ProductManagement.DataAccess.Repository.IRepository;
using ProductManagement.Models.Entities;

namespace ProductManagement.Controllers;
public class CategoriesController(IRepository<Category> _categoryRepository) : Controller
{

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Await the result of the async method
        var categories = await _categoryRepository.GetAllAsync();
        return View(categories);  // Now passing the actual list of categories to the view
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Category modal)
    {
        
        if (ModelState.IsValid)
        {
            //var category = _categoryRepository.GetByIdAsync(modal.CategoryId);
            //if (category.Result == null)
            //{
                await _categoryRepository.AddAsync(modal);
                TempData["SuccessMessage"] = "Category Created successfully";
                return RedirectToAction("Index");
            //}
            //else
            //{
            //    TempData["ErrorMessage"] = "Category already exist";
            //}
        }
        return View(modal);
    }
    [HttpGet]
    public async Task<IActionResult> UpdateCategory(int id)
    {
        var modal= await _categoryRepository.GetByIdAsync(id);
        return View(modal);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(Category modal)
    {
        if (ModelState.IsValid)
        {
            var category = await _categoryRepository.GetByIdAsync(modal.CategoryId);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not exist";
               
            }
            else
            {
                await _categoryRepository.UpdateAsync(modal);
                TempData["SuccessMessage"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
        }
        return View(modal);
    }
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        await _categoryRepository.DeleteAsync(id);
        TempData["SuccessMessage"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}


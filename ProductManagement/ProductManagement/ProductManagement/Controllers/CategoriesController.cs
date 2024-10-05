using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models.Entities;
using ProductManagement.DataAccess.Repository.IRepository;

namespace ProductManagement.Controllers;
public class CategoriesController : Controller
{
	private readonly IRepository<Category> _categoryRepository;

	public CategoriesController(IRepository<Category> categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
	{
		var categories = await _categoryRepository.GetAllAsync();
		return Ok(categories);
	}

	[HttpPost]
	public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
	{
		await _categoryRepository.AddAsync(category);
		return CreatedAtAction(nameof(GetCategories), new { id = category.CategoryID }, category);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
	{
		if (id != category.CategoryID)
			return BadRequest();

		await _categoryRepository.UpdateAsync(category);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCategory(int id)
	{
		await _categoryRepository.DeleteAsync(id);
		return NoContent();
	}
}


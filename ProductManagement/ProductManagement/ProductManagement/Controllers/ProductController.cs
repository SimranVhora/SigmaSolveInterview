using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models.Entities;

namespace ProductManagement.Controllers;
[Route("api/[controller]")]
public class ProductController : Controller
{
	[HttpGet]
	private readonly IRepository<Product> _productRepository;
	private readonly IRepository<Category> _categoryRepository;

	public ProductsController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
	{
		_productRepository = productRepository;
		_categoryRepository = categoryRepository;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
	{
		var products = await _productRepository.GetAllAsync();
		return Ok(products);
	}

	[HttpPost]
	public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
	{
		await _productRepository.AddAsync(product);
		return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId }, product);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
	{
		if (id != product.ProductId)
			return BadRequest();

		await _productRepository.UpdateAsync(product);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		await _productRepository.DeleteAsync(id);
		return NoContent();
	}
}


using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProductManagement.Models.Entities;
public class Category
{
	[ValidateNever]
	public int CategoryId { get; set; }
	public string CategoryName { get; set; }
	public bool IsActive { get; set; }
    [ValidateNever]
    public ICollection<Product> Products { get; set; }
}


namespace ProductManagement.Models.Entities;
public class Category
{
	public int CategoryId { get; set; }
	public string CategoryName { get; set; }
	public bool IsActive { get; set; }

	public ICollection<Product> Products { get; set; }
}


using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models.Entities;

public class Product
{
	public int ProductId { get; set; }

	[MaxLength(256)]
	public string Name { get; set; }

	[MaxLength(6)]
	public string SKU { get; set; }

	public int CategoryId { get; set; }
	public Category Category { get; set; }

	[Range(0, Double.MaxValue)]
	public decimal BasePrice { get; set; }

	[Range(0, Double.MaxValue)]
	public decimal MRP { get; set; }

	public string Description { get; set; }

	public CurrencyType Currency { get; set; }

	public DateTime ManufacturedDate { get; set; }

	public DateTime ExpireDate { get; set; }
}
public enum CurrencyType
{
	USD,
	EUR,
	INR
}


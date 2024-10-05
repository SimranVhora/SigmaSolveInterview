using Microsoft.EntityFrameworkCore;
using ProductManagement.Models.Entities;

namespace ProductManagement.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>()
			.HasIndex(p => p.SKU)
			.IsUnique();

		modelBuilder.Entity<Category>()
			.HasMany(c => c.Products)
			.WithOne(p => p.Category)
			.HasForeignKey(p => p.CategoryId);
	}
}


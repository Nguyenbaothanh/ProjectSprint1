using Microsoft.EntityFrameworkCore;
using Shopping.Models;

namespace Shopping.Repository
{
	public class DataContex : DbContext
	{
		public DataContex(DbContextOptions<DbContext> options) :base(options)
		{

		}
		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CateGoryModel> CateGories { get; set; }

	}
}

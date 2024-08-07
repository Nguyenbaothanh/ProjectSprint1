using Microsoft.EntityFrameworkCore;
using Shopping.Models;

namespace Shopping.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CateGoryModel macbook =new CateGoryModel {Name= "Macbook", Slug = "macbook", Description = "Macbook is Large Brand in the world", Status = 1 };
				CateGoryModel pc = new CateGoryModel { Name = "Pc", Slug = "pc", Description = "Pc is Large Brand in the world", Status = 1 };

				BrandModel apple = new BrandModel { Name = "Apple", Slug = "apple", Description = "Apple is Large Brand in the world", Status = 1 };
				BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "Samsung", Description = "Samsung is Large Brand in the world", Status = 1 };
				_context.Products.AddRange(
					new ProductModel { Name = "Macbook", Slug = "macbook", Description = "Màn hình Liquid Retina XDR 14 inch²\r\nHai cổng Thunderbolt / USB 4, cổng HDMI, khe thẻ nhớ SDXC, jack cắm tai nghe, cổng MagSafe 3\r\nMagic Keyboard với Touch ID\r\nBàn di chuột Force Touch\r\nBộ Tiếp Hợp Nguồn USB-C 70W", Image = "1.jpg", CateGory = macbook, Brand = apple, Price = 1200 },
					new ProductModel { Name = "Pc", Slug = "pc", Description = "Pc is Best", Image = "1.jpg", CateGory = pc, Brand = samsung, Price = 500 }
				);
				_context.SaveChanges();
			}
				


			
			{

			}
		}
	}
}

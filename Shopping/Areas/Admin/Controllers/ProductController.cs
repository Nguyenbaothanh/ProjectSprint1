using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Repository;

namespace Shopping.Areas.Admin.Controllers
{

	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
		{
			_dataContext = context;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<IActionResult> Index()
		{

			return View(await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.CateGory).Include(p => p.Brand).ToListAsync());
		}
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_dataContext.CateGories, "Id", "Name");
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductModel product)
		{
			ViewBag.Categories = new SelectList(_dataContext.CateGories, "Id", "Name", product.CategoryId);
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
			if (ModelState.IsValid)
			{
				product.Slug = product.Name.Replace(" ", "-");
				var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
				if (slug != null)
				{
					ModelState.AddModelError("", "Sản phẩm đã có trong database");
					return View(product);
				}
				else
				{
					if(product.ImageUpload != null)
					{
						string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath,"media/products");
						string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
						string filePath = Path.Combine(uploadsDir, imageName);

						FileStream fs = new FileStream(filePath, FileMode.Create);
						await product.ImageUpload.CopyToAsync(fs);
						fs.Close();
					}
				}
				//TempData["success"] = "Model đã ổn";// code them du lieu
			}
			else
			{
				TempData["error"] = "Model có một vài thứ đang bị lỗi";
				List<string> errors = new List<string>();
				foreach(var value in ModelState.Values)
				{
					foreach(var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}
				string errorMessage = string.Join("\n", errors);
				return BadRequest(errorMessage);
			}
			
			return View(product);
		}

		//[Route("Edit")]
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(ProductModel product)
		//{
		//	ViewBag.Categories = new SelectList(_dataContext.CateGories, "Id", "Name", product.CategoryId);
		//	ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

		//	var existed_product = _dataContext.Products.Find(product.Id);

		//}
	} 
}

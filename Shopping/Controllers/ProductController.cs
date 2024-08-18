using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Repository;

namespace Shopping.Controllers
{
	public class ProductController : Controller
	{
		private readonly DataContext _dataContext;
		public ProductController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
		{
			return View();
		}
        public async Task<IActionResult> Search(string searchTerm)
        {
            var products = await _dataContext.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToArrayAsync();

            ViewBag.Keyword = searchTerm;

            return View(products);
        }
        public async Task<IActionResult> Details(int Id)
		{
			if(Id == null) return RedirectToAction("Index");
			var productsById = _dataContext.Products.Where(p => p.Id == Id).FirstOrDefault();
			return View(productsById);
		}
	}
}

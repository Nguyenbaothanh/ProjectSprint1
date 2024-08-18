using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Repository;

namespace Shopping.Controllers
{
    public class CategoryController : Controller 
    {   
        private readonly DataContext _dataContext;
        public CategoryController(DataContext context) 
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index(string Slug = "")
        {
            CategoryModel cateGory = _dataContext.CateGories.Where(c => c.Slug == Slug).FirstOrDefault();
            if (cateGory == null) return RedirectToAction("Index");
            var productsByCategory = _dataContext.Products.Where(c => c.CategoryId == cateGory.Id);
            return View(await productsByCategory.OrderByDescending(p => p.CategoryId).ToListAsync());
            return View();
        }
    }
}

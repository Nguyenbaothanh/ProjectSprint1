using Microsoft.AspNetCore.Mvc;

namespace Shopping.Controllers
{
    public class CateGoryController : Controller 
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

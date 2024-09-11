using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.CodeAnalysis.Differencing;

namespace Shopping_Tutorial.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/AppRoles")]
    //[Authorize(Roles = "Admin,Publisher")]
    public class AppRolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
			_roleManager = roleManager;
		}
		//List all the roles created by Users
		[Route("Index")]
		public IActionResult Index()
		{
			var roles = _roleManager.Roles;
			return View(roles);
		}
		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			return View();
		}
       
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(IdentityRole model)
		{
			//avoid duplicate role
			if(!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult()) { 
				_roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
			}
			return Redirect("Index");
		}
        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(); // Handle missing Id
            }
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }
        [HttpPost]
        [Route("Edit")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityRole model)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(); // Handle missing Id
            }

            if (ModelState.IsValid) // Validate model state before proceeding
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                {
                    return NotFound(); // Handle role not found
                }

                role.Name = model.Name; // Update role properties with model data

                try
                {
                    await _roleManager.UpdateAsync(role);
                    TempData["success"] = "Role updated successfully!";
                    return RedirectToAction("Index"); // Redirect to the index action
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the role.");
                }
            }

            // If model is invalid or role not found, return the view with the model (or an empty model for a new role)
            return View(model ?? new IdentityRole { Id = id }); // Pre-populate Id or provide an empty model
        }
        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(); // Handle missing Id
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound(); // Handle role not found
            }

            try
            {
                await _roleManager.DeleteAsync(role);
                TempData["success"] = "Role deleted successfully!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the role.");
            }

            return Redirect("Index");
        }
    }
}

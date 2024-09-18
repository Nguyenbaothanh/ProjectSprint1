using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;
using Shopping_Tutorial.Repository;

namespace Shopping_Tutorial.Areas.Admin.Controllers
{

    [Route("Admin/User")]
    //[Authorize(Roles ="Admin,Publisher")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
      
        public UserController(UserManager<AppUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
           
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync(); // Get all roles asynchronously
            ViewBag.Roles = new SelectList(roles, "Id", "Name"); // Create SelectList for roles
            return View(new AppUserModel()); // Return view with empty user model
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(AppUserModel user)
        {
            if (ModelState.IsValid)
            {
                var createUserResult = await _userManager.CreateAsync(user, user.PasswordHash);
                if (createUserResult.Succeeded)
                {
                    // User created successfully
                    // Get the newly created user's ID
                    var createdUser = await _userManager.FindByEmailAsync(user.Email); // Assuming email is unique
                    var userId = createdUser.Id;
                    var role = _roleManager.FindByIdAsync(user.RoleId);
                   
                   // Add user to role using user ID and role ID
                   var addToRoleResult = await _userManager.AddToRoleAsync(createdUser, role.Result.Name);
                    if (!addToRoleResult.Succeeded)
                    {
                        // Handle adding user to role failure (e.g., add errors to ModelState)
                        foreach (var error in addToRoleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    
                        
                    }

                    TempData["success"] = "User created successfully.";
                    return RedirectToAction("Index"); // Redirect to user list view
                }
                else
                {
                    foreach (var error in createUserResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            var roles = await _roleManager.Roles.ToListAsync(); // Get roles again (in case of errors)
            ViewBag.Roles = new SelectList(roles, "Id", "Name"); // Recreate SelectList
            return View(user); // Return the view with validation errors (if any)
        }
        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(); // Handle missing ID
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // Handle user not found
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                // Handle deletion errors (e.g., add error messages to TempData)
                return View("Error"); // Or redirect to an error page
            }

            // User deleted successfully
            TempData["success"] = "User deleted successfully.";
            return RedirectToAction("Index"); // Redirect to user list view
        }
    }
}

using Cabinet.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cabinet.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> login(login model)
		{
            if(ModelState.IsValid)
            {
                // var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
                var user = await _userManager.FindByEmailAsync(model.Username);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false,lockoutOnFailure:false);


				if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
				}
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login Attempt");
                    return View(model);
                }
			}
			return View(model);

		}

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(Signup Signup)
        {
            var user = new ApplicationUser { UserName = Signup.username, Email = Signup.email, fullname = Signup.fullname };
            var result = await _userManager.CreateAsync(user,Signup.password);
            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
				return RedirectToAction("index", "Home");
			}
            //
            return View(Signup);
        }

        public void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty,error.Description);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
			return RedirectToAction("login", "account");

		}

		public async Task<IActionResult> AssignUserRole(string userId)
		{
			// Find the user by their user ID
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				// User not found, handle accordingly
				return NotFound();
			}
             var doctorRole = await _roleManager.FindByNameAsync("Doctor");
			if (doctorRole == null)
			{
				// Role does not exist, create it
				doctorRole = new IdentityRole("Doctor");
				var createResult = await _roleManager.CreateAsync(doctorRole);
				if (!createResult.Succeeded)
				{
					// Failed to create the role, handle accordingly
					return BadRequest(createResult .Errors);
				}
			}
			// Assign the "Doctor" role to the user
			var result = await _userManager.AddToRoleAsync(user, "Doctor");
			if (result.Succeeded)
			{
				// Role assigned successfully
				return Ok("Role assigned to user");
			}
			else
			{
				// Failed to assign the role
				return BadRequest(result.Errors);
			}
		}
	}
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductCatelogPL.Models;
using System.Data;

namespace ProductCatelogPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public readonly IMapper Imapper;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager,
            RoleManager<IdentityRole> _roleManager, IMapper _Imapper)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            this.roleManager = _roleManager;
            this.Imapper = _Imapper;
        }

        //register
        public IActionResult Register()
        {
            UserVM uservm = new UserVM();
            return View(uservm);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser();
                  user = Imapper.Map<IdentityUser>(model);
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);

        }
        //if email is unique or not
        public async Task<bool> checkEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
                return false;
            return true;
        }
        public async Task<bool> checkEmailLogin(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            return true;
        }

        public async Task<bool> checkUserName(string UserName)
        {
            var user = await userManager.FindByNameAsync(UserName);
            if (user != null)
                return false;
            return true;
        }
        //login


        public IActionResult LogIn(string ReturnUrl = "~/Home/Index")
        {
            ViewData["redirect"] = ReturnUrl;
            LogInVM logInVM = new LogInVM();
            return View(logInVM);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM model, string ReturnUrl = "~/Home/Index")
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await userManager.CheckPasswordAsync(user, model.Password);
                    if (result)
                    {
                        var logInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                        if (logInResult.Succeeded)
                            return LocalRedirect(ReturnUrl);
                        else
                            ModelState.AddModelError("", "username and password are not correct");
                        return View(model);
                    }
                }

            }
            return View(model);

        }
        //signout
        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }

        //add role 
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            RoleVM roleVM = new RoleVM();
            return View(roleVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = roleVM.name
                };
                var result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return Content("succeded");
                }
                else
                {
                    return BadRequest();
                }
            }
            return View(roleVM);
        }
        //add admin
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            UserVM uservm = new UserVM();
            return View("Register", uservm);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAdmin(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser();
                user = Imapper.Map<IdentityUser>(model);
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);

        }
    }
}

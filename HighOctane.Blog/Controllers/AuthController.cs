using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HighOctane.Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HighOctane.Blog.Controllers
{
    public class AuthController : Controller
    {

        SignInManager<IdentityUser> signInManager { get; set; }

        public AuthController( SignInManager<IdentityUser> sigInManager ) 
        {
            this.signInManager = sigInManager;
        }


        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel loginViewModel )
        {
            if (ModelState.IsValid)
            {
                var signedIn = await
                    signInManager
                        .PasswordSignInAsync(
                            loginViewModel.UserName,
                            loginViewModel.Password,
                            false,
                            false);
                if (signedIn.Succeeded)
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ValidationResult"] = "Validation failed.";
                ViewData["ValidationResultColor"] = "red";
            }
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
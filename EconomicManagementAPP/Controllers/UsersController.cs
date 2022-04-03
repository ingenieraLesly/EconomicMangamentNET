using EconomicManagementAPP.Models;
using EconomicManagementAPP.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EconomicManagementAPP.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepositorieUsers repositorieUsers;
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;

        public UsersController(IRepositorieUsers repositorieUsers,
                               UserManager<Users> userManager,
                               SignInManager<Users> signInManager)
        {
            this.repositorieUsers = repositorieUsers;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(Users user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var userExist =
               await repositorieUsers.Exist(user.Email);

            if (userExist)
            {
                ModelState.AddModelError(nameof(user.Email),
                    $"Email already use.");

                return View(user);
            }

            var result = await userManager.CreateAsync(user, password: user.Password);
            if(result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Transactions");
            }

            return View(user);
        }

        //Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var user = await repositorieUsers.GetUserById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> Modify(Users user)
        {
            var userExists = await repositorieUsers.GetUserById(user.Id);

            if (userExists is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUsers.Modify(user);
            return RedirectToAction("Index");
        }

        //Permite que usuarios no identificados puedan ejecutar le metodo
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {   
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user) 
        {
            if(!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, user.Remember, lockoutOnFailure: false);
            
            if(!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Wrong Email or Password");
                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login");
        }
    }
}

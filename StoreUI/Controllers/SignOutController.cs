using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StoreUI.Controllers
{
    public sealed class SignOutController : Controller
    {
        private readonly SignInManager<Customer> _signInManager;
        public SignOutController(SignInManager<Customer> signInManager) 
        {
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

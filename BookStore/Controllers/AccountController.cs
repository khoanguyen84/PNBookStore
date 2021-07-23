using BookStore.Entities;
using BookStore.Models.Account;
using BookStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Login(login);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                ViewBag.Error = result.Message;
                return View();
            }
            return View();
        }

        public IActionResult Singout()
        {
            userService.Signout();
            return RedirectToAction("Login", "Account");
        }
    }
}

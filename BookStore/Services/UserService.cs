using BookStore.Entities;
using BookStore.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly SignInManager<AppIdentityUser> signInManager;

        public UserService(UserManager<AppIdentityUser> userManager,
                            SignInManager<AppIdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<LoginResult> Login(Login LoginUser)
        {
            var user = await userManager.FindByNameAsync(LoginUser.Email);
            if(user == null)
            {
                return new LoginResult()
                {
                    UserId = string.Empty,
                    Email = string.Empty,
                    Message = "User is not existing."
                };
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, LoginUser.Password, LoginUser.RememberMe, false);
            if (signInResult.Succeeded)
            {
                return new LoginResult()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Message = "Login successed"
                };
            }
            return new LoginResult()
            {
                UserId = string.Empty,
                Email = string.Empty,
                Message = "Something went wrong, please try again later."
            };
        }

        public void Signout()
        {
            signInManager.SignOutAsync().Wait();
        }
    }
}

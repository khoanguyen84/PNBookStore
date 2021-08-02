using BookStore.Entities;
using BookStore.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IUserService
    {
        Task<LoginResult> Login(Login LoginUser);

        void Signout();
        Task<RegisterResult> Register(Register register);
    }
}

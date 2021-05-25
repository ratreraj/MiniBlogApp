
using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Entities;
using MiniBlogApp.Services.Interfaces;
using MiniBlogApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
       
        private IAuthenticationService _authServices;

        public AccountController(IAuthenticationService authServices)
        {
            _authServices = authServices;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SingUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SingUp(UserModel model)
        {
            User user = new()
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            bool res = _authServices.CreateUser(user, model.Password);
            if (res)
               return  RedirectToAction("AccountCreated");
            return View();
        }

        public IActionResult AccountCreated()
        {
            return View();
        }
    }
}

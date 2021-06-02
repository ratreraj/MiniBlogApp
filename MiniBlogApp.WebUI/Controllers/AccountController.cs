
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiniBlogApp.Entities;
using MiniBlogApp.Services.Interfaces;
using MiniBlogApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly HttpClient client;
        //private IAuthenticationService _authServices;

        public AccountController(IConfiguration config)
        {
            _config = config;
            client = new HttpClient();
            Uri uri = new Uri(_config["apiAddress"]);
            client.BaseAddress = uri;

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
            string strData = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

            var response = client.PostAsync(client.BaseAddress + "/Account/CreateAccount", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("AccountCreated");
            }


            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            //var user = _authService.AuthenticateUser(model.Email, model.Password);

            string strData = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

            var response = client.PostAsync(client.BaseAddress + "/Account/Login", content).Result;
            if (response.IsSuccessStatusCode)
            {

                string result = response.Content.ReadAsStringAsync().Result;
                User user = JsonSerializer.Deserialize<User>(result);


                if (user != null)
                {
                    //if (!string.IsNullOrEmpty(returnUrl))
                    //{
                    //    return Redirect(returnUrl);
                    //}

                    if (user.Roles.Contains("ADMIN"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    else if (user.Roles.Contains("USER"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "User" });
                    }
                }
            }


            return View();
        }

        public IActionResult AccountCreated()
        {
            return View();
        }
    }
}

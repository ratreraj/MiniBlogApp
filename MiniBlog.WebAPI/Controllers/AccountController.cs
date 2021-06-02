using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Entities;
using MiniBlogApp.Services.Interfaces;
using MiniBlogApp.WebAPI.Models;
using System;

namespace MiniBlog.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAuthenticationService _authService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authService = authenticationService;
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _authService.AuthenticateUser(model.Email, model.Password);


                    return StatusCode(StatusCodes.Status200OK, user);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception)
            {

                throw;
            }


        }
        [HttpPost]
        public IActionResult CreateAccount([FromBody] UserModel model)
        {
            try
            {
                User user = new User
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                bool result = _authService.CreateUser(user, model.Password);

                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}

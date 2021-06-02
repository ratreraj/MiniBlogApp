using Microsoft.AspNetCore.Identity;
using MiniBlogApp.Entities;
using MiniBlogApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlogApp.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        protected SignInManager<User> _singInManager;
        protected UserManager<User> _userManager;
        protected RoleManager<Role> _roleManager;

        public AuthenticationService(SignInManager<User> singInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _singInManager = singInManager;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public User AuthenticateUser(string Username, string Password)
        {

            //lockoutOnFailure default value 5 times 
            // false remeber me value 
            var result = _singInManager.PasswordSignInAsync(Username, Password, false, lockoutOnFailure: false).Result;
            if (result.Succeeded)
            {

                var user = _userManager.FindByNameAsync(Username).Result;
                var role = _userManager.GetRolesAsync(user).Result;
                user.Roles = role.ToArray();
                return user;
            }
            return null;
        }

        // User Manger for singup proccess
        public bool CreateUser(User user, string Password)
        {
            var result = _userManager.CreateAsync(user, Password).Result;
            if (result.Succeeded)
            {
                //Admin ,User
                string role = "User";
                var res = _userManager.AddToRoleAsync(user, role).Result;
                if (res.Succeeded)
                {

                    return true;
                }

            }
            return false;
        }

        public User GetUser(string Username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SingOut()
        {
            throw new NotImplementedException();
        }
    }
}

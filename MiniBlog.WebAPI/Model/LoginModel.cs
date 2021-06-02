using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MiniBlogApp.WebAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="please enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter password")]
        public string Password { get; set; }
    }
}

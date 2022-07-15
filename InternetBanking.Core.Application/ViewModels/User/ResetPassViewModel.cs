using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.User
{
    public class ResetPassViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Token is required")]
        [DataType(DataType.Text)]
        public string Token { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool HasError { get; set; }

        public string Error { get; set; }

    }
}

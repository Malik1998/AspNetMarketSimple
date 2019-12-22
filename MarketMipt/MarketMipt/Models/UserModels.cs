using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsAuthApp.Models
{
    public class LoginModel
    {
        [Required]
        public string login { get; set; }

        [Required]
        public bool is_admin { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public string login { get; set; }

        [Required]
        public bool is_admin { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }

    public class ViewModel
    {
        [Required]
        public string login { get; set; }

        [Required]
        public bool is_admin { get; set; }

        [Required]
        public List<string> products { get; set; }

        [Required]
        public List<string> products_ordered { get; set; }
    }
}
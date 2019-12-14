using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsAuthApp.Models
{
    public class CreatingProductModel
    {
        [Required]
        public string login { get; set; }

        [Required]
        public bool is_admin { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    public class ProductOderModel
    {

        [Required]
        public int count { get; set; }

        [Required]
        public int id { get; set; }

    }

    public class ViewProductModel
    {
        [Required]
        public string product { get; set; }

        [Required]
        public int count { get; set; }

        [Required]
        public int id { get; set; }
    }
}
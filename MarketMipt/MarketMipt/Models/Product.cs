using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MarketMipt.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }

        public string image { get; set; }

    //    public HttpPostedFileBase Image_base { get; set; }
    }

    public class ProductOder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int id_product { get; set; }

        public string user { get; set; }
        public int count { get; set; }

        public bool is_odered { get; set; }
    }

    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) :
            base(options)
        {
          //  Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Products> Products { get; set; }
    }

    public class ProductOderContext : DbContext
    {
        public ProductOderContext(DbContextOptions<ProductOderContext> options) :
            base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ProductOder> ProductOders { get; set; }
    }
}

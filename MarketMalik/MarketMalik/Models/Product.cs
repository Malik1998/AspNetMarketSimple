using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketMalik.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
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
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
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

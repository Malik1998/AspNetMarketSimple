using System;
using System.Linq;
using System.Threading.Tasks;
using MarketMipt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace MarketMipt.Controllers
{
    public class ProductsController : Controller
    {
        private const string V = "name = = == = = = =";
        private ProductContext db;
        private ProductOderContext db_oder;
        private UserContext db_user;

        public ProductsController(ProductContext db_, UserContext us, ProductOderContext oder_)
        {
            db = db_;
            db_user = us;
            db_oder = oder_;
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {


            return View(db.Products.FirstOrDefault(p => p.id == id));
        }

        // GET: Products/Create
        [HttpGet]
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Products product)
        {

            System.Diagnostics.Debug.WriteLine(V);

            try
            {
                if (db_user.Users.FirstOrDefault(u => u.login == HttpContext.User.Identity.Name).is_admin)
                {                 // TODO: Add insert logic here

                    var files = HttpContext.Request.Form.Files;

                    string image_name = "";
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {

                            var file = Image;
                            var uploads = Path.Combine("wwwroot", "img\\");

                            if (file.Length > 0)
                            {
                                var fileName = ContentDispositionHeaderValue.Parse
                                    (file.ContentDisposition).FileName.Trim('"');

                                System.Console.WriteLine(fileName);
                                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    image_name = Path.Combine("img\\", file.FileName);
                                }


                            }
                        }
                    }

                    System.Diagnostics.Debug.WriteLine(V + image_name);
                    db.Products.Add(new Products { id = product.id, name = product.name, count = product.count, image = image_name });
                    db.SaveChanges();
                    ModelState.AddModelError("", "Продукт создан");
                }
                return View(product);
            }
            catch
            {
                ModelState.AddModelError("", "Продукт " + product.id + " не может быть создан");
                return View(product);
            }
        }

        private static async Task NewMethod(IFormFile formFile, FileStream stream)
        {
            await formFile.CopyToAsync(stream);
        }

        [HttpGet]
        public ActionResult Oder(int id)
        {
            ProductOder prod = new ProductOder();
            prod.user = User.Identity.Name;
            prod.id_product = id;
            return View(prod);
        }

        [HttpPost]
        public ActionResult Oder(ProductOder product)
        {

            try
            {
                if (db.Products.FirstOrDefault(u => u.id == product.id_product) != null && 
                    db.Products.FirstOrDefault(u => u.id == product.id_product).count >= product.count)
                {              

                    db.Products.FirstOrDefault(u => u.id == product.id_product).count -= product.count;
                    db_oder.Add(new ProductOder { count = product.count,  id_product = product.id_product, user = product.user, is_odered = false });
                    db_oder.SaveChanges();
                    db.SaveChanges();
                    ModelState.AddModelError("", "Продукт заказан");
                } else {
                    ModelState.AddModelError("", "Продукт " + product.id_product + " не может быть заказан, не хватает товара");
                }
                return View(product);
            }
            catch
            {
                ModelState.AddModelError("", "Продукт " + product.id + " не может быть заказан");
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult OderLast()
        {
            db_oder.ProductOders.Where(p => p.user == User.Identity.Name).ToList().ForEach(p => p.is_odered = true);
            /*
            foreach (ProductOder oder in )
            {                    oder.is_odered = true;
                }
            }
            */
            db_oder.SaveChanges();
            
            return RedirectToAction("ViewModel", "Account");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
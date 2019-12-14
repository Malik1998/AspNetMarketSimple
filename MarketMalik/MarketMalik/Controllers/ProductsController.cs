using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketMalik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketMalik.Controllers
{
    public class ProductsController : Controller
    {
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            try
            {
                if (db_user.Users.FirstOrDefault(u => u.login == HttpContext.User.Identity.Name).is_admin) {                 // TODO: Add insert logic here
                    db.Products.Add(new Product { id = product.id, name = product.name, count = product.count });
                    db.SaveChanges();
                    ModelState.AddModelError("", "Продукт создан");
                }
                return View(product);
            }
            catch
            {
                ModelState.AddModelError("", "Продукт " + product.id +" не может быть создан");
                return View(product);
            }
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
                {                 // TODO: Add insert logic here

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
            foreach(ProductOder oder in db_oder.ProductOders)
            {
                if (oder.user == User.Identity.Name)
                {
                    oder.is_odered = true;
                }
            }
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
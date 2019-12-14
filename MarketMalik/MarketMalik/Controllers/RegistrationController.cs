using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FormsAuthApp.Models;
using MarketMalik.Models;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;

namespace MarketMalik.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db;
        private ProductOderContext db_oder;
        private ProductContext db_products;

        public AccountController(UserContext db_, ProductOderContext oder, ProductContext prod)
        {
            db = db_;
            db_oder = oder;
            db_products = prod;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> LoginAsync(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                user = db.Users.FirstOrDefault(u => u.login == model.login && u.password == model.password);

                if (user != null)
                {

                    await AuthentificateAsync(user.login);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }
        
        public ActionResult ViewModel()
        {
            ViewModel model = new ViewModel();

            User user = null;
            user = db.Users.FirstOrDefault(u => u.login == HttpContext.User.Identity.Name);

            if (user != null)
            {
                model.login = user.login;
                model.is_admin = user.is_admin;
                model.products = new List<string>();
                model.products_ordered = new List<string>();
                foreach (ProductOder oder in db_oder.ProductOders)
                {
                    if (oder.user == user.login)
                    {
                        Product product = db_products.Products.FirstOrDefault(prop => prop.id == oder.id_product);
                        if (product != null && !oder.is_odered)
                        {
                            model.products.Add(product.name + " " + oder.count);
                        } else if (product != null)
                        {
                            model.products_ordered.Add(product.name + " " + oder.count);
                        }
                    }
                }
            }

            return View(model);
        }

        private async System.Threading.Tasks.Task AuthentificateAsync(string login)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                user = db.Users.FirstOrDefault(u => u.login == model.login);

                if (user == null)
                {
                    // создаем нового пользователя
                    db.Users.Add(new User { login = model.login, password = model.password, is_admin = model.is_admin });
                    db.SaveChanges();

                    user = db.Users.Where(u => u.login == model.login && u.password == model.password).FirstOrDefault();
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        await AuthentificateAsync(user.login);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> LogoffAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
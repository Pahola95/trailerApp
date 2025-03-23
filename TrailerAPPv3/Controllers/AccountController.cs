using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TrailerAPPv3.Models;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace TrailerAPPv3.Controllers
{
    public class AccountController : Controller
    {
        private trailers_dbEntities3 db = new trailers_dbEntities3();


        public AccountController()
        {
           
        }

        // Register User
        [HttpGet]
        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            Console.WriteLine(ModelState.Values);
            Console.WriteLine(model.ToString());
            if (!ModelState.IsValid) return View(model);

            var user = new user_system
            {
                email = model.Email,
                password=(model.Password),
                name = model.Name,
                role_id = 2 // Default role (User)
            };
            user.id = Guid.NewGuid();
            db.user_system.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        // Login User
        [HttpGet]
        public ActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await db.user_system.Include(u=>u.role).FirstOrDefaultAsync(u => u.email == model.Email);
            if (user != null && (Regex.Replace(user.password, @"\s", "") == model.Password))
            {
                System.Web.HttpContext.Current.Session["UserId"] = user.id;
                System.Web.HttpContext.Current.Session["Role"] = user.role.id;
                return RedirectToAction("GeolocationMap", "Home");
            }

            ViewBag.Message = "Credenciales incorrectas";
            return View(model);
        }

        // Logout
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
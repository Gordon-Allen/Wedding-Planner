using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Google.Maps;
using Google.Maps.StaticMaps;
using Google.Maps.Geocoding;

namespace WeddingPlanner.Controllers
{
    public class UserController : Controller
    {

        private MyContext dbContext;

        public UserController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Session") == null)
            {
                ViewBag.IsLoggedIn = false;
                return View("Index");
            }
            else
            {
                ViewBag.IsLoggedIn = true;
                return RedirectToAction("Dashboard");
            }
        }

        [HttpPost("User/Register")]
        public IActionResult register(User user)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserID", user.UserId);
                HttpContext.Session.SetString("Session", "True");
                return RedirectToAction("Dashboard");
            }
            return View("Index");
            // other code
        }

        [HttpPost("User/Login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if (ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
                if (userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index");
                }

                var hasher = new PasswordHasher<LoginUser>();

                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);

                if (result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserID", userInDb.UserId);
                HttpContext.Session.SetString("Session", "True");
                return RedirectToAction("Dashboard");
            }
            return View("Index");
        }

        [HttpGet("User/Dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Session") == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Userid = HttpContext.Session.GetInt32("UserID");
                ViewBag.AllWed = dbContext.Weddings
                .Include(c => c.WeddingtoUser)
                .ToList();
                return View();
            }
        }


        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
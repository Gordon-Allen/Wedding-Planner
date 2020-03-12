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
    public class UserWeddingViewModelController : Controller
    {

        private MyContext dbContext;

        public UserWeddingViewModelController(MyContext context)
        {
            dbContext = context;
        }

        [Route("addrsvp/{wedid}/{userid}")]
        [HttpGet]
        public IActionResult AddRsvp(int wedid, int userid)
        {
            Wedding newWed = dbContext.Weddings.Include(c => c.WeddingtoUser).ThenInclude(b => b.User).FirstOrDefault(wed => wed.WeddingId == wedid);
            User newUser = dbContext.Users.Include(c => c.UsertoWedding).ThenInclude(b => b.Wedding).FirstOrDefault(us => us.UserId == userid);
            foreach (var thiswed in newUser.UsertoWedding)
            {
                if (thiswed.Wedding.WeddingDate.Date == newWed.WeddingDate.Date)
                {
                    ModelState.AddModelError("WeddingDate", "You have plan to go to another wedding on that day already!!!");
                    ViewBag.sameDayWedding = true;
                    return Redirect("/User/Dashboard");
                }
            }
            ViewBag.sameDayWedding = false;
            UserWeddingViewModel a = new UserWeddingViewModel();
            a.WeddingId = wedid;
            a.UserId = userid;
            dbContext.Add(a);
            dbContext.SaveChanges();
            return RedirectToAction("DetailWed", "Wedding", new { id = wedid });
        }

        [Route("unrsvp/{id}")]
        [HttpGet]
        public IActionResult UnRsvp(int id)
        {
            UserWeddingViewModel a = dbContext.UserWeddingViewModels.FirstOrDefault(wed => wed.UserWeddingViewModelId == id);
            dbContext.Remove(a);
            dbContext.SaveChanges();
            return Redirect("/User/Dashboard");
        }

    }
}
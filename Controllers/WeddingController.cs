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
    public class WeddingController : Controller
    {

        private MyContext dbContext;

        public WeddingController(MyContext context)
        {
            dbContext = context;
        }

        [Route("Wedding/newwedding")]
        [HttpGet]
        public IActionResult NewWedding()
        {
            if (HttpContext.Session.GetString("Session") == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Userid = HttpContext.Session.GetInt32("UserID");
                return View();
            }
        }

        [Route("Wedding/btAddWed")]
        [HttpPost]
        public IActionResult btAddWed(Wedding newWed)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newWed);
                dbContext.SaveChanges();
                return RedirectToAction("DetailWed", new { id = newWed.WeddingId });
            }
            else
            {
                return View("NewWedding");
            }
        }

        [Route("Wedding/detail/{id}")]
        [HttpGet]
        public IActionResult DetailWed(int id)
        {
            Wedding a = dbContext.Weddings.FirstOrDefault(pro => pro.WeddingId == id);
            ViewBag.WeddingInfo = a;
            var ListGuest = dbContext.Weddings
            .Include(per => per.WeddingtoUser)
            .ThenInclude(x => x.User)
            .FirstOrDefault(y => y.WeddingId == id);

            ViewBag.ListGuest = ListGuest;

            GoogleSigned.AssignAllServices(new GoogleSigned("ENTER-GOOGLE-MAPS-API-KEY-HERE"));

            var request = new GeocodingRequest();
            request.Address = a.WeddingAddress;
            var response = new GeocodingService().GetResponse(request);

            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
            {
                var result = response.Results.First();
                Console.WriteLine("Full Address: " + result.FormattedAddress);
                Console.WriteLine("Latitude: " + result.Geometry.Location.Latitude);
                Console.WriteLine("Longitude: " + result.Geometry.Location.Longitude);
                Console.WriteLine();
                ViewBag.Lati = result.Geometry.Location.Latitude;
                ViewBag.Long = result.Geometry.Location.Longitude;
            }
            else
            {
                Console.WriteLine("Unable to geocode.  Status={0} and ErrorMessage={1}", response.Status, response.ErrorMessage);
            }

            return View();
        }

        [Route("Wedding/DeleteWedding/{wedid}")]
        [HttpGet]
        public IActionResult DeleteWedding(int wedid)
        {
            Wedding a = dbContext.Weddings.FirstOrDefault(wed => wed.WeddingId == wedid);

            dbContext.Remove(a);
            dbContext.SaveChanges();
            return Redirect("/User/Dashboard");
        }
    }
}
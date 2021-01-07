using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesBooking.Models;

namespace MoviesBooking.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult MangmentMovies()
        {
            return View();
        }
    }
}
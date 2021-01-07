using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesBooking.Models;
using MoviesBooking.DAL;
using MoviesBooking.ViewModel;

namespace MoviesBooking.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult MangmentMovies()
        {
            Dal dal = new Dal();
            MovieViewModel cvm = new MovieViewModel();
            List<Movie> movies = dal.movies.ToList<Movie>();
            cvm.movie = new Movie();
            cvm.movies = movies;
            return View(cvm);
        }
    }
}
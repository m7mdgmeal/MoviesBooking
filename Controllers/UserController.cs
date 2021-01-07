using MoviesBooking.DAL;
using MoviesBooking.Models;
using MoviesBooking.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesBooking.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult ShowHowPage()
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
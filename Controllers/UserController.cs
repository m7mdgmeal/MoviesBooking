﻿using MoviesBooking.DAL;
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
            if (Session["UserName"] != null)
                Session["UserName"] = "0";

            Dal dal = new Dal();
            MovieViewModel cvm = new MovieViewModel();
            List<Movie> movies = dal.movies.ToList<Movie>();
            cvm.movie = new Movie();
            cvm.movies = movies;
            return View(cvm);
        }
        public ActionResult LoginUsers()
        {
            return View();
        }
        public ActionResult RegisterUsers()
        {
            return View();
        }
        public ActionResult AddNewUser(User obj)
        {
            Dal dal = new Dal();
            List<User> exist = (from x in dal.users where x.userName.Contains(obj.userName) select x).ToList<User>();
            if (exist.Count != 0)
            {
                TempData["msg"] = "User already exist !!";
                TempData["color"] = "red";
                return View("RegisterUsers");
            }

            TempData["msg"] = "User created successfully !!!";
            TempData["color"] = "blue";
            dal.users.Add(obj);
            dal.SaveChanges();
            return View("RegisterUsers");
        }
        public ActionResult SingIn(User obj)
        {
            Dal dal = new Dal();
            List<User> exist = (from x in dal.users where x.userName.Equals(obj.userName)
                                && x.password.Equals(obj.password)
                                select x).ToList<User>();
            if (exist.Count == 0)
            {
                TempData["msg"] = "Wrong information !!";
                TempData["color"] = "red";
                return View("LoginUsers");
            }

            if (exist[0].userType == "A")
                return RedirectToAction("MangmentMovies", "Admin");
            else
            {
                Session["UserName"] = obj.userName;
                return RedirectToAction("ShowHowPage", "User");
            }
        }
        
        public ActionResult BuyMovie()
        {

            Dal dal = new Dal();

            var movieId = int.Parse(Request.Form["movieId"]);
            var movie = (from x in dal.movies
                        where x.movieId == movieId
                        select x).ToList<Movie>()[0];

            return View("HallSeats", movie);
            /*if (exist.Count == 0)
            {
                TempData["msg"] = "Wrong information !!";
                TempData["color"] = "red";
                return View("LoginUsers");
            }

            if (exist[0].userType == "A")
                return RedirectToAction("MangmentMovies", "Admin");
            else
                return RedirectToAction("ShowHowPage", "User");
            */
            }
        public ActionResult HallSeats(Movie movie)
        {
            var dal = new Dal();
            var hall = (from x in dal.movies
                        from y in dal.halls
                        where x.movieId == movie.movieId
                        && y.hallId == x.hallId
                        select y).ToList<Hall>()[0];

            // get all not avalibale tickets
            var notAvalibaleTickets = (from x in dal.tickets
                                       where x.movieId == movie.movieId
                                       select x).ToList<Ticket>();

            var seatView = new SeatsViewModel()
            {
                movie = movie,
                tickets = notAvalibaleTickets,
                hall = hall
            };
            return View(seatView);
        }
        public ActionResult OrderMovies()
        {
            Dal dal = new Dal();
            MovieViewModel cvm = new MovieViewModel();
            string choice = (string)Request.Form["OrderBy"];
            List<Movie> movies;
            switch (choice)
            {
                case "Price increase":
                    movies = dal.movies.OrderBy(c => c.price).ToList<Movie>();
                    break;
                case "Price decrease":
                    movies = dal.movies.OrderByDescending(x => x.price).ToList<Movie>();
                    break;
                case "Most popular":
                    int i, j, min;
                    Movie temp;
                    List<Movie> AllMovies= dal.movies.ToList<Movie>();
                    for (i = 0; i < AllMovies.Count-1; i++)
                    {
                        min = i;
                        for (j = i + 1; j < AllMovies.Count; j++)
                            if (GetPopular(AllMovies[j].movieId) >= GetPopular(AllMovies[min].movieId))
                                min = j;
                        temp = AllMovies[i];
                        AllMovies[i] = AllMovies[min];
                        AllMovies[min] = temp;
                    }
                    movies = AllMovies;
                    break;
                default:
                    movies = dal.movies.OrderBy(x => x.category).ToList<Movie>();
                    break;
            }
            cvm.movie = new Movie();
            cvm.movies = movies;
            return View("ShowHowPage", cvm);
        }
        public int GetPopular(int id)
        {
            Dal dal = new Dal();
            List<Ticket> tickets = (from x in dal.tickets where x.movieId.Equals(id) select x).ToList<Ticket>();
            return tickets.Count;
        }
        public ActionResult SearchMovie()
        {
            return View();
        }
        public ActionResult SearchByCategory()
        {

            Dal dal = new Dal();
            string category=Request.Form["MovieCategory"];
            MovieViewModel cvm = new MovieViewModel();
            List<Movie> movies = (from x in dal.movies where x.category.Equals(category)
                                  select x).ToList<Movie>();
            cvm.movie = new Movie();
            cvm.movies = movies;
            return View("ShowHowPage", cvm);
        }
        public ActionResult SearchByTime()
        {

            Dal dal = new Dal();
            string time = Request.Form["MovieTime"];
            MovieViewModel cvm = new MovieViewModel();
            List<Movie> movies = (from x in dal.movies
                                  where x.time.Equals(time)
                                  select x).ToList<Movie>();
            cvm.movie = new Movie();
            cvm.movies = movies;
            return View("ShowHowPage", cvm);
        }
        public ActionResult SearchByDate()
        {

            Dal dal = new Dal();
            string date = Request.Form["MovieDate"];
            MovieViewModel cvm = new MovieViewModel();
            List<Movie> movies = (from x in dal.movies
                                  where x.time.Equals(date)
                                  select x).ToList<Movie>();
            cvm.movie = new Movie();
            cvm.movies = movies;
            return View("ShowHowPage",cvm);
        }
        public ActionResult SearchByPrice()
        {

            Dal dal = new Dal();
            double price;
            double.TryParse(Request.Form["rangeInput"], out price);
            MovieViewModel cvm = new MovieViewModel();
            List<Movie> movies = (from x in dal.movies
                                  where x.price <= price 
                                  select x).ToList<Movie>();
            cvm.movie = new Movie();
            cvm.movies = movies;
            return View("ShowHowPage", cvm);
        }
        public ActionResult FilterMovie()
        {

            Dal dal = new Dal();
            MovieViewModel cvm = new MovieViewModel();
            List<Movie> movies = (from x in dal.movies
                                  where x.prePrice !=0
                                  select x).ToList<Movie>();
            cvm.movie = new Movie();
            cvm.movies = movies;
            return View("ShowHowPage", cvm);
        }
        public ActionResult Cart()
        {
            Dal dal = new Dal();
            string UserName = (string)Session["UserName"];
            List<Ticket> tickets = (from x in dal.tickets where x.userName.Equals(UserName)
                                    select x).ToList<Ticket>();
            TicketViewModel cvm = new TicketViewModel();
            cvm.tickets = tickets;
            return View(cvm);
        }
    }
}
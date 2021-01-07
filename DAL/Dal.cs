using MoviesBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesBooking.DAL
{
    public class Dal:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("usersTbl");
            modelBuilder.Entity<Movie>().ToTable("moviesTbl");
            modelBuilder.Entity<Ticket>().ToTable("ticketsTbl");
            modelBuilder.Entity<Hall>().ToTable("hallsTbl");

        }
        public DbSet<User> users { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Hall> halls { get; set; }


        public List<Movie> getMoviesToUser()
        {
            //get all recent movies 
            var movieList = getRecentMovie();
            // get all avalibale movies
            movieList = getAvalibleMovies(movieList);
            //get all movies matching user ageg
            // movieList = getMatchedAges(movieList);
            return movieList;
        }
        public List<Movie> getRecentMovie()
        {
            var movieList = (from x in movies
                             where x.time.Equals(DateTime.Now.ToString())
                             select x).ToList<Movie>();
            return movieList;
        }
        public List<Movie> getAvalibleMovies(List<Movie> mv) 
        {
           var movieList =(from x in mv
                           from y in halls
                           where x.hallId == y.hallId &&
                           y.seatsNumber > 0
                           select x).ToList<Movie>();
            return movieList;
        }

    }

}
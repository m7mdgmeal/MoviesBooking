using MoviesBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesBooking.DAL
{
   // dfg
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


    }
}
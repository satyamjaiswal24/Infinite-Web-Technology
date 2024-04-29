using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MoviesDB_CodeFirst.Models
{
  
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext() : base("connectstr")
        { }
        public DbSet<Movie> Movies { get; set; }
    }
}
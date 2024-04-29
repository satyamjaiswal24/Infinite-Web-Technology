﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MoviesDB_CodeFirst.Models
{
    public class Movie
    {
        [Key]
        public int Mid { get; set; }
        public string Moviename { get; set; }
        public DateTime DateofRelease { get; set; }
    }

}
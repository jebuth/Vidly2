﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly2.Models;

namespace Vidly2.ViewModels
{
    public class MoviesWithGenres
    {
        public List<Movie> Movies { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
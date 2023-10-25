﻿using System.Diagnostics.CodeAnalysis;

namespace Cinemanage.Models.Database
{
    public class MovieCast
    {
        public int Id { get; set; }


        public string? MovieId { get; set; }

        public int CastId { get; set; }

        public string? Department {  get; set; }
        public string? Name { get; set; }
        public string? Character {  get; set; }
        public string? ImageUrl { get; set; }

        public Movie? Movie { get; set; }

    }
}
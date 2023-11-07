﻿using Cinemanage.Models.TMDB;

namespace Cinemanage.Services.Interfaces
{
    public interface IRemoteMovieService
    {
        Task<MovieDetail> MovieDetailAsync(int id); 
        Task<MovieSearch> SearchMoviesAsync(Enums.MovieCategory category, int count);
        Task<ActorDetail> ActorDetailAsync(int id);
    }
}

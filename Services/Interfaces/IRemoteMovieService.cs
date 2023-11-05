using Cinemanage.Models.TMDB;

namespace Cinemanage.Services.Interfaces
{
    public interface IRemoteMovieService
    {
        Task<MovieDetail> MovieDetailAsync(int id); 
        Task<MovieSearch> SearchMovieAsync(Enums.MovieCategory category, int count);
        Task<ActorDetail> ActorDetailAsync(int id);
    }
}

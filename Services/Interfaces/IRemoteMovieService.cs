using Cinemanage.Models.TMDB;

namespace Cinemanage.Services.Interfaces
{
    public interface IRemoteMovieService
    {
        Task<MovieDetail> MovieDetailAsync(int id); 
        Task<MovieSearch> MovieSearchAsync(Enums.MovieCategory category, int count);
        Task<ActorDetail> ActorDetailAsync(int id);
    }
}

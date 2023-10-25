using Cinemanage.Models.Database;
using Cinemanage.Models.TMDB;

namespace Cinemanage.Services.Interfaces
{
    public interface IDataMappingService
    {
        Task<Movie> MapMovieDetailAsync(MovieDetail movie);
        ActorDetail MapActorDetail(ActorDetail actor);


    }
}

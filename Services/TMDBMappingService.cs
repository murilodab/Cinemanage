﻿using Cinemanage.Enums;
using Cinemanage.Models.Database;
using Cinemanage.Models.Settings;
using Cinemanage.Models.TMDB;
using Cinemanage.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Cinemanage.Services
{
    public class TMDBMappingService : IDataMappingService
    {
        private AppSettings _appSettings;
        private readonly IImageService _imageService;

        public TMDBMappingService(IOptions<AppSettings> appSettings, IImageService imageService)
        {
            _appSettings = appSettings.Value;
            _imageService = imageService;
        }



        public ActorDetail MapActorDetail(ActorDetail actor)
        {
            actor.profile_path = BuildCastImage(actor.profile_path);

            if (string.IsNullOrEmpty(actor.biography))
                actor.biography = "Not Available";

            if (string.IsNullOrEmpty(actor.birthday))
                actor.place_of_birth = "Not Available";

            if (string.IsNullOrEmpty(actor.birthday))
                actor.birthday = "Not Available";
            else
                actor.birthday = DateTime.Parse(actor.birthday).ToString("MMM dd, yyyy");

            return actor;
        }

        public async Task<Movie> MapMovieDetailAsync(MovieDetail movie)
        {
            Movie newMovie = null;

            try
            {
                newMovie = new Movie()
                {
                    MovieId = movie.Id,
                    Title = movie.title,
                    TagLine = movie.tagline,
                    Overview = movie.overview,
                    RunTime = movie.runtime,
                    VoteAverage = movie.vote_average,
                    ReleaseDate = DateTime.Parse(movie.release_date.release_date),
                    TrailerUrl = BuildTrailerPath(movie.video),
                    Backdrop = await EncodeBackdropImageAsync(movie.backdrop_path),
                    BackdropType = BuildImageType(movie.backdrop_path),
                    Poster = await EncodePosterImageAsync(movie.poster_path),
                    Rating = GetRating(movie.release_dates)


                };

                var castMembers = movie.credits.cast.OrderByDescending(c => c.popularity)
                                                    .GroupBy(c => c.cast_id)
                                                    .Select(g => g.FirstOrDefault())
                                                    .Take(20)
                                                    .ToList();

                castMembers.ForEach(member =>
                {
                    newMovie.Cast.Add(new MovieCast()
                    {
                        CastId = member.id,
                        Department = member.known_for_department,
                        Name = member.name,
                        Character = member.character,
                        ImageUrl = BuildCastImage(member.profile_path)

                    });
                });

                var crewMembers = movie.credits.crew.OrderByDescending(c => c.popularity)
                                                    .GroupBy(c => c.id)
                                                    .Select(g => g.FirstOrDefault())
                                                    .Take(20)
                                                    .ToList();

                crewMembers.ForEach(member =>
                {
                    newMovie.Crew.Add(new MovieCrew()
                    {
                        CrewId = member.id,
                        Department = member.department,
                        Name = member.name,
                        Job = member.job,
                        ImageUrl = BuildCastImage(member.profile_path)
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MapMovieDetailAsync: {ex.Message}");
            }

            return newMovie;
        }

        private string BuildCastImage(string profilePath)
        {
            if (string.IsNullOrEmpty(profilePath))
            {
                return _appSettings.CinemanageSettings.DefaultCastImage;
            }

            return $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.CinemanageSettings.DefaultPosterSize}/{profilePath}";

        }

        private MovieRating GetRating(Release_Dates dates)
        {
            var movieRating = MovieRating.NR;
            var certification = dates.results.FirstOrDefault(r => r.iso_3166_1 == "US");
            if (certification is not null)
            {
                var apiRating = certification.release_dates.FirstOrDefault(c => c.certification != "")?.certification.Replace("-", "");
                if (!string.IsNullOrEmpty(apiRating))
                {
                    movieRating = (MovieRating)Enum.Parse(typeof(MovieRating), apiRating, true);
                }
            }

            return movieRating;
        }

        private async Task<byte[]> EncodePosterImageAsync(string path)
        {
            var posterPath = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.CinemanageSettings.DefaultPosterSize}/{path}";
            return await _imageService.EncodeImageURLAsync(posterPath);
        }
       
        private string BuildImageType(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                return path;
            }

            return $"image/{Path.GetExtension(path).TrimStart('.')}";
        }


        private string BuildTrailerPath(Videos video)
        {
            var videoKey = video.results.FirstOrDefault(r => r.type.ToLower().Trim() == "trailer" && r.key != "")?.key;
            return string.IsNullOrEmpty(videoKey) ? videoKey : $"{_appSettings.TMDBSettings.BaseYoutubePath}{videoKey}";       
        }

        private async Task<byte[]> EncodeBackdropImageAsync(string path)
        {
            var backdropPath = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.CinemanageSettings.DefaultBackdropSize}/{path}";
            return await _imageService.EncodeImageURLAsync(backdropPath);
        }
    }
}

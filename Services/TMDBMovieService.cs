﻿using Cinemanage.Enums;
using Cinemanage.Models.Settings;
using Cinemanage.Models.TMDB;
using Cinemanage.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace Cinemanage.Services
{
    public class TMDBMovieService : IRemoteMovieService
    {

        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClient;

        public TMDBMovieService(IOptions<AppSettings> appSettings, IHttpClientFactory httpClient)
        {
            _appSettings = appSettings.Value;
            _httpClient = httpClient;
        }

        public async Task<ActorDetail> ActorDetailAsync(int id)
        {
            // Step1: setup a default instance of MovieSearch
            ActorDetail actorDetail = new();

            //Step2: Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/person/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                {"api_key", _appSettings.CinemanageSettings.TMDBApiKey },
                {"language", _appSettings.TMDBSettings.QueryOptions.Language },
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step3: Create a client and execute the request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step4: Return the MovieSearch object
            if (response.IsSuccessStatusCode)
            {

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var dcjs = new DataContractJsonSerializer(typeof(ActorDetail));
                actorDetail = (ActorDetail)dcjs.ReadObject(responseStream);
            }

            return actorDetail;
        }

        public async Task<MovieDetail> MovieDetailAsync(int id)
        {
            // Step1: setup a default instance of MovieDetail
            MovieDetail movieDetail = new MovieDetail();

            //Step2: Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{id}";

            var queryParams = new Dictionary<string, string>()
            {
                {"api_key", _appSettings.CinemanageSettings.TMDBApiKey },
                {"language", _appSettings.TMDBSettings.QueryOptions.Language },
                {"append_to_response", _appSettings.TMDBSettings.QueryOptions.AppendToResponse }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step3: Create a client and execute the request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step4: Deserialize into MovieDetail
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                //var dcjs = new DataContractJsonSerializer(typeof(MovieDetail));
                
                //movieDetail = dcjs.ReadObject(responseStream) as MovieDetail;

                movieDetail = JsonSerializer.Deserialize<MovieDetail>(responseStream);
               
            }

            return movieDetail;
        }


        public async Task<MovieSearch> SearchMoviesAsync(MovieCategory category, int count)
        {
            //Step1: setup a default instance of MovieSearch
            MovieSearch movieSearch = new();

            //Step2: Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{category}";

            var queryParams = new Dictionary<string, string>()
            {
                {"api_key", _appSettings.CinemanageSettings.TMDBApiKey },
                {"language", _appSettings.TMDBSettings.QueryOptions.Language },
                {"page", _appSettings.TMDBSettings.QueryOptions.Page }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step3: Create a client and execute the request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step4: Return the MovieSearch object
            if (response.IsSuccessStatusCode)
            {
                var dcjs = new DataContractJsonSerializer(typeof(MovieSearch));
                using var responseStream = await response.Content.ReadAsStreamAsync();
                movieSearch = (MovieSearch)dcjs.ReadObject(responseStream);
                movieSearch.results = movieSearch.results.Take(count).ToArray();
                movieSearch.results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.CinemanageSettings.DefaultPosterSize}/{r.poster_path}");
            }

            return movieSearch;
        }
    }
}

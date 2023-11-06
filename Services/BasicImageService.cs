using Cinemanage.Services.Interfaces;

namespace Cinemanage.Services
{
    public class BasicImageService : IImageService
    {

        private readonly IHttpClientFactory _httpClient;

        public BasicImageService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public string DecodeImage(byte[] oster, string contentType)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> EncodeImageAsync(IFormFile poster)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> EncodeImageURLAsync(string imageURL)
        {
            throw new NotImplementedException();
        }
    }
}

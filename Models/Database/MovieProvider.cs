namespace Cinemanage.Models.Database
{
    public class MovieProvider
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ProviderId { get; set; }
        public string? Name {  get; set; }
        public string? LogoUrl { get; set; }
    }
}

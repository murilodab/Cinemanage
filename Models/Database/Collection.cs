namespace Cinemanage.Models.Database
{
    public class Collection
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descripton {  get; set; }

        public ICollection<MovieCollection>? MovieCollections { get; set; } = new HashSet<MovieCollection>();

    }
}

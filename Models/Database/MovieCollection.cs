using X.PagedList;

namespace Cinemanage.Models.Database
{
    public class MovieCollection
    {
        public int Id {  get; set; }
        public int CollectionId {  get; set; }
        public int MovieId {  get; set; }
        public int Order {  get; set; }

        public Collection? Collection { get; set; }
        public Movie? Movie { get; set; }

        public IPagedList<MovieCollection>? MovieCollectionsList { get; set; }

    }
}

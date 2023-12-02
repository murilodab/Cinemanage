using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace Cinemanage.Models.Database
{
    public class MovieCollection
    {
        public int Id {  get; set; }
        public int CollectionId {  get; set; }
        public int MovieId {  get; set; }
       
        [Required]
        public string? AppUserId { get; set; }

        public Collection? Collection { get; set; }
        public Movie? Movie { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public IPagedList<MovieCollection>? MovieCollectionsList { get; set; }

    }
}

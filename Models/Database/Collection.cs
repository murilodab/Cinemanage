using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.PagedList;

namespace Cinemanage.Models.Database
{
    public class Collection
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public string? Name { get; set; }
        public string? Description {  get; set; }    
        
        

        public virtual AppUser? AppUser { get; set; }
        public ICollection<MovieCollection>? MovieCollections { get; set; } = new HashSet<MovieCollection>();
}

    }

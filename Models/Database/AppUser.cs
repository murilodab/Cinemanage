using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cinemanage.Models.Database
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]//Label
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and a max {1} characters long.", MinimumLength = 2)] //{0} is the column itself
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]//Label
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and a max {1} characters long.", MinimumLength = 2)] //{0} is the column itself
        public string? LastName { get; set; }

        [NotMapped]
        public string? FullName { get { return $"{FirstName} {LastName}"; } }

        public virtual ICollection<Collection?> Collections { get; set; } = new HashSet<Collection?>();
        public virtual ICollection<MovieCollection?> MovieCollections { get; set; } = new HashSet<MovieCollection?>();

    }
}

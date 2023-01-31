using Microsoft.AspNetCore.Identity;

namespace ProjWebProgramming.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public List<Wishlist>? Wishlists { get; set; }
    }
}

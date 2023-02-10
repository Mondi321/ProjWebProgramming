using Microsoft.AspNetCore.Identity;

namespace ProjWebProgramming.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public List<Wishlist>? Wishlists { get; set; }
        public ICollection<TvShow> TvShows { get; set; } = new List<TvShow>();
        public List<TvShowWishlist>? TvShowWishlists { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

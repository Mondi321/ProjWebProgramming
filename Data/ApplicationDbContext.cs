using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProjWebProgramming.Models;

namespace ProjWebProgramming.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .HasMany(p => p.Genres)
                .WithMany(p => p.Movies)
                .UsingEntity<MovieGenre>(
                    j => j
                        .HasOne(pt => pt.Genre)
                        .WithMany(t => t.MovieGenres)
                        .HasForeignKey(pt => pt.GenreId),
                    j => j
                        .HasOne(pt => pt.Movie)
                        .WithMany(p => p.MovieGenres)
                        .HasForeignKey(pt => pt.MovieId),
                    j =>
                    {
                        j.Property(pt => pt.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                        j.HasKey(t => new { t.MovieId, t.GenreId});
                    });

            modelBuilder.Entity<Movie>()
                .HasMany(p => p.Actors)
                .WithMany(p => p.Movies)
                .UsingEntity<MovieActors>(
                    j => j
                        .HasOne(pt => pt.Actor)
                        .WithMany(t => t.MovieActors)
                        .HasForeignKey(pt => pt.ActorId),
                    j => j
                        .HasOne(pt => pt.Movie)
                        .WithMany(p => p.MovieActors)
                        .HasForeignKey(pt => pt.MovieId));

            modelBuilder.Entity<Movie>()
                .HasMany(p => p.Users)
                .WithMany(p => p.Movies)
                .UsingEntity<Wishlist>(
                    j => j
                        .HasOne(pt => pt.User)
                        .WithMany(t => t.Wishlists)
                        .HasForeignKey(pt => pt.UserId),
                    j => j
                        .HasOne(pt => pt.Movie)
                        .WithMany(p => p.Wishlists)
                        .HasForeignKey(pt => pt.MovieId),
                    j =>
                    {
                        j.Property(pt => pt.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                        j.HasKey(t => new { t.MovieId, t.UserId });
                    });



            modelBuilder.Entity<TvShow>()
                .HasMany(p => p.Genres)
                .WithMany(p => p.TvShows)
                .UsingEntity<TvShowGenre>(
                    j => j
                        .HasOne(pt => pt.Genre)
                        .WithMany(t => t.TvShowGenres)
                        .HasForeignKey(pt => pt.GenreId),
                    j => j
                        .HasOne(pt => pt.TvShow)
                        .WithMany(p => p.TvShowGenres)
                        .HasForeignKey(pt => pt.TvShowId),
                    j =>
                    {
                        j.Property(pt => pt.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                        j.HasKey(t => new { t.TvShowId, t.GenreId });
                    });


            modelBuilder.Entity<TvShow>()
                .HasMany(p => p.Actors)
                .WithMany(p => p.TvShows)
                .UsingEntity<TvShowActor>(
                    j => j
                        .HasOne(pt => pt.Actor)
                        .WithMany(t => t.TvShowActors)
                        .HasForeignKey(pt => pt.ActorId),
                    j => j
                        .HasOne(pt => pt.TvShow)
                        .WithMany(p => p.TvShowActors)
                        .HasForeignKey(pt => pt.TvShowId));


            modelBuilder.Entity<TvShow>()
                .HasMany(p => p.Users)
                .WithMany(p => p.TvShows)
                .UsingEntity<TvShowWishlist>(
                    j => j
                        .HasOne(pt => pt.User)
                        .WithMany(t => t.TvShowWishlists)
                        .HasForeignKey(pt => pt.UserId),
                    j => j
                        .HasOne(pt => pt.TvShow)
                        .WithMany(p => p.TvShowWishlists)
                        .HasForeignKey(pt => pt.TvShowId),
                    j =>
                    {
                        j.Property(pt => pt.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                        j.HasKey(t => new { t.TvShowId, t.UserId });
                    });
        }

        public DbSet<ProjWebProgramming.Models.MovieGenre> MovieGenre { get; set; }

        public DbSet<ProjWebProgramming.Models.Actor> Actor { get; set; }

        public DbSet<ProjWebProgramming.Models.MovieActors> MovieActors { get; set; }
        public DbSet<ProjWebProgramming.Models.Director> Directors{ get; set; }
        public DbSet<ProjWebProgramming.Models.Wishlist> Wishlists{ get; set; }
        public DbSet<TvShow> TvShows{ get; set; }
        public DbSet<TvShowActor> TvShowActors { get; set; }
        public DbSet<TvShowGenre> TvShowGenres { get; set; }
        public DbSet<TvShowWishlist> TvShowWishlists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ProjWebProgramming.Models.Review> Review { get; set; }
        public DbSet<ProjWebProgramming.Models.Contact> Contact { get; set; }
    }
}
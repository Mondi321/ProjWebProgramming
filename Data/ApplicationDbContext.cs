﻿using Microsoft.AspNetCore.Identity;
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
        }

        public DbSet<ProjWebProgramming.Models.MovieGenre> MovieGenre { get; set; }
    }
}
﻿using Microsoft.EntityFrameworkCore;
using DisneyMoviesWatchlist.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DisneyMoviesWatchlist.DatabaseContext;
public partial class DisneyMoviesDbContext : IdentityDbContext
{
    public DisneyMoviesDbContext()
    {
    }

    public DisneyMoviesDbContext(DbContextOptions<DisneyMoviesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> DisneyMovies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Name=ConnectionStrings:DisneyMoviesDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Identity");

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId);

            entity.ToTable("disney_movies");

            entity.Property(e => e.Directors).HasColumnName("directors");
            entity.Property(e => e.Genre).HasColumnName("genre");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Link).HasColumnName("link");
            entity.Property(e => e.Metascore).HasColumnName("metascore");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Runtime).HasColumnName("runtime");
            entity.Property(e => e.Stars).HasColumnName("stars");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");
        });
        modelBuilder.UseCollation("NOCASE");

    }
}

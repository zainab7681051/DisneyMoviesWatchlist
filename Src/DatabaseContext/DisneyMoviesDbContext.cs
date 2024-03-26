using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DisneyMoviesWatchlist.Src.DatabaseContext;
public partial class DisneyMoviesDbContext : IdentityDbContext
{
    public DisneyMoviesDbContext()
    {
    }

    public DisneyMoviesDbContext(DbContextOptions<DisneyMoviesDbContext> options)
        : base(options)
    {
    }


    public DbSet<Movie> DisneyMovies { get; set; }
    public DbSet<MovieAndUser> MoviesAndUsers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Name=ConnectionStrings:DisneyMoviesDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
        modelBuilder.Entity<MovieAndUser>(e =>
        {
            e.HasKey(ee => new { ee.UserId, ee.MovieId });
        });

    }


}
public class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; }

    public string Year { get; set; }

    public string Link { get; set; }

    public string Image { get; set; }

    public string Runtime { get; set; }

    public string Genre { get; set; }

    public string Summary { get; set; }

    public string Rating { get; set; }

    public string Metascore { get; set; }

    public string Directors { get; set; }

    public string Stars { get; set; }
}

public class MovieAndUser
{
    public string UserId { get; set; }
    public int MovieId { get; set; }
}
using DisneyMoviesWatchlist.Src.DatabaseContext;
using DisneyMoviesWatchlist.Src.Extensions;
using DisneyMoviesWatchlist.Src.Models;

namespace DisneyMoviesWatchlist.Src.Repository;
public class MovieRepository : IMovieRepository
{
    private readonly ILogger<MovieRepository> logger;
    private readonly DisneyMoviesDbContext context;
    public MovieRepository(
        ILogger<MovieRepository> logger,
        DisneyMoviesDbContext context)
    {
        this.logger = logger;
        this.context = context;
    }
    public List<MovieDto> GetAll(string query = null)
    {
        var Movies = from m in context.DisneyMovies
                     select m;

        Movies = Movies.OrderByDescending(s => s.MovieId);

        var MovieList = Movies.Select(e => e.MovieLessDetail());
        if (!string.IsNullOrEmpty(query))
        {
            MovieList = MovieList.Where(s => s.Title.Equals(query, StringComparison.OrdinalIgnoreCase));
        }
        return MovieList.ToList();
    }

    public Movie GetOne(int id)
    {
        throw new NotImplementedException();
    }
}
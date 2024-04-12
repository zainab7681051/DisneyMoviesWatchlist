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


    public List<Movie> GetAll(string query)
    {
        var Movies = from m in context.DisneyMovies
                     select m;

        Movies = Movies.OrderByDescending(s => s.MovieId);
        return Movies.ToList();
    }

    public Movie GetOne(int MovieId)
    {
        return context.DisneyMovies.Find(MovieId);
    }

    public List<MovieDto> GetWatchList(string UserId)
    {
        var watchList = context.MoviesAndUsers.Where(m => m.UserId == UserId).ToList();
        List<MovieDto> list = new();
        foreach (var m in watchList)
        {
            var x = context.DisneyMovies.Find(m.MovieId).MovieLessDetail();
            list.Add(x);
        }
        return list;
    }

    public void AddToWatchList(string UserId, int MovieId)
    {
        if (!IsInWatchList(UserId, MovieId))
        {
            var movie = context.DisneyMovies.Find(MovieId);
            context.MoviesAndUsers.Add(new MovieAndUser
            {
                UserId = UserId,
                MovieId = movie.MovieId
            });
            context.SaveChanges();
        }
    }

    public bool IsInWatchList(string UserId, int MovieId)
    {
        var x = context.MoviesAndUsers.Find(UserId, MovieId);
        if (x is null) return false;
        return true;
    }

    public void RemoveFromWatchList(string UserId, int MovieId)
    {
        if (IsInWatchList(UserId, MovieId))
        {
            var movie = context.DisneyMovies.Find(MovieId);
            var x = context.MoviesAndUsers.Find(UserId, movie.MovieId);
            context.MoviesAndUsers.Remove(x);
            context.SaveChanges();
        }
    }

}
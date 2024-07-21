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


    public List<MovieDto> GetAll(string query, int pageNumber, out bool lastPage)
    {
        const int chunkSize = 9;
        int start = (pageNumber - 1) * chunkSize;
        Console.WriteLine('here');
        IQueryable<Movie> movies;
        
        if (!string.IsNullOrEmpty(query))
        {
            movies = context.DisneyMovies
                .Where(m => (m.Title.Contains(query) || m.Year.Contains(query) || m.Summary.Contains(query) || m.Stars.Contains(query) || m.Directors.Contains(query)) && m.MovieId > start)
                .Take(chunkSize + 1 );
        }
        else
        {
            movies = context.DisneyMovies
                .Where(m => m.MovieId > start)
                .Take(chunkSize + 1);
        }
        
        
        var result = movies.Select(m => m.MovieLessDetail()).ToList();
        if (result.Count > chunkSize)
        {
            lastPage = false;
            result = result.Take(chunkSize).ToList();
        }
        else
        {
            lastPage = true;
        }
        return result;
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
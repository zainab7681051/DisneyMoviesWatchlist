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


    public List<MovieDto> GetAll(string query, int pageNumber, out bool islastPage)
    {
        const int chunkSize = 12;
        int start = (pageNumber - 1) * chunkSize;
        List<MovieDto> movies;

        if (!string.IsNullOrEmpty(query))
        {
            movies = context.DisneyMovies
                .ToList()
                .Where((m, index) => (m.Title.Contains(query) || m.Year.Contains(query) || m.Summary.Contains(query) || m.Stars.Contains(query) || m.Directors.Contains(query)) && index > start)
                .Take(chunkSize + 1)
                .Select(m => m.MovieLessDetail())
                .ToList();
        }
        else
        {
            movies = context.DisneyMovies
                .ToList()
                .Where((m, index) => index > start)
                .Take(chunkSize + 1)
                .Select(m => m.MovieLessDetail())
                .ToList();
        }


        if (movies.Count > chunkSize)
        {
            islastPage = false;
            movies = movies.Take(chunkSize).ToList();
        }
        else
        {
            islastPage = true;
        }
        return movies;
    }


    public Movie GetOne(int MovieId)
    {
        return context.DisneyMovies.Find(MovieId);
    }

    public List<MovieDto> GetWatchList(string UserId)
    {
        List<MovieAndUser> watchList = context.MoviesAndUsers.Where(m => m.UserId == UserId).ToList();
        List<MovieDto> list = new();
        foreach (var m in watchList)
        {
            MovieDto x = context.DisneyMovies.Find(m.MovieId).MovieLessDetail();
            list.Add(x);
        }
        return list;
    }

    public void AddToWatchList(string UserId, int MovieId)
    {
        if (!IsInWatchList(UserId, MovieId))
        {
            Movie movie = context.DisneyMovies.Find(MovieId);
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
        MovieAndUser x = context.MoviesAndUsers.Find(UserId, MovieId);
        if (x is null) return false;
        return true;
    }

    public void RemoveFromWatchList(string UserId, int MovieId)
    {
        if (IsInWatchList(UserId, MovieId))
        {
            Movie movie = context.DisneyMovies.Find(MovieId);
            MovieAndUser x = context.MoviesAndUsers.Find(UserId, movie.MovieId);
            context.MoviesAndUsers.Remove(x);
            context.SaveChanges();
        }
    }

}
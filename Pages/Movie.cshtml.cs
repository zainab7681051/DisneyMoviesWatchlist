using DisneyMoviesWatchlist.DatabaseContext;
using DisneyMoviesWatchlist.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace DisneyMoviesWatchlist.Pages;

public class MovieModel : PageModel
{
    private readonly ILogger<MovieModel> logger;
    private readonly DisneyMoviesDbContext context;

    public MovieModel(ILogger<MovieModel> logger, DisneyMoviesDbContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    public Movie? movie { get; set; }
    public void OnGet(int id)
    {
        movie = context.DisneyMovies.Find(id);
    }
}

using DisneyMoviesWatchlist.DatabaseContext;
using DisneyMoviesWatchlist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DisneyMoviesWatchlist.Extensions;
namespace DisneyMoviesWatchlist.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    private readonly DisneyMoviesDbContext context;

    public IndexModel(ILogger<IndexModel> logger, DisneyMoviesDbContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    public List<MovieDto>? movies { get; set; }
    public void OnGet()
    {
        movies = context.DisneyMovies.Select(e => e.MovieLessDetail()).ToList();
    }
}

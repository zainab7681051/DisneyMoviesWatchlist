using DisneyMoviesWatchlist.DatabaseContext;
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

    [BindProperty(SupportsGet = true)]
    public string? query { get; set; }
    public void OnGet()
    {
        var Movies = from m in context.DisneyMovies
                     select m;
        if (!string.IsNullOrEmpty(query))
        {
            Movies = Movies.Where(s => s.Title!.Contains(query));
        }
        movies = Movies.Select(e => e.MovieLessDetail()).ToList();

    }
}

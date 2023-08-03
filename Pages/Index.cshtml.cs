using DisneyMoviesWatchlist.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DisneyMoviesWatchlist.Extensions;
using Microsoft.AspNetCore.Identity;

namespace DisneyMoviesWatchlist.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    private readonly DisneyMoviesDbContext context;
    private readonly UserManager<IdentityUser> userManager;
    public IndexModel(
        ILogger<IndexModel> logger,
        DisneyMoviesDbContext context,
        UserManager<IdentityUser> userManager)
    {
        this.logger = logger;
        this.context = context;
        this.userManager = userManager;
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
    public void OnPostAdd(int id)
    {
        int user = int.Parse(userManager.GetUserId(User));
        var movie = context.DisneyMovies.Find(id);
        int movieId = movie.MovieId;
        context.MoviesAndUsers.Add(new MovieAndUser
        {
            UserId = user,
            MovieId = movieId
        });
    }
}

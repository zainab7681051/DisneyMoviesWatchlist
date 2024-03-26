using DisneyMoviesWatchlist.Src.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace DisneyMoviesWatchlist.Src.Pages;

public class MovieModel : PageModel
{
    private readonly ILogger<MovieModel> logger;
    private readonly DisneyMoviesDbContext context;
    private readonly UserManager<IdentityUser> userManager;
    public MovieModel(
        ILogger<MovieModel> logger,
        DisneyMoviesDbContext context,
        UserManager<IdentityUser> userManager)
    {
        this.logger = logger;
        this.context = context;
        this.userManager = userManager;
    }

    public Movie DisneyMovie { get; set; }
    public void OnGet(int id)
    {
        DisneyMovie = context.DisneyMovies.Find(id);
    }
    public IActionResult OnPostAdd(int id)
    {
        var user = userManager.GetUserId(User);
        var movie = context.DisneyMovies.Find(id);
        int movieId = movie.MovieId;
        context.MoviesAndUsers.Add(new MovieAndUser
        {
            UserId = user,
            MovieId = movieId
        });
        context.SaveChanges();
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int id)
    {
        var userId = userManager.GetUserId(User);
        var movie = context.DisneyMovies.Find(id);
        int MovieId = movie.MovieId;
        var x = context.MoviesAndUsers.Find(userId, MovieId);
        context.MoviesAndUsers.Remove(x);
        context.SaveChanges();
        return RedirectToPage();
    }

    public bool Bookmarked(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        var x = context.MoviesAndUsers.Find(userId, MovieId);
        if (x is null) return false;
        return true;
    }
}

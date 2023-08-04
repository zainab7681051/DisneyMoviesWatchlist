using DisneyMoviesWatchlist.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DisneyMoviesWatchlist.Extensions;

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
    
    public IActionResult OnPostRemove(int id){
        var userId=userManager.GetUserId(User);
        var movie=context.DisneyMovies.Find(id);
        int MovieId=movie.MovieId;
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DisneyMoviesWatchlist.DatabaseContext;
using DisneyMoviesWatchlist.Extensions;

namespace DisneyMoviesWatchlist.Pages;

[Authorize]
public class WatchlistModel : PageModel
{
    private readonly ILogger<WatchlistModel> logger;
    private readonly DisneyMoviesDbContext context;
    private readonly UserManager<IdentityUser> userManager;

    public WatchlistModel(
        ILogger<WatchlistModel> logger,
        DisneyMoviesDbContext context,
        UserManager<IdentityUser> userManager)
    {
        this.logger = logger;
        this.context = context;
        this.userManager = userManager;
    }

    public List<MovieDto>? movies { get; set; }
    public void OnGet()
    {
        var userId=userManager.GetUserId(User);
        var watchList=context.MoviesAndUsers.Where(m=>m.UserId==userId).ToList();
        List<MovieDto> list=new();
        foreach (var m in watchList)
        {
            var x=context.DisneyMovies.Find(m.MovieId).MovieLessDetail();
            list.Add(x);   
        }
        movies=list;
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
}

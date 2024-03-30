using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;

namespace DisneyMoviesWatchlist.Src.Pages;

public class IndexModel : PageModel
{
    private readonly IMovieRepository movieRepo;
    private readonly UserManager<IdentityUser> userManager;

    public List<MovieDto>? Movies { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? query { get; set; }
    public int? PageViewHash { get; set; }=null;

    public IndexModel(
        IMovieRepository movieRepo,
        UserManager<IdentityUser> userManager)
    {
        this.movieRepo = movieRepo;
        this.userManager = userManager;
    }

    public void OnGet()
    {
        Movies = movieRepo.GetAll(query);
    }
    public IActionResult OnPostAdd(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        movieRepo.AddToWatchList(userId, MovieId);
        PageViewHash = MovieId;
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        movieRepo.RemoveFromWatchList(userId, MovieId);
        PageViewHash = MovieId;
        return RedirectToPage();
    }

    public bool Bookmarked(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        return movieRepo.IsInWatchList(userId, MovieId);
    }
}

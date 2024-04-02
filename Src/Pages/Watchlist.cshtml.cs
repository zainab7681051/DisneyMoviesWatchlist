using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;

namespace DisneyMoviesWatchlist.Src.Pages;

[Authorize]
public class WatchlistModel : PageModel
{
    private readonly IMovieRepository movieRepo;
    private readonly UserManager<IdentityUser> userManager;
    public List<MovieDto> Movies { get; set; }

    public WatchlistModel(
        IMovieRepository movieRepo,
        UserManager<IdentityUser> userManager)
    {
        this.movieRepo = movieRepo;
        this.userManager = userManager;
    }
    public void OnGet()
    {
        var userId = userManager.GetUserId(User);
        Movies = movieRepo.GetWatchList(userId);
    }

    public IActionResult OnPostRemove(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        movieRepo.RemoveFromWatchList(userId, MovieId);
        return RedirectToPage();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;

namespace DisneyMoviesWatchlist.Src.Pages;

public class MovieModel : PageModel
{
    private readonly IMovieRepository movieRepo;
    private readonly UserManager<IdentityUser> userManager;
    public Movie DisneyMovie { get; set; }

    public MovieModel(
        IMovieRepository movieRepo,
        UserManager<IdentityUser> userManager)
    {
        this.movieRepo = movieRepo;
        this.userManager = userManager;
    }

    public void OnGet(int MovieId)
    {
        DisneyMovie = movieRepo.GetOne(MovieId);
    }
    public IActionResult OnPostAdd(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        movieRepo.AddToWatchList(userId, MovieId);
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        movieRepo.RemoveFromWatchList(userId, MovieId);
        return RedirectToPage();
    }

    public bool Bookmarked(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        return movieRepo.IsInWatchList(userId, MovieId);
    }
}

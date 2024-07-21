using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;
using DisneyMoviesWatchlist.Src.Extensions;

namespace DisneyMoviesWatchlist.Src.Pages;

public class IndexModel : PageModel
{
    private readonly IMovieRepository movieRepo;
    private readonly UserManager<IdentityUser> userManager;

    public List<MovieDto> Movies { get; set; }
    public List<Movie> HeroSectionItems { get; set; }
    
    public bool islastPage {get; set;}
    [FromQuery(Name="page")]
    public int page {get; set;}
    [BindProperty(SupportsGet = true)]
    public string query { get; set; }

    public IndexModel(
        IMovieRepository movieRepo,
        UserManager<IdentityUser> userManager
    )
    {
        this.movieRepo = movieRepo;
        this.userManager = userManager;
        this.page = 1;
    }

    public void OnGet()
    {
        HeroSectionItems = new List<Movie>();
        bool last = islastPage;
        Movies = movieRepo.GetAll(query, page, out last);
        islastPage = last;
        if (string.IsNullOrEmpty(query) && page == 1)
        {
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                int randomNumber = rnd.Next(1, 73);
                HeroSectionItems.Add(movieRepo.GetOne(randomNumber));
            }
        }

    }

    public IActionResult OnPostAdd(int MovieId)
    {
        string userId = userManager.GetUserId(User);
        movieRepo.AddToWatchList(userId, MovieId);
        
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int MovieId)
    {
        string userId = userManager.GetUserId(User);
        movieRepo.RemoveFromWatchList(userId, MovieId);
        
        return RedirectToPage();
    }

    public bool Bookmarked(int MovieId)
    {
        string userId = userManager.GetUserId(User);
        return movieRepo.IsInWatchList(userId, MovieId);
    }
}

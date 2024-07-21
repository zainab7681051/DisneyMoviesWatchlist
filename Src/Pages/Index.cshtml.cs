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
    [BindProperty(SupportsGet = true)]
    public int page {get; set;} = 1;
    [BindProperty(SupportsGet = true)]
    public string query { get; set; }

    public IndexModel(
        IMovieRepository movieRepo,
        UserManager<IdentityUser> userManager
    )
    {
        this.movieRepo = movieRepo;
        this.userManager = userManager;
    }

    public void OnGet()
    {
        HeroSectionItems = new List<Movie>();
        bool last = islastPage;
        Console.WriteLine("page is 🍧🍧🍧🍧🍧🍧🍧🍧🍧🍧 {0}", page);
        Movies = movieRepo.GetAll(query, 5, out last);
        islastPage = last;
        if (string.IsNullOrEmpty(query) && page > 1)
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
    public IActionResult OnPostNext(){
        Console.WriteLine("HERERRERERERERERER🍓🍓🍓🍓🍓🍓🍓-----");
        page++;
        return RedirectToPage("./Index", new { page = page});
    }
    
    public IActionResult OnPostPrev(){
        page--;
        return RedirectToPage("./Index", new { page = page});
    }
}

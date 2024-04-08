using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;
using DisneyMoviesWatchlist.Src.Extensions;

namespace DisneyMoviesWatchlist.Src.Pages;

public class IndexModel : PageModel
{
    private readonly IMovieRepository movieRepo;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IMemoryCache memoryCache;


    public List<MovieDto> Movies { get; set; }
    public List<Movie> HeroSectionItems { get; set; }

    [BindProperty(SupportsGet = true)]
    public string query { get; set; }


    public int PageViewHash
    {
        get => memoryCache.Get<int>("PageViewHash");
        set => memoryCache.Set("PageViewHash", value, TimeSpan.FromSeconds(7));
    }
    public IndexModel(
        IMovieRepository movieRepo,
        UserManager<IdentityUser> userManager,
        IMemoryCache memoryCache)
    {
        this.movieRepo = movieRepo;
        this.userManager = userManager;
        this.memoryCache = memoryCache;
    }

    public void OnGet()
    {
        HeroSectionItems = new();
        var AllMovies = movieRepo.GetAll(query);
        Movies = AllMovies.Select(m => m.MovieLessDetail()).ToList();
        if (!string.IsNullOrEmpty(query))
        {
            Movies = Movies.Where(s => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || s.Year.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        else
        {
            Random rnd = new();
            for (int i = 0; i < 4; i++)
            {
                int randomNumber = rnd.Next(0, 72);
                HeroSectionItems.Add(AllMovies[randomNumber]);
            }
        }

    }

    public IActionResult OnPostAdd(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        movieRepo.AddToWatchList(userId, MovieId);
        if (memoryCache.Get<int>("PageViewHash") == 0)
        {
            PageViewHash = MovieId;
        }
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        movieRepo.RemoveFromWatchList(userId, MovieId);
        if (memoryCache.Get<int>("PageViewHash") == 0)
        {
            PageViewHash = MovieId;
        }
        return RedirectToPage();
    }

    public bool Bookmarked(int MovieId)
    {
        var userId = userManager.GetUserId(User);
        return movieRepo.IsInWatchList(userId, MovieId);
    }
}

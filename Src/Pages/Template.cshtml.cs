using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;
using DisneyMoviesWatchlist.Src.Extensions;

namespace DisneyMoviesWatchlist.Src.Pages;

public class TemplateModel : PageModel
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
    public TemplateModel(
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
        var AllMovies = movieRepo.GetAll(query);
        Random rnd = new();
        for (int i = 0; i < 4; i++)
        {
            int randomNumber = rnd.Next(0, 72); // Generates a random number between 0 and 71
            HeroSectionItems.Add(AllMovies[randomNumber]);
        }

        Movies = AllMovies.Select(m => m.MovieLessDetail()).ToList();
        if (!string.IsNullOrEmpty(query))
        {
            Movies = Movies.Where(s => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || s.Year.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
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

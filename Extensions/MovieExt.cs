using DisneyMoviesWatchlist.DatabaseContext;

namespace DisneyMoviesWatchlist.Extensions;

public class MovieDto
{
    public int MovieId { get; set; }

    public string? Title { get; set; }

    public string? Year { get; set; }

    public string? Image { get; set; }
    public string? Rating { get; set; }


}
public static class MovieExt
{
    public static MovieDto MovieLessDetail(this Movie movie)
    {
        return new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title ?? "no title",
            Year = movie.Year ?? "no year",
            Image = movie.Image ?? "",
            Rating = movie.Rating ?? "no rating"
        };
    }
}
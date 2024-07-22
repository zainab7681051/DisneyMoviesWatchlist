using DisneyMoviesWatchlist.Src.Models;

namespace DisneyMoviesWatchlist.Src.Repository;
public interface IMovieRepository
{
    List<MovieDto> GetAll(string query, int pageNumber, out bool islastPage);
    Movie GetOne(int MovieId);
    List<MovieDto> GetWatchList(string UserId);
    void AddToWatchList(string UserId, int MovieId);
    void RemoveFromWatchList(string UserId, int MovieId);
    bool IsInWatchList(string UserId, int MovieId);

}
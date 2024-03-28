using DisneyMoviesWatchlist.Src.Extensions;
using DisneyMoviesWatchlist.Src.Models;

namespace DisneyMoviesWatchlist.Src.Repository;
public interface IMovieRepository
{
    List<MovieDto> GetAll(string query);
    Movie GetOne(int id);
    List<MovieDto> GetWatchList(string UserId);
    void AddToWatchList(string UserId, int id);
    void RemoveFromWatchList(string UserId, int id);
    bool IsInWatchList(string UserId, int id);

}
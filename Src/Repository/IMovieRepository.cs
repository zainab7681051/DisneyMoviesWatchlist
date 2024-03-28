using DisneyMoviesWatchlist.Src.Extensions;
using DisneyMoviesWatchlist.Src.Models;

namespace DisneyMoviesWatchlist.Src.Repository;
public interface IMovieRepository
{
    public List<MovieDto> GetAll(string query = null);
    public Movie GetOne(int id);
}
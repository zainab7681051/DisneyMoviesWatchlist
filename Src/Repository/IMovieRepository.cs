using DisneyMoviesWatchlist.Src.Extensions;
using DisneyMoviesWatchlist.Src.Models;

namespace DisneyMoviesWatchlist.Src.Repository;
public interface IMovieRepository
{
    public List<MovieDto> GetAll();
    public Movie GetOne(int id);
}
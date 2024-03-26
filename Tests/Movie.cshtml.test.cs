using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DisneyMoviesWatchlist.Src.DatabaseContext;
using DisneyMoviesWatchlist.Src.Pages;
namespace DisneyMoviesWatchlist.Tests;
public class MovieModelTest
{
    [Fact]
    public void OnGet_ValidId_LoadsMovie()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<MovieModel>>();
        var dbContextMock = new Mock<DisneyMoviesDbContext>();
        var userManagerMock = new Mock<UserManager<IdentityUser>>();
        var movieModel = new MovieModel(loggerMock.Object, dbContextMock.Object, userManagerMock.Object);
        var movie = new Movie { MovieId = 1, Title = "Snow White and the Seven Dwarfs" };
        dbContextMock.Setup(m => m.DisneyMovies.Find(It.IsAny<int>())).Returns(movie);

        // Act
        movieModel.OnGet(1);

        // Assert
        Assert.Equal(movie.MovieId, movieModel.DisneyMovie.MovieId);
        Assert.Equal(movie.Title, movieModel.DisneyMovie.Title);

    }
}
using Microsoft.AspNetCore.Identity;
using Xunit;
using Moq;
using DisneyMoviesWatchlist.Src.Pages;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;

namespace DisneyMoviesWatchlist.Tests.Pages;

public class WatchlistModelTest
{
    private readonly Mock<UserManager<IdentityUser>> userManagerMock;
    private readonly Mock<IMovieRepository> movieRepoMock;
    public WatchlistModelTest()
    {
        // Initialize mocks
        movieRepoMock = new();
        userManagerMock = new(
            Mock.Of<IUserStore<IdentityUser>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
    }

    [Fact]
    public void OnGet_Should_Set_Movies()
    {
        // Arrange
        movieRepoMock.Setup(r => r.GetWatchList(It.IsAny<string>())).Returns(new List<MovieDto>());
        var watchlistModel = new WatchlistModel(
            movieRepoMock.Object,
            userManagerMock.Object);
        // Act
        watchlistModel.OnGet();
        // Assert
        Assert.NotNull(watchlistModel.Movies);
    }
}

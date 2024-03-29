using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using DisneyMoviesWatchlist.Src.Pages;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;

namespace DisneyMoviesWatchlist.Tests.Pages;

public class IndexModelTest
{
    private readonly Mock<UserManager<IdentityUser>> userManagerMock;
    private readonly Mock<IMovieRepository> movieRepoMock;
    public IndexModelTest()
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
        movieRepoMock.Setup(r => r.GetAll(It.IsAny<string>())).Returns(new List<MovieDto>());
        var indexmodel = new IndexModel(
            movieRepoMock.Object,
            userManagerMock.Object);
        // Act
        indexmodel.OnGet();
        // Assert
        Assert.NotNull(indexmodel.Movies);
    }
}

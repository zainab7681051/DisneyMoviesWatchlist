using Microsoft.AspNetCore.Identity;
using Xunit;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using DisneyMoviesWatchlist.Src.Pages;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;

namespace DisneyMoviesWatchlist.Tests.Pages;

public class IndexModelTest
{
    private readonly Mock<UserManager<IdentityUser>> userManagerMock;
    private readonly Mock<IMovieRepository> movieRepoMock;
    private readonly Mock<IMemoryCache> memoryCache;
    private readonly IndexModel indexModel;
    public IndexModelTest()
    {
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
        memoryCache = new();
        indexModel = new(movieRepoMock.Object, userManagerMock.Object, memoryCache.Object);
    }

    [Fact]
    public void OnGet_Should_Set_Movies()
    {

        // Arrange
        movieRepoMock.Setup(r => r.GetAll(It.IsAny<string>())).Returns(new List<MovieDto>());
        // Act
        indexModel.OnGet();
        // Assert
        Assert.NotNull(indexModel.Movies);
    }
}

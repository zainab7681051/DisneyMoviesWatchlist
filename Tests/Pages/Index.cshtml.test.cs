using Microsoft.AspNetCore.Identity;
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

        indexModel = new(movieRepoMock.Object, userManagerMock.Object);
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
    [Fact]
    public void OnPostAdd_AddsMovieToWatchList()
    {
        // Arrange
        movieRepoMock.Setup(repo => repo.AddToWatchList(It.IsAny<string>(), It.IsAny<int>()));
        userManagerMock.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns("userId");

        // Act
        var result = indexModel.OnPostAdd(1) as Microsoft.AspNetCore.Mvc.RedirectToPageResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("/home#1", result.PageName);
    }
}

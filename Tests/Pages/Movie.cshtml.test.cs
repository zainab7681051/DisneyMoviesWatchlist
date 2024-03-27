using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using Moq.EntityFrameworkCore;
using DisneyMoviesWatchlist.Src.DatabaseContext;
using DisneyMoviesWatchlist.Src.Pages;

namespace DisneyMoviesWatchlist.Tests.Pages;

public class MovieModelTest
{
    private readonly Mock<ILogger<MovieModel>> loggerMock;
    private readonly Mock<DisneyMoviesDbContext> contextMock;
    private readonly Mock<UserManager<IdentityUser>> userManagerMock;

    public MovieModelTest()
    {
        // Initialize mocks
        loggerMock = new Mock<ILogger<MovieModel>>();
        contextMock = new Mock<DisneyMoviesDbContext>();
        userManagerMock = new Mock<UserManager<IdentityUser>>(
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
    public void OnGet_Should_Set_DisneyMovie()
    {
        // Arrange
        var movies = FakeMovies.FakeMoviesList;
        var setMock = new Mock<DbSet<Movie>>().SetupData(movies);
        contextMock.Setup(x => x.DisneyMovies).Returns(setMock.Object);

        var movieModel = new MovieModel(
            loggerMock.Object,
            contextMock.Object,
            userManagerMock.Object);

        // Act
        movieModel.OnGet(1);

        // Assert
        Assert.NotNull(movieModel.DisneyMovie);
        Assert.Equal(1, movieModel.DisneyMovie.MovieId);
        Assert.Equal("Movie1", movieModel.DisneyMovie.Title);
    }
}

public static class FakeMovies
{
    public static readonly List<Movie> FakeMoviesList = new(){
        new Movie{
            MovieId = 1,
            Title = "Movie1",
            Runtime = "",
            Year = "",
            Summary = "",
            Rating = "",
            Metascore = "",
            Link = "",
            Image = "",
            Genre = "",
            Directors = "",
            Stars = ""

        },
        new Movie{

            MovieId = 2,
            Title = "Movie2",
            Runtime = "",
            Year = "",
            Summary = "",
            Rating = "",
            Metascore = "",
            Link = "",
            Image = "",
            Genre = "",
            Directors = "",
            Stars = ""
        }
    };
}

public static class DbSetMockExtensions
{
    public static Mock<DbSet<TEntity>> SetupData<TEntity>(this Mock<DbSet<TEntity>> mockSet, List<TEntity> data) where TEntity : class
    {
        var queryable = data.AsQueryable();
        mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        return mockSet;
    }
}
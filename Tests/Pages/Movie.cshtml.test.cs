using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using DisneyMoviesWatchlist.Src.DatabaseContext;
using DisneyMoviesWatchlist.Src.Pages;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DisneyMoviesWatchlist.Tests.Pages
{
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
            var movieId = 1;
            var movieTitle = "Movie1";
            var movie = new Movie
            {
                MovieId = movieId,
                Title = movieTitle,
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
            };
            var movies = new List<Movie> { movie };
            var mockSet = new Mock<DbSet<Movie>>().SetupData(movies);
            contextMock.Setup(c => c.DisneyMovies).Returns(mockSet.Object);
            var movieModel = new MovieModel(
                loggerMock.Object,
                contextMock.Object,
                userManagerMock.Object);

            // Act
            movieModel.OnGet(movieId);

            // Assert
            Assert.NotNull(movieModel.DisneyMovie);
            Assert.Equal(movieId, movieModel.DisneyMovie.MovieId);
            Assert.Equal(movieTitle, movieModel.DisneyMovie.Title);
        }
    }

    public static class DbSetExtensions
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
}

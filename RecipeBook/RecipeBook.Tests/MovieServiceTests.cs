﻿using Castle.Core.Logging;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using RecipeBook.BL.Services;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;

namespace RecipeBook.Tests
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly Mock<IActorRepository> _actorRepositoryMock;

        private List<Actor> _actors = new List<Actor>
        {
            new Actor()
            {
                Id = "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                Name = "Actor 1"
            },
            new Actor()
            {
                Id = "baac2b19-bbd2-468d-bd3b-5bd18aba98d7",
                Name = "Actor 2"
            },
            new Actor()
            {
                Id = "5c93ba13-e803-49c1-b465-d471607e97b3",
                Name = "Actor 3"
            },
        };

        private List<Movie> _movies = new List<Movie>()
        {
            new Movie()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Movie 1",
                Year = 2021,
                Actors = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "baac2b19-bbd2-468d-bd3b-5bd18aba98d7"]
            },
            new Movie()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Movie 2",
                Year = 2022,
                Actors = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "5c93ba13-e803-49c1-b465-d471607e97b3"
                ]
            }
        };

        public MovieServiceTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _actorRepositoryMock = new Mock<IActorRepository>();
        }

        [Fact]
        void GetById_Ok()
        {
            //Arrange
            var movieId = _movies[0].Id;
            var actorId = _actors[0].Id;

            _movieRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _movies.FirstOrDefault(m => m.Id == id));

            _actorRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _actors.FirstOrDefault(m => m.Id == id));

            var loggerMock = new Mock<ILogger<MoviesService>>();
            ILogger<MoviesService> logger = loggerMock.Object;

            //Act
            var movieService = new MoviesService(
                _movieRepositoryMock.Object,
                logger,
                _actorRepositoryMock.Object);

            var result = movieService.GetById(movieId);
            var result2 = movieService.GetById(actorId);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result2);
            Assert.Equal(movieId, result.Id);
            Assert.Equal(actorId, result2.Id);
        }

        [Fact]
        void GetById_NotExistingId()
        {
            //Arrange
            var movieId = Guid.NewGuid().ToString();
            var actorId = Guid.NewGuid().ToString();

            _movieRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _movies.FirstOrDefault(m => m.Id == id));

            _actorRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _actors.FirstOrDefault(m => m.Id == id));

            var loggerMock = new Mock<ILogger<MoviesService>>();
            ILogger<MoviesService> logger = loggerMock.Object;

            //Act
            var movieService = new MoviesService(
                _movieRepositoryMock.Object,
                logger,
                _actorRepositoryMock.Object);

            var result = movieService.GetById(movieId);
            var result2 = movieService.GetById(actorId);

            //Assert
            Assert.Null(result);
            Assert.Null(result2);
        }

        [Fact]
        void GetById_WrongGuidId()
        {
            //Arrange
            var movieId = "zsdfsd";
            var actorId = "asgsagtd";

            _movieRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _movies.FirstOrDefault(m => m.Id == id));

            _actorRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _actors.FirstOrDefault(m => m.Id == id));

            var loggerMock = new Mock<ILogger<MoviesService>>();
            ILogger<MoviesService> logger = loggerMock.Object;

            //Act
            var movieService = new MoviesService(
                _movieRepositoryMock.Object,
                logger,
                _actorRepositoryMock.Object);

            var result = movieService.GetById(movieId);
            var result2 = movieService.GetById(actorId);

            //Assert
            Assert.Null(result);
            Assert.Null(result2);
        }
    }
}

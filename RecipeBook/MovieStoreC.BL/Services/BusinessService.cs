﻿using RecipeBook.BL.Interfaces;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.Responses;

namespace RecipeBook.BL.Services
{
    internal class BusinessService : IBusinessService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public BusinessService(
            IMovieRepository movieRepository,
            IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public List<MovieFullDetailsResponse> GetAllMovies()
        {
            var result = new List<MovieFullDetailsResponse>();

            var movies = _movieRepository.GetAll();

            foreach (var movie in movies)
            {
                var detailedMovie = new MovieFullDetailsResponse()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Year = movie.Year
                };

                foreach (var actorId in movie.Actors)
                {
                    var actor = _actorRepository.GetById(actorId);
                    if (actor == null) continue;
                    detailedMovie.Actors.Add(actor);
                }

                result.Add(detailedMovie);
            }

            return result;
        }
    }
}

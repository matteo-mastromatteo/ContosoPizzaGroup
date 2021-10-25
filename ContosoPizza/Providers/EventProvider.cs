using System.Collections.Generic;
using System.Linq;
using ContosoPizza.Abstraction;
using ContosoPizza.Models;

namespace ContosoPizza.Providers
{
    public class EventProvider: IEventProvider
    {
        private readonly IMovieRatingProvider _movieRatingProvider;

        public EventProvider(IMovieRatingProvider movieRatingProvider)
        {
            _movieRatingProvider = movieRatingProvider;
        }

        public IEnumerable<Event> GetActiveEvents()
        {
            var events =  GetAllEvents();

            return ApplyRatings(events);
        }

        private IEnumerable<Event> ApplyRatings(IEnumerable<Event> events)
        {
            var movieRatings = _movieRatingProvider.GetMovieRatings(
                events.Where(e => e.Type == EventType.Movie)
                      .Select(m => m.Title));

            foreach (var rating in movieRatings)
            {
                var eventToRate = events.FirstOrDefault(e => e.Title == rating.Key);
                if (eventToRate != null)
                {
                    eventToRate.Rating = rating.Value;
                }
            }

            return events;
        }

        private static IEnumerable<Event> GetAllEvents()
        {
            var events = new List<Event>(){
                new Event(){
                    Id = 1,
                    Rating = 0,
                    Title = "Squid Game",
                    Type = EventType.Movie
                },
                new Event(){
                    Id = 2,
                    Rating = 0,
                    Title = "Narcos",
                    Type = EventType.Movie
                }
            };
            return events;
        }
    }
}
using System.Collections.Generic;

namespace ContosoPizza.Abstraction
{
    public interface IMovieRatingProvider
    {
        IDictionary<string, decimal> GetMovieRatings(IEnumerable<string> movieTitles);
    }
}
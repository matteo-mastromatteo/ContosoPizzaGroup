using System;
using System.Collections.Generic;
using System.Linq;
using ContosoPizza.Abstraction;

namespace ContosoPizza.Providers
{
    public class MovieRatingProvider: IMovieRatingProvider
    {
        private Dictionary<string, decimal> _ratings = null;
        public IDictionary<string, decimal> GetMovieRatings(IEnumerable<string> movieTitles)
        {
            var random = new Random();

            if (_ratings == null)
            {
                var ratings = movieTitles
                    .Distinct()
                    .Select(title => new KeyValuePair<string, decimal>(title, (decimal)random.Next(10, 50) / 10));
                _ratings = new Dictionary<string, decimal>(ratings);
            }

            return _ratings;
        }
    }
}
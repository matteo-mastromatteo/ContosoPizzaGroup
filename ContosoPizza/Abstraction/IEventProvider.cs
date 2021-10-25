using System.Collections.Generic;
using ContosoPizza.Models;

namespace ContosoPizza.Abstraction
{
    public interface IEventProvider
    {
        IEnumerable<Event> GetActiveEvents();
    }
}
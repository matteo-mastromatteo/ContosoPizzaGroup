using ContosoPizzaService.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoPizza.Abstraction
{
    public interface IPizzaServiceFactory
    {
        IPizzaService GetPizzaService(string pizzaServiceName);
    }
}

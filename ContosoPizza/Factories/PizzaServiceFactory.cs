using ContosoPizza.Abstraction;
using ContosoPizzaService.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoPizza.Factories
{
    public class PizzaServiceFactory : IPizzaServiceFactory
    {
        protected readonly IEnumerable<IPizzaService> _pizzaServices;

        public PizzaServiceFactory(IEnumerable<IPizzaService> pizzaServices)
        {
            _pizzaServices = pizzaServices;
        }

        public IPizzaService GetPizzaService(string pizzaServiceName)
        {
            var ps = _pizzaServices.Where(t => t.Name.ToLower() == pizzaServiceName.ToLower()).FirstOrDefault();
            if (ps == null)
            {
                throw new TypeLoadException($"Il tipo {pizzaServiceName} non è stato trovato.");
            }
            return ps;
        }
    }
}

using ContosoPizza.Entities.Models;
using ContosoPizzaService.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace ContosoPizza.Services
{
    public class MemoryPizzaService : IPizzaService
    {
        public string Name {get return "MemoryPizzaService"};
        protected virtual List<Pizza> Pizzas { get; set; }
        protected virtual int nextId { get; set; } = 3;
        public MemoryPizzaService()
        {
            Pizzas = new List<Pizza>
            {
                new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
                new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
            };
        }

        public List<Pizza> GetAll() => Pizzas;

        public Pizza Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

        public void Add(Pizza pizza)
        {
            pizza.Id = nextId++;
            Pizzas.Add(pizza);
        }

        public void Delete(int id)
        {
            var pizza = Get(id);
            if(pizza is null)
                return;

            Pizzas.Remove(pizza);
        }

        public void Update(Pizza pizza)
        {
            var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
            if(index == -1)
                return;

            Pizzas[index] = pizza;
        }
    }
}
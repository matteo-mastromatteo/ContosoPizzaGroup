using ContosoPizza.Entities.Models;
using ContosoPizzaService.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoPizzaService.MemoryPizzaService
{
    public class MemoryPizzaService : IPizzaService
    {
        protected readonly IConfiguration _Configuration;
        private MemoryPizzaServiceConfiguration _settings;

        public string Name => "MemoryPizzaService";

        private void LoadSettings()
        {
            _settings = new MemoryPizzaServiceConfiguration();

            var section = _Configuration.GetSection(Name + "Configuration");
            if (!section.Exists())
            {
                throw new ArgumentNullException($"Manca la configurazione {Name}Configuration");
            }

            section.Bind(_settings);
        }

        protected virtual List<Pizza> Pizzas { get; set; }
        protected virtual int nextId { get; set; } = 3;
        public MemoryPizzaService(IConfiguration configuration)
        {
            _Configuration = configuration;

            LoadSettings();

            if (_settings.InitializeDefaultPizzas)
            {
                Pizzas = new List<Pizza>
                {
                    new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
                    new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
                };
            }
            else 
            { 
                Pizzas = new List<Pizza>();
            }
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
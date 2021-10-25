using System;
using System.Collections.Generic;
using ContosoPizza.Entities.Models;

namespace ContosoPizzaService.Abstraction
{
    public interface IPizzaService
    {
        string Name { get; }

        List<Pizza> GetAll();

        Pizza Get(int id);

        void Add(Pizza pizza);

        void Delete(int id);

        void Update(Pizza pizza);
    }
}

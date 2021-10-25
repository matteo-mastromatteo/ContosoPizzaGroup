using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using ContosoPizzaService.Abstraction;

namespace ContosoPizza.Helpers
{
    public static class PizzaServiceLoader
    {
        public static void AddPizzaServiceImplementations(this IServiceCollection services, string pizzaServiceName)
        {
            var typ = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(f => Assembly.LoadFrom(f)).ToList()
                .SelectMany(a => a.GetTypes()
                    .Where(t => typeof(IPizzaService).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface && t.IsClass && t.IsPublic && t.Name.ToLower() == pizzaServiceName.ToLower())
                )
                .FirstOrDefault();

            if (typ != null)
            {
                services.AddSingleton(typeof(IPizzaService), typ);
            }
            else
            {
                throw new TypeLoadException($"Il tipo {pizzaServiceName} non è stato trovato in nessuno degli assembly presenti.");
            }
        }
    }
}

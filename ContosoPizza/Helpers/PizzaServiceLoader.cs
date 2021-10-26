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
        public static void AddPizzaServiceImplementations(this IServiceCollection services)
        {
            var types = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(f => Assembly.LoadFrom(f)).ToList()
                           .SelectMany(a => a.GetTypes()
                               .Where(t => typeof(IPizzaService).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface && t.IsClass && t.IsPublic)
                           )
                           .ToList();

            if (types.Count == 0)
            {
                throw new TypeLoadException($"Non è stato trovato alcuna implementazione per IPizzaService.");
            }

            types.ForEach(t => services.AddSingleton(typeof(IPizzaService), t));
        }
    }
}

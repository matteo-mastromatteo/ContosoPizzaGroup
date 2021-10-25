using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizzaService.Abstraction;
using ContosoPizza.Entities.Models;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        protected readonly IPizzaService _PizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            _PizzaService = pizzaService;
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() =>
            _PizzaService.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = _PizzaService.Get(id);

            if(pizza == null)
                return NotFound();

            return pizza;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {            
            _PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = _PizzaService.Get(id);
            if(existingPizza is null)
                return NotFound();

            _PizzaService.Update(pizza);           

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = _PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            _PizzaService.Delete(id);

            return NoContent();
        }
    }
}
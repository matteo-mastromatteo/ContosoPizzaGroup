using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Http;
using System;
using ContosoPizza.Abstraction;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController  : ControllerBase
    {
        
        private readonly IEventProvider _eventProvider;
        private IServiceProvider _serviceProvider;

        public EventsController(IEventProvider eventProvider, IServiceProvider serviceProvider)
        {
            _eventProvider = eventProvider;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IMovieRatingProvider mrp = (IMovieRatingProvider) _serviceProvider.GetService(typeof(IMovieRatingProvider));
                var dic = mrp.GetMovieRatings(new List<string>(){"Pippo"});
                return new JsonResult(new { eventi = _eventProvider.GetActiveEvents(), dic = dic });
            }
            catch (Exception)
            {
                // logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
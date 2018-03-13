using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PokeInfo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        // now get the pokemon info from the api call
        //NEED TO ENTER AS SUCH:   http://localhost:5000/pokemon/55
        // GET: 
        [HttpGet]
        [Route("pokemon/{pokeid}")]
        public IActionResult GetPokemon(int pokeid)
        {
            var CurrentPokemon = new Pokemon();
           
                WebRequest.GetPokemonDataAsync(pokeid, ApiResponse =>
                {
                    CurrentPokemon = ApiResponse;
                    Console.WriteLine("=====Current Pokemon: "+ CurrentPokemon);
                }
            ).Wait();
            Console.WriteLine("Waiting ====== Waiting===");
            ViewBag.Pokemon = CurrentPokemon;
            return View("Index");
        }
    }
}

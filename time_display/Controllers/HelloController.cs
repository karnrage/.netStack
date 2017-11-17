// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace timeDisplay.Controllers
{
    public class HelloController : Controller
    {
       
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }
                      
    }
}


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace timedisplay.Controllers
{
    public class ClockController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("index");
        }
    }
}

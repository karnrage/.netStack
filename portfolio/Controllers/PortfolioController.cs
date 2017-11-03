using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace portfolio.Controllers
{
    public class PortfolioController : Controller
    {
        [HttpGet]
        [Route("/Home")]
        public IActionResult home()
        {
            return View();
        }

        [HttpGet]
        [Route("Projects")]
        public IActionResult Projects()
        {
            return View("Projects");
        }

        [HttpGet]
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View("Contact");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// below: my additions
using DbConnection;

namespace repeatQuoting.Controllers
{
    public class HomeController : Controller
    {
        private DbConnector cnx;
        public HomeController(){
            cnx = new DbConnector();
        }
        // GET: /Home/
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/quotes")]
        public IActionResult postQuote(string Name, string quote)
        {
        string query = $"INSERT INTO quotes (name, quote, created_at) VALUES ('{Name}','{quote}', NOW())";
        DbConnector.Execute(query);
        return Redirect("/quotes");
          
        }
        [HttpGet]
        [Route("/Quotes")]
        public IActionResult Quotes()
        {
        string query="SELECT * FROM quotes ORDER BY created_at DESC";
        var AllQuotes = DbConnector.Query(query);
        ViewBag.allQuotes = AllQuotes;
        
        return View("Quotes");
          
        }
    }
}

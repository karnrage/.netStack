using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//my added using statements
// using quotingCheck.Connectors;
using DbConnection;

namespace quotingCheck.Controllers
{
    public class HomeController : Controller
    {
        // private readonly DbConnector _dbConnector;
        private DbConnector cnx;

        // public HomeController(DbConnector connect)
        public HomeController(){
            cnx = new DbConnector();
        }
        // {
        //     _dbConnector = connect;
        // }
        // GET: /Home/
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            
            
            //maybe above code needs to be modifed. Make sure that table is called "users"
            return View();
        }

        [HttpPost]
        [Route("/quotes")]
        //inside function need parameters that were taking in in html
        public IActionResult postQuote(string Name, string quote)
        {
            //need to take information in and save to the db. do this by declaring a variable and 
            //running the "INSERT" into query
            string query = $"INSERT INTO quotes (name, quote, created_at) VALUES ('{Name}','{quote}', NOW())";
            DbConnector.Execute(query);

            return Redirect("/quotes");



        }

        [HttpGet]
        [Route("/Quotes")]
        public IActionResult Quotes()
        {
            //need to run a query to select all the data from the database
            // List<Dictionary<string, object>> allQuotes = DbConnector.Query("SELECT * FROM quotes ORDER BY created_at DESC");
            string query="SELECT * FROM quotes ORDER BY created_at DESC";
            var AllQuotes = DbConnector.Query(query);
            ViewBag.allQuotes = AllQuotes;
            
            return View("Quotes");

            //need to redirectToAction if putting something in ()that
            // is not what is the name of the IActionResult

        }
    }
}

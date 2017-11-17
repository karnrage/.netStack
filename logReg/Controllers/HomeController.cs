using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//
using logReg.Models;
using Newtonsoft.Json;

namespace logReg.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.errors = TempData["Error"];
            return View();
        }

        [HttpPost]
        [Route("register")]
        // public IActionResult User(string first_name, string last_name, string email, string password, string password_confirm) why not this way?
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                //taking values submitted in form and creating a query to sent info to the Db
                string query = $"INSERT INTO users (first_name, last_name, email, password, created_at)"
                + "VALUES ('{user.first_name}','{user.last_name}','{user.email}', '{user.password}', NOW())";
                //
                _dbConnector.Execute(query);

                //once registered, and entered into db, take back from db and use in session
                string saveUserToSession = $"SELECT email, password, first_name FROM users WHERE email = '{user.email}'";
                var theUser = _dbConnector.Query(saveUserToSession);
                SessionExtensions.SetObjectAsJson(HttpContext.Session, "UserSession", theUser[0]);
                return RedirectToAction("Success");

            }
            return View("Index", user);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password){
            TempData["Error"] = null;

            Login theLogin = new Login{
                Email = Email,
                Password = Password

        };

        if(TryValidateModel(theLogin))
            {
                string query = $"SELECT email, password, first_name FROM users WHERE email = '{theLogin.Email}'";
                var theUser = _dbConnector.Query(query);
                if(theUser.Count == 0){
                    //verifing if email is in database
                    string[] message = {"Email not in database"};
                    TempData["Error"] = message;
                    return RedirectToAction("Index");
                    //if email exists in database verifing if password is correct
                } else if(Password != (string)theUser[0]["password"]){
                    string[] message = {"Password didn't match"};
                    TempData["Error"] = message;
                    return RedirectToAction("Index");
                } else{
                    // Setting the session as an Json String to desirialize it later
                    SessionExtensions.SetObjectAsJson(HttpContext.Session,"UserSession", theUser[0]);
                    return RedirectToAction("Welcome");
                }
            } else{
                List<string> theErrors = new List<string>();
                foreach(var error in ModelState.Values){
                    if(error.Errors.Count > 0){

                        theErrors.Add(error.Errors[0].ErrorMessage.ToString());
                    }
                }
                TempData["Error"] = theErrors;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("Success")]
        public IActionResult Success()
        {
            // Attaching all the properties of the query to an Object of User
            User theUser = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session, "UserSession");
            // Querying for all the users except the on in session
            string query = $"SELECT * FROM users WHERE NOT email = '{theUser.email}' ORDER BY created_at DESC";
            var AllUsers = _dbConnector.Query(query);
            // string theUser = HttpContext.Session.GetString("UserSession");

            ViewBag.theUser = theUser;
            ViewBag.allUsers = AllUsers;
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

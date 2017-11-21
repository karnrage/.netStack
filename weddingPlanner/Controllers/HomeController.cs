using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//my adds
using Microsoft.EntityFrameworkCore;
using weddingPlanner.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;

namespace weddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private weddingPlannerContext _context;

        public HomeController(weddingPlannerContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        //below new instance of a class that I modeled my form out

        //need to call in wrapper
        //when assigning use dot function to move through wrapper and into "wrapped" class
        public IActionResult Register(LoginRegViewModel modelnew)
        {
            TempData["UserError"] = null;
            // TryValidateModel(modelnew);
            // {   
            //using ASP validations you only need to use below and not above tryValidateModl
            //DO NOT HAVE SEMICOLIN AFTER IF CHECK!!!!!!will kill condition and move into braces
            if (ModelState.IsValid)
            {
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                modelnew.registerVM.password = Hasher.HashPassword(modelnew.registerVM, modelnew.registerVM.password);
                user NewUser = new user
                {
                    //model attribute name = object name.
                    //through the parameter way: it grabs the input name through html.
                    //however done this way the name has to be the same as the object.
                    firstName = modelnew.registerVM.firstName,
                    // System.Console.WriteLine(firstName);
                    lastName = modelnew.registerVM.lastName,
                    email = modelnew.registerVM.email,
                    password = modelnew.registerVM.password,
                    createAt = DateTime.Now,
                };

                _context.users.Add(NewUser);// middle word is supposed to match up with table name
                _context.SaveChanges();
                //with successful registration save current user to session
                user helloUser = _context.users.SingleOrDefault(user => user.email == modelnew.registerVM.email);
                HttpContext.Session.SetInt32("ActiveId", helloUser.userID);
                HttpContext.Session.GetInt32("ActiveId");
                // HttpContext.Session.SetString("UserName", modelnew.firstName);

                return RedirectToAction("Success");


            }
            return View("Index", modelnew);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(LoginRegViewModel logUser)
        {
            TempData["UserError"] = null;
            user tryUser = _context.users.SingleOrDefault(attempt => attempt.email == logUser.loginVM.email);
            {
                if (tryUser == null)
                {
                    TempData["Error"] = "Email not found, please register";
                    return View("Index", logUser);
                }
                else
                {
                    if (logUser.loginVM.email != tryUser.email)
                    {
                        TempData["Error"] = "Username or Password is incorrect";
                        return View("Index", logUser);
                    }
                    else
                    {
                        PasswordHasher<user> Hasher = new PasswordHasher<user>();
                        if (0 == Hasher.VerifyHashedPassword(tryUser, tryUser.password, logUser.loginVM.password))
                        {
                            TempData["Error"] = "Username or Password is incorrect";
                            return View("Index", logUser);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("ActiveId", tryUser.userID);
                            return RedirectToAction("Success");
                        }
                    }


                }
                // return View("Index", tryUser);
            }
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        // above is case senstive to cshtml
        {
            //optional: set to variables, could directly set to viewbag
            int? userID = HttpContext.Session.GetInt32("ActiveId");
            user helloUser = _context.users.SingleOrDefault(user => user.userID == userID);

            //why do we want a list?
            //stacked below, could make into a single line
            List<wedding> allWeddings = _context.weddings
                                        .Include(w => w.guests)
                                        .ThenInclude(g => g.user)
                                        .ToList();

            //whatever variables set above, set/send those to ViewBag
            System.Console.WriteLine(helloUser);
           

            ViewBag.helloUser = helloUser;
            ViewBag.allWeddings = allWeddings;
            // only fulfill with post to pass data along
            return View();

        }
        


        //to show the new wedding page
        [HttpGet]
        //name page and post collection different names
        [Route("wedPlanner")]
        public IActionResult wedPlanner()
        //name of IActionResult needs to be the name of the html file

        // above is case senstive to cshtml
        {

            return View();

        }
        //to collect the new wedding data
        [HttpPost]
        //name page and post collection different names
        [Route("CreateWed")]
        public IActionResult CreateWed(wedding wedding)
        // above is case senstive to cshtml
        {
            int? userID = HttpContext.Session.GetInt32("ActiveId");
            ViewBag.userID = userID;
            user thisUser = _context.users.SingleOrDefault(user => user.userID == userID);

            if (userID == null)
            {
                return RedirectToAction("Index");
            }
            if (wedding.wedDate < DateTime.Now){
                TempData["DateError"] = "You can't get married in the past";
                return View("wedPlanner", wedding);
            }
            if (ModelState.IsValid)
            {
                wedding newWedding = new wedding
                {
                    husband = wedding.husband,
                    wife = wedding.wife,
                    address = wedding.address,
                    wedDate = wedding.wedDate,

                    userId = thisUser.userID,
                    createAt = DateTime.Now,
                };
                _context.Add(newWedding);
                _context.SaveChanges();
                //not error handling here, so bad idea to return view

                var lastWed = _context.weddings.OrderByDescending(s => s.wedID) 
                         .FirstOrDefault();
                        //  s => s.Site_Id == this.SiteIdentifier

                return RedirectToAction("wedDetail", new{wedID = lastWed});
                //
            }
            return View("wedPlanner", wedding);
        }


        // [HttpPost]
        // [Route("wedPlanner")]
        // public IActionResult wedPlanner()
        // // above is case senstive to cshtml
        // {

        //     return View("wedPlanner");

        // }




        [HttpGet]
        //*****3.{wedID} was taking in from the <a href= > on the html
        //*****2.{wedID} was given to html from viewbag
        //*****1.whole object was passed to html.{wedID} is a column/property of that object 
        [Route("wedDetail/{wedID}")]
        public IActionResult wedDetail(int wedID)
        // above is case senstive to cshtml
        {   System.Console.WriteLine(wedID);

            // wedding lastWed = _context.weddings.OrderByDescending(s => s.wedID) 
            //     .FirstOrDefault();

            //whole object does NOT get collected. need to use include to capture additional objects
            wedding thisWed = _context.weddings.Where(s => s.wedID == wedID).Include(i => i.guests).ThenInclude(i=>i.user).SingleOrDefault();
        
            // need to create query that gets all the guests, and the user, from the current wedding ID
            // wedding lastEntered = _context.weddings.Include(i => i.guests).ThenInclude(i=>i.user).SingleOrDefault(item => item.wedID == (int)lastWed.wedID);
            // wedding lastEntered = _context.weddings.Orderby created at .take(1)
            
            // System.Console.WriteLine("lastEntered");

            // TempData["name"] = $"Wedding of {lastEntered.husband} & {lastEntered.wife}";
            // TempData["date"] = $"Date of Wedding {lastEntered.wedDate}";
            // TempData not supposed to passdata to frontend

            ViewBag.Wedding = thisWed;

            return View();

        }

        [HttpGet]
        [Route("delete/{wedID}")]
        public IActionResult delete(int wedID)
        {
            wedding deleteWed = _context.weddings.SingleOrDefault(x => x.wedID == wedID);
           

            _context.weddings.Remove(deleteWed);
            _context.SaveChanges();

            return RedirectToAction("success");

        }

        [HttpGet]
        [Route("rsvp/{wedID}")]
        public IActionResult rsvp(int wedID)
        {
            int? userID = HttpContext.Session.GetInt32("ActiveId");
            ViewBag.userID = userID;

            user thisUser = _context.users.SingleOrDefault(user => user.userID == (int)userID);
            wedding thisWed = _context.weddings.Where(s => s.wedID == wedID).SingleOrDefault();

            System.Console.WriteLine((int)userID);
            System.Console.WriteLine(wedID);

            guest reserved = new guest{
                //guest ID was set here before was 
                userID = thisUser.userID,
                wedID = thisWed.wedID,
            };

            _context.Add(reserved);
            _context.SaveChanges();

            return RedirectToAction("success");

        }

        [HttpGet]
        [Route("decline/{wedId}")]
        public IActionResult decline(int wedID)
        {
           
            int? userID = HttpContext.Session.GetInt32("ActiveId");
            ViewBag.userID = userID;

            guest decline = _context.guests
            .SingleOrDefault(guest => guest.userID == userID && guest.wedID == wedID );
            
            _context.guests.Remove(decline);
            _context.SaveChanges();

            return RedirectToAction("success");
        }



        // // above is case senstive to cshtml
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");

        }
    }
}
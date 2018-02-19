using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using trialrun.Models;
using trialrun.Factory;
using trialrun.ActionFilters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace trialrun.Controllers
{
    public class HomeController : Controller
    {
        // ########## ROUTES ##########
        //  /
        //  /(add_routes_guide)
        //  /
        // ########## ROUTES ##########

        // Dapper connections
        // private readonly UserFactory UserFactory;
        // private readonly DbConnector _dbConnector;

        // Entity PostGres Code First connection
        private TrialrunContext _context;

        public HomeController(TrialrunContext context)
        {
            // Dapper framework connections
            // _dbConnector = connect;
            // UserFactory = new UserFactory();

            // Entity Framework connections
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        [ImportModelState]
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
                User NewUser = new User
                {
                    //model attribute name = object name.
                    //through the parameter way: it grabs the input name through html.
                    //however done this way the name has to be the same as the object.
                    firstName = modelnew.registerVM.firstName,
                    // System.Console.WriteLine(firstName);
                    lastName = modelnew.registerVM.lastName,
                    username = modelnew.registerVM.username,
                    password = modelnew.registerVM.password,
                    createdAt = DateTime.Now,
                };

                _context.Users.Add(NewUser);// middle word is supposed to match up with table name
                _context.SaveChanges();
                //with successful registration save current User to session
                User helloUser = _context.Users.SingleOrDefault(User => User.username == modelnew.registerVM.username);
                HttpContext.Session.SetInt32("activeID", helloUser.UserID);
                HttpContext.Session.GetInt32("activeID");
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
            User tryUser = _context.Users.SingleOrDefault(attempt => attempt.username == logUser.loginVM.username);
            {
                if (tryUser == null)
                {
                    TempData["Error"] = "username not found, please register";
                    return View("Index", logUser);
                }
                else
                {
                    if (logUser.loginVM.username != tryUser.username)
                    {
                        TempData["Error"] = "Username or Password is incorrect";
                        return View("Index", logUser);
                    }
                    else
                    {
                        PasswordHasher<User> Hasher = new PasswordHasher<User>();
                        if (0 == Hasher.VerifyHashedPassword(tryUser, tryUser.password, logUser.loginVM.password))
                        {
                            TempData["Error"] = "Username or Password is incorrect";
                            return View("Index", logUser);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("activeID", tryUser.UserID);
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
            int? UserID = HttpContext.Session.GetInt32("activeID");
            User helloUser = _context.Users.FirstOrDefault(User => User.UserID == UserID);

            // List<Product> allProducts = _context.Products
            //                             // .Where(w => w.name)
            //                             // .ThenInclude(g => g.UserID)
            //                             // .ThenInclude(x => x.bid)
            //                             .ToList();

            
           

            ViewBag.helloUser = helloUser;
            // ViewBag.allAuctions = allProducts;
            
            // only fulfill with post to pass data along
            return View();

        }

        [HttpGet]
        //name page and post collection different names
        [Route("auctionPlanner")]
        public IActionResult auctionPlanner()
        //name of IActionResult needs to be the name of the html file

        // above is case senstive to cshtml
        {

            return View();

        }

        [HttpPost]
        //name page and post collection different names
        [Route("createProduct")]
        public IActionResult createProduct(Product product)
        // above is case senstive to cshtml
        {
            int? userID = HttpContext.Session.GetInt32("activeID");
            ViewBag.userID = userID;
            User helloUser = _context.Users.FirstOrDefault(user => user.UserID == userID);

            if (userID == null)
            {
                return RedirectToAction("Index");
            }
            if (product.endDate < DateTime.Now){
                TempData["DateError"] = "You can't Auction in the past";
                return View("auctionPlanner", product);
            }
            if (ModelState.IsValid)
            {
                Product newProduct = new Product
                {
                    bid = product.bid,
                    name = product.name,
                    description = product.description,
                    endDate = product.endDate,

                    UserID = helloUser.UserID,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                };
                _context.Add(newProduct);
                _context.SaveChanges();
                //not error handling here, so bad idea to return view

                var lastProduct = _context.Products.OrderByDescending(s => s.ProductID) 
                         .FirstOrDefault();
                        //  s => s.Site_Id == this.SiteIdentifier

                return RedirectToAction("Success", new{wedID = lastProduct});
                //
            }
            return View("auctionPlanner", product);
        }

        [HttpGet]
        //*****3.{wedID} was taking in from the <a href= > on the html
        //*****2.{wedID} was given to html from viewbag
        //*****1.whole object was passed to html.{wedID} is a column/property of that object 
        [Route("auctionDetail/{wedID}")]
        public IActionResult auctionDetail(int wedID)
        // above is case senstive to cshtml
        {   System.Console.WriteLine(wedID);

            // wedding lastWed = _context.weddings.OrderByDescending(s => s.wedID) 
            //     .FirstOrDefault();

            //whole object does NOT get collected. need to use include to capture additional objects
            Product thisProduct = _context.Products.Where(s => s.ProductID == wedID)
                                .Include(i => i.name)
                                
                                .SingleOrDefault();
        
            // need to create query that gets all the guests, and the user, from the current wedding ID
            // wedding lastEntered = _context.weddings.Include(i => i.guests).ThenInclude(i=>i.user).SingleOrDefault(item => item.wedID == (int)lastWed.wedID);
            // wedding lastEntered = _context.weddings.Orderby created at .take(1)
            
            // System.Console.WriteLine("lastEntered");

            // TempData["name"] = $"Wedding of {lastEntered.husband} & {lastEntered.wife}";
            // TempData["date"] = $"Date of Wedding {lastEntered.wedDate}";
            // TempData not supposed to passdata to frontend

            ViewBag.Wedding = thisProduct;

            return View();

        }

        [HttpGet]
        [Route("delete/{wedID}")]
        public IActionResult delete(int wedID)
        {
            Product deleteProduct = _context.Products.SingleOrDefault(x => x.ProductID == wedID);
           

            _context.Products.Remove(deleteProduct);
            _context.SaveChanges();

            return RedirectToAction("success");

        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");

        }

    }
}
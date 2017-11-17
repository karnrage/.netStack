using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//add by me
using Microsoft.EntityFrameworkCore;
using bankAccounts.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
// using System.Data.DataSetExtensions;

namespace bankAccounts.Controllers
{
    public class HomeController : Controller
    {
        private bankAccountContext _context;

        public HomeController(bankAccountContext context)
        {
            _context = context;
        }

        //Routes starting below--------------------------------

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
                    createTime = DateTime.Now,
                };

                _context.users.Add(NewUser);// middle word is supposed to match up with table name
                _context.SaveChanges();
                //with successful registration save current user to session
                user helloUser = _context.users.SingleOrDefault(user => user.email == modelnew.registerVM.email);
                HttpContext.Session.SetInt32("ActiveId", helloUser.userid);
                HttpContext.Session.GetInt32("ActiveId");
                // HttpContext.Session.SetString("UserName", modelnew.firstName);

                return RedirectToAction("Success");


            }


            // }
            return View("Index", modelnew);
        }


        // (user => user.Email == Email);
        // left "user" is like 'i', the iterator, in for loops. Right side is query for what to set it to
        [HttpPost]
        [Route("login")]
        public IActionResult login(LoginRegViewModel logUser)
        {
            TempData["UserError"] = null;
            user tryUser = _context.users.SingleOrDefault(attempt => attempt.email == logUser.loginVM.email);
            {
                if (tryUser == null)
                {
                    TempData["Error"] = "Username or Password is incorrect";
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
                            HttpContext.Session.SetInt32("ActiveId", tryUser.userid);
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

            return View();

        }

        [HttpPost]//POST do not go to VIEW -> redirect
        [Route("changes")]
        public IActionResult changes(transactions newTrans)
        // above is case senstive to cshtml
        {
            //setting ID from session to userID
            int? userID = HttpContext.Session.GetInt32("ActiveId");
            // var ID = userID;
            if (userID == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //getting the users information from the DB
                // user helloUser = _context.users.SingleOrDefault(user => user.userid == userID);
                //getting a list of all of his the transactions..getting with LINQ..(iterator) 
                // List<transactions> allTrans = _context.transactions.Where(user => user.id == userID).ToList();
                //if they want to withdraw they will put neg in front of amount
                

                transactions newTransaction = new transactions{
                    transaction = newTrans.transaction,
                    //need to put (int) below to cast to int type
                    createTime = DateTime.Now,
                    usersid = (int)HttpContext.Session.GetInt32("ActiveId")
                };
               

                //if they want to withdraw they will put neg in front of amount
                // helloUser.balance += newTrans.transaction;
                // _context.SaveChanges();
                _context.transactions.Add(newTransaction);
                _context.SaveChanges();

               
                return RedirectToAction("Success");



            }

        }
    }
}

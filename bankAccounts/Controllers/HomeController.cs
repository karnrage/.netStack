using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//add by me
using Microsoft.EntityFrameworkCore;
using bankAccounts.Models;
using System.Linq;

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
        //below new instance of a class that i modeled my form out
        public IActionResult register(RegisterViewModel model)
        {
            // if(ModelState.IsValid)
            {
                user NewUser = new user
                {
                    //model attribute name = object name.
                    //through the parameter way: it grabs the input name through html.
                    //however done this way the name has to be the same as the object.
                    firstName = model.firstName,
                    lastName = model.lastName,
                    email = model.email,
                    password = model.password,
                    createTime= DateTime.Now,
                 
                };

                _context.users.Add(NewUser);// middle word is supposed to match up with table name
                _context.SaveChanges();
                
            }
            return RedirectToAction("success");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult success()
        {
            List<user> allUsers = _context.users.ToList();
            ViewBag.theUsers = allUsers;
            return View();
        }
    }
}

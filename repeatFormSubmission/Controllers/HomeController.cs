using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;//in order to use session in this controller file
// below: my adds
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;

using repeatFormSubmission.Models;
using Newtonsoft.Json;

namespace repeatFormSubmission.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.error = TempData["Error"];
            return View();
        }
        [HttpPost]
        [Route("/Process")]
        public IActionResult Process(string firstname, string lastname, int age, string email, string password)
         
        {
         
            
            User NewUser = new User
            {
                firstName = firstname,
                lastName = lastname,
                Age = age,
                Email = email,
                Password = password,
            };
        
            if(!TryValidateModel(NewUser))
            {
                ViewBag.ModelFields = ModelState.Values;
                return View();
            }
            else
            {
     
                return RedirectToAction("Success");

            }
        }
        [HttpGet]
        [Route("/success")]
        public IActionResult Success()
        {
            return View();
        }                   
    }
}

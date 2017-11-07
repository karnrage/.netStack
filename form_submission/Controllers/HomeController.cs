using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; //in order to use session in this controller file
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;

using form_submission.Models;
using Newtonsoft.Json;
 
namespace form_submission.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
           
            {
                ViewBag.error = TempData["Error"];
                
                
                return View();
            }
            
        }

        
        [HttpPost]
        [Route("formProcess")]
        public IActionResult formProcess(string firstname, string lastname, int age, string email, string password)
         
        {
         
            
            User NewUser = new User
            {
                firstName = firstname,
                lastName = lastname,
                Age = age,
                Email = email,
                Password = password,
            };
        
            if(TryValidateModel(NewUser))
            {
                TempData["Error"] ="Success";
                return RedirectToAction("Index");
            }
            else
            {
                System.Console.WriteLine("validator didn't work");
                List<string> theErrors = new List<string>();
                foreach(var iterator in ModelState.Values)
                {

                    if(iterator.Errors.Count >0){
                        System.Console.WriteLine(theErrors);
                        theErrors.Add(iterator.Errors[0].ErrorMessage.ToString());
                    }
                
                }
                TempData["Error"]=theErrors;
                return RedirectToAction("Index");

            }
        }                   
    }
}

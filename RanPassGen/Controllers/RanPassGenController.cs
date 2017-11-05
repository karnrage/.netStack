using System;using System.Collections.Generic;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Newtonsoft.Json;

namespace RanPassGen.Controllers

{
    public class RanPassGenController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult home()
        {
            //need a variable to keep track of the # of passcode which has been generated
            // if the variable count is null, then set it to zero
            if (HttpContext.Session.GetInt32("Count")==null){
                HttpContext.Session.SetInt32("Count",0);
            }
            
            int? newCount= HttpContext.Session.GetInt32("Count")+1;
            HttpContext.Session.SetInt32("Count", (int)newCount);
            // created a string of possible characters usable in making the password
            string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            // string of the current password. currently empty until generated
            string Password = "";
            // created a new random object. syntax below
            // password needs to be 14 characters long.....loop below
            Random Rand = new Random();
             for (int i=0; i<15; i++){
                char character = Characters[Rand.Next(0,Characters.Length)];
                Password += character;
            }

            ViewBag.Password = Password;
            ViewBag.Count = newCount;
            return View("home");
            
        }
        
        [HttpGet]
        [Route("clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();

            return View("");
           
        }
        
        
    }
}

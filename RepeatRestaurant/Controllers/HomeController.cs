using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// below: my adds
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using RepeatRestaurant.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RepeatRestaurant.Controllers
{
    public class HomeController : Controller
    {
        RepeatRestaurantContext _context;

         public HomeController(RepeatRestaurantContext context)
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
        [Route("Process")]
        public IActionResult Process(thisUserReview newReview)// name of the class; name of new object
            //no longer needed string reviewer_Name, string restaurant_Name, string Review, DateTime visit_Date, int Stars
        {
            if(ModelState.IsValid)
            {
                thisUserReview NewReview = new thisUserReview
                {
                    reviewer_name = newReview.reviewer_name,
                    restaurant_name = newReview.restaurant_name,
                    review = newReview.review,
                    visit_date = newReview.visit_date,
                    stars = newReview.stars

                };
                if(newReview.visit_date > DateTime.Now)
                {
                    TempData["dateError"] = "You can't review for a visit in the future";
                }

                else
                {
                    _context.review_table.Add(NewReview);// middle word is supposed to match up with table name
                    _context.SaveChanges();
                    return RedirectToAction("success");
                }   
                
            }
            
            return RedirectToAction("Index");
            // return something
        }

    }
}

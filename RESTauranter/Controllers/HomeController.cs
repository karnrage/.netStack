using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// using DbConnection;
//my insertions below
using Microsoft.EntityFrameworkCore;
using RESTauranter.Models;
using System.Linq;

namespace RESTauranter.Controllers
{
    public class HomeController : Controller
    {
        private RESTauranterContext _context;
 
    	public HomeController(RESTauranterContext context)
    	{
            _context = context;
    	}
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM users");
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
                    //model attribute name = object name.
                    //through the parameter way: it grabs the input name through html.
                    //however done this way the name has to be the same as the object.
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

                // string savedReview = $"INSERT INTO USER(reviewer_name, restaurant_Name, Review, visit_Date, Stars, date_created, date_modified) VALUES ('{reviewer_Name}', '{restaurant_Name}', '{Review}','{visit_Date}','{Stars}', NOW(), NOW()); SELECT LAST_INSERT_ID() as id";
                //SET NEWEST REVIEW AS LAST TO PRESENT AS ORDERED
                // _dbConnector.Execute(savedReview);
                // HttpContext.Session.SetInt32("id", Convert.ToInt32(_dbConnector.Query(query)[0]["id"]));
                //FIX ABOVE LINES
                
                // NOW AND NOW DON'T CHANGE COLOR BECAUSE THEY ARE NOT PARAMETERS, AND BRACES INDICATE YOU ARE PASSING IN A VALUE


                //add validations
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

        [HttpGet]
        [Route("success")]
        public IActionResult success()
        {
            List<thisUserReview> Allreviews = _context.review_table.ToList();
            ViewBag.theReviews = Allreviews;
            return View();
        }
    }
}

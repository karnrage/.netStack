using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using eCommerce.Models;
using eCommerce.ActionFilters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    public class ProductController : Controller
    {
        // ########## ROUTES ##########
        //  /
        //  /(add_routes_guide)
        //  /
        // ########## ROUTES ##########

        // Dapper connections
        // private readonly UserFactory userFactory;
        // private readonly DbConnector _dbConnector;

        // Entity PostGres Code First connection
        private ECommerceContext _context;

        public ProductController(ECommerceContext context)
        {
            // Dapper framework connections
            // _dbConnector = connect;
            // userFactory = new UserFactory();

            // Entity Framework connections
            _context = context;
        }

        // GET: /Home/
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
                    return RedirectToAction("Success");
                }   
                
            }
            
            return RedirectToAction("Index");
            // return something
        }

    }
}

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
using System.Globalization;

namespace eCommerce.Controllers
{
    public class CustomerController : Controller
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

        public CustomerController(ECommerceContext context)
        {
            // Dapper framework connections
            // _dbConnector = connect;
            // userFactory = new UserFactory();

            // Entity Framework connections
            _context = context;
        }

        // GET: /Home/

        [HttpGet]
        [Route("/customers")]
        [ImportModelState]
        public IActionResult Customers()
        {
            return View();
        }

        [HttpPost]
        [Route("createCustomer")]
        [ImportModelState]
        public IActionResult createCustomer(string newName)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine("============LOOK RIGHT HERE=============" + newName);
                
               if(  _context.Customers.SingleOrDefault(c => c.Name == newName ) == null)
                {
                   Customer newCustomer = new Customer
                   {
                               Name = newName,
                   };
                    _context.Customers.Add(newCustomer);// middle word is supposed to match up with table name
                    _context.SaveChanges();
                    return View("Customers");
                   
                } else {
                    TempData["DuplicateCustomer"] = "You can't create a customer that already exists";
                  }
                  
            } else{
                TempData["ModelState"] = "Bad Model state";
            }
            return RedirectToAction("Index","Home");
        }

        
                
            //     Product PickedProduct = _context.Products.SingleOrDefault(p => p.ProductID == ProductID );
            //     if(PickedCustomer != null && PickedProduct != null)
            //     {
            //         if(Quantity > PickedProduct.Quantity)
            //         {
            //             TempData["error"] = true ;
            //         } else {
            //             Order createOrder = new Order
            //             {
            //                 product = PickedProduct,
            //                 customer = PickedCustomer,
            //                 // ImageLink = newProduct.ImageLink,
            //                 // Description = newProduct.Description,
            //                 Quantity = Quantity,
            //                 PurchaseDate = DateTime.Now

            //             };
            //             _context.Orders.Add(createOrder);// middle word is supposed to match up with table name
            //             _context.SaveChanges();

            //         }
            //     }

            //         return RedirectToAction("Index","Home");
            // }
            // else
            // {
            //     System.Console.WriteLine(ModelState);
            // } 
            // return View("Products");
        }


    }


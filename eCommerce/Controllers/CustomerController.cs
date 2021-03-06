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
            List <Customer> customers = _context.Customers.OrderByDescending(d => d.CustomerDate).ToList();
            ViewBag.AllCustomers = customers;

            // System.Console.WriteLine(ViewBag.AllCustomers);

            List<Product> products = _context.Products.ToList();
            ViewBag.AllProducts = products;

            List<Order> orders = _context.Orders.ToList();
            ViewBag.AllOrders = orders;
            // System.Console.WriteLine("looooooooooook ==== " + orders);

             

            System.Console.WriteLine("lllllllllllllOOOOOOOOOOOOOOOK  at # of customers in the list"+customers.Count);
            // for (int when = 0; when < customers.Count; when++)
            foreach( var when in customers)
                {
                    
                    DateTime myformatting = when.CustomerDate;
                    string postFormat = "MMMM dd yyyy";
                    Console.WriteLine(myformatting.ToString(postFormat)); 
                    var postformatted = myformatting.ToString(postFormat);
                    // ViewBag.postformatted = postformatted; 

                    //separting the date into parts and will sandwhich together when printing
                    // System.Console.WriteLine("month part");
                    string postFormatMonth = "MMMM";
                    var postformattedMonth = myformatting.ToString(postFormatMonth);

                        
                    long ForOrdinals = long.Parse(myformatting.ToString("dd"));
                    Console.WriteLine(ForOrdinals);

                    
                    if( ForOrdinals <= 0 ){
                        ViewBag.postformatted = ForOrdinals.ToString();
                        Console.WriteLine("after if statement");
                    } 
                    

                    switch(ForOrdinals % 100)
                    {
                        case 11:
                        case 12:
                        case 13:

                        ViewBag.postformatted = ForOrdinals + "th";
                        System.Console.WriteLine(ViewBag.postformatted);
                        break;
                    }
                    Console.WriteLine("between switch statements"); 
                    switch(ForOrdinals % 10)
                    {
                        case 1:
                            ViewBag.postformatted4 = ForOrdinals + "st";
                            Console.WriteLine(/*postformattedMonth +*/ " "+ ForOrdinals +"st");
                            System.Console.WriteLine(ViewBag.postformatted);
                            break;
                        case 2:
                            ViewBag.postformatted3 = ForOrdinals + "nd";
                            System.Console.WriteLine(ViewBag.postformatted);
                            break;
                        case 3:                                    
                            ViewBag.postformatted2 = /*postformattedMonth +*/" "+ ForOrdinals + "rd";
                            Console.WriteLine(/*postformattedMonth +*/" "+ ForOrdinals +"rd");
                            System.Console.WriteLine(ViewBag.postformatted);
                            break;
                        default:
                            postformatted = ForOrdinals + "th";
                            ViewBag.postformatted = postformatted;
                            ViewBag.testing = ForOrdinals + "th";
                            ViewData["th"] = ForOrdinals + "th";
                            Console.WriteLine(/*postformattedMonth +*/" "+ ForOrdinals +"th");
                            List <string> finalized = new List<string>();
                            finalized.Add(postformattedMonth +" "+ postformatted);
                            ViewBag.finalized = finalized;

                            System.Console.WriteLine(ViewBag.postformatted);
                            break;
                        
                    }  
                    Console.WriteLine("Reached end of loop");                   
                }
            return View();
        }

        [HttpPost]
        [Route("createCustomer")]
        [ImportModelState]
        public IActionResult createCustomer(string Name)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine("============LOOK RIGHT HERE=============" + Name);
                
               if(  _context.Customers.SingleOrDefault(c => c.Name == Name ) == null)
                {
                   Customer newCustomer = new Customer
                   {
                               Name = Name,
                               CustomerDate = DateTime.Now
                   };
                    _context.Customers.Add(newCustomer);// middle word is supposed to match up with table name
                    _context.SaveChanges();
                    return RedirectToAction("Customers","Customer");
                   
                } else {
                    TempData["DuplicateCustomer"] = "You can't create a customer that already exists";
                    // trying below to fix error, no good
                    return RedirectToAction("Customers","Customer");
                  }
                  
            } else{
                TempData["ModelState"] = "Bad Model state";
            }
            return RedirectToAction("","Home");
        }

        [HttpPost]
		[Route("removeCustomer/{id}")]
		public IActionResult RemoveCustomer(int id)
		{
			Customer customer  = _context.Customers.SingleOrDefault(c => c.CustomerID == id);
			_context.Customers.Remove(customer);
			_context.SaveChanges();
			return RedirectToAction("customers");
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


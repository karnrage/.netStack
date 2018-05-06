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
    public class OrderController : Controller
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

        public OrderController(ECommerceContext context)
        {
            // Dapper framework connections
            // _dbConnector = connect;
            // userFactory = new UserFactory();

            // Entity Framework connections
            _context = context;
        }

        // GET: /Home/
   


        [HttpPost]
        [Route("createOrder")]
        [ImportModelState]
        public IActionResult createOrder(int CustomerID, int Quantity, int ProductID)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine("============LOOK RIGHT HERE=============" + CustomerID);
                Customer PickedCustomer = _context.Customers.SingleOrDefault(c => c.CustomerID == CustomerID );
                Product PickedProduct = _context.Products.SingleOrDefault(p => p.ProductID == ProductID );
                
                ViewBag.PickedCustomer = PickedCustomer;
                ViewBag.PickedProduct = PickedProduct;
                
                
                
                if(PickedCustomer != null && PickedProduct != null)
                {
                    if(Quantity > PickedProduct.Quantity)
                    {
                        TempData["Understock"] = "You can't order more than the number in inventory";

                        
                    } else {
                        Order createOrder = new Order
                        {
                            product = PickedProduct,
                            customer = PickedCustomer,
                            // ImageLink = newProduct.ImageLink,
                            // Description = newProduct.Description,
                            Quantity = Quantity,
                            PurchaseDate = DateTime.Now

                        };
                        _context.Orders.Add(createOrder);// middle word is supposed to match up with table name
                        _context.SaveChanges();

                    }
                }

                    return RedirectToAction("Index","Home");
            }
            else
            {
                System.Console.WriteLine(ModelState);
            } 
            return View("Products");
        }

        [HttpGet]
        [Route("orders")]
        [ImportModelState]
        public IActionResult Orders()
        {
            List <Customer> customers = _context.Customers.ToList();
            ViewBag.AllCustomers = customers;

            System.Console.WriteLine(ViewBag.AllCustomers);

            List<Product> products = _context.Products.ToList();
            ViewBag.AllProducts = products;

            List<Order> orders = _context.Orders.ToList();
            ViewBag.AllOrders = orders;

            foreach( var when in orders)
                {
                    DateTime myformatting = when.PurchaseDate;
                    string postFormat = "MMMM d yyyy";
                    Console.WriteLine(myformatting.ToString(postFormat)); 
                    var postformatted = myformatting.ToString(postFormat);
                    ViewBag.postformatted = postformatted;               
                        
                    long ForOrdinals = long.Parse(myformatting.ToString("dd"));
                    Console.WriteLine(ForOrdinals);
                    // below code blocks are not being entered. fix
                    if( ForOrdinals <= 0 ) 
                    
                        ViewBag.portformatted = ForOrdinals.ToString();
                    Console.WriteLine("after if statement"); 
                    switch(ForOrdinals % 100)
                    {
                        case 11:
                        case 12:
                        case 13:

                        ViewBag.portformatted = ForOrdinals + "th";
                        break;
                    }
                    Console.WriteLine("between switch statements"); 
                    switch(ForOrdinals % 10)
                    {
                        case 1:
                            ViewBag.portformatted = ForOrdinals + "st";
                            break;
                        case 2:
                            ViewBag.portformatted = ForOrdinals + "nd";
                            break;
                        case 3:                                    
                            ViewBag.portformatted = ForOrdinals + "rd";
                            System.Console.WriteLine("rd");
                            break;
                        default:
                            ViewBag.portformatted = ForOrdinals + "th";
                            Console.WriteLine("th");
                            break;
                        
                    }  
                    Console.WriteLine("Reached end of loop");                   
                }
            return View();
        }


        // [HttpGet]
        // [Route("")]
        // [ImportModelState]
        // public IActionResult index()
        // {
        //     // return View();
        //     return RedirectToAction("","Home");
        // }    

    }
}

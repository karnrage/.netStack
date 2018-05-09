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
            System.Console.WriteLine("looooooooooook ==== " + orders);


            foreach( var when in orders)
                {
                    DateTime myformatting = when.PurchaseDate;
                    string postFormat = "MMMM dd yyyy";
                    Console.WriteLine(myformatting.ToString(postFormat)); 
                    var postformatted = myformatting.ToString(postFormat);
                    ViewBag.postformatted = postformatted; 

                    //separting the date into parts and will sandwhich together when printing
                    System.Console.WriteLine("month part");
                    string postFormatMonth = "MMMM";
                    var postformattedMonth = myformatting.ToString(postFormatMonth);

                        
                    long ForOrdinals = long.Parse(myformatting.ToString("dd"));
                    Console.WriteLine(ForOrdinals);

                    
                    if( ForOrdinals <= 0 ) 
                    
                        ViewBag.postformatted = ForOrdinals.ToString();
                        Console.WriteLine("after if statement");

                    switch(ForOrdinals % 100)
                    {
                        case 11:
                        case 12:
                        case 13:

                        ViewBag.postformatted = ForOrdinals + "th";
                        break;
                    }
                    Console.WriteLine("between switch statements"); 
                    switch(ForOrdinals % 10)
                    {
                        case 1:
                            ViewBag.postformatted = ForOrdinals + "st";
                            break;
                        case 2:
                            ViewBag.postformatted = ForOrdinals + "nd";
                            break;
                        case 3:                                    
                            ViewBag.postformatted = postformattedMonth +" "+ ForOrdinals + "rd";
                            Console.WriteLine(postformattedMonth +" "+ ForOrdinals +"rd");
                            break;
                        default:
                            ViewBag.postformatted = ForOrdinals + "th";
                            Console.WriteLine(postformattedMonth +" "+ ForOrdinals +"th");
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

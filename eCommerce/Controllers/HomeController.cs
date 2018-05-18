using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using eCommerce.Models;
using eCommerce.Factory;
using eCommerce.ActionFilters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
//my adds
using Microsoft.AspNetCore.Session;
using System.Globalization;



namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        // ########## ROUTES ##########
        //  /
        //  /(add_routes_guide)
        //  /
        // ########## ROUTES ##########

        // Dapper connections
        // private readonly UserFactory UserFactory;
        // private readonly DbConnector _dbConnector;

        // Entity PostGres Code First connection
        private ECommerceContext _context;

        public HomeController(ECommerceContext context)
        {
            // Dapper framework connections
            // _dbConnector = connect;
            // UserFactory = new UserFactory();

            // Entity Framework connections
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("index")]
        [ImportModelState]
        public IActionResult Index()
        {
            // Need to query customers and products before querying the order. Will not establish relationships and and will create "null" instead

            List <Customer> customers = _context.Customers.ToList();
            ViewBag.AllCustomers = customers;

            System.Console.WriteLine(ViewBag.AllCustomers);

            List<Product> products = _context.Products.ToList();
            ViewBag.AllProducts = products;


            List<Order> NewOrders = _context.Orders.OrderByDescending(p => p.PurchaseDate).Take(3).ToList();
            ViewBag.LastThreeOrders = NewOrders;
            System.Console.WriteLine("looooooooooook ====" + NewOrders);

            List<Customer> NewCustomers = _context.Customers.OrderByDescending(p => p.CustomerDate).Take(3).ToList();
            ViewBag.LastThreeCustomers = NewCustomers;
            System.Console.WriteLine("looooooooooook ====" + NewCustomers);
            return View();
        }

        [HttpGet]
        [Route("settings")]
        [ImportModelState]
        public IActionResult settings()
        {
            return View();
        }

        

        // [HttpPost]
        // [Route("product/new")]
        // [ImportModelState]
        // public IActionResult CreateProduct(Product newProduct)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         Product createProduct = new Product
        //         {
        //             ProductName = newProduct.ProductName,
        //             ImageLink = newProduct.ImageLink,
        //             Description = newProduct.Description,
        //             Quantity = newProduct.Quantity,

        //         };
        //             _context.Products.Add(newProduct);// middle word is supposed to match up with table name
        //             _context.SaveChanges();
        //             return RedirectToAction("Index");
        //     }
        //     return RedirectToAction("Products");
        // }

    }
}



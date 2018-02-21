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
        [Route("")]
        [ImportModelState]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("orders")]
        [ImportModelState]
        public IActionResult Orders()
        {
            return View();
        }

        [HttpPost]
        [Route("product/new")]
        [ImportModelState]
        public IActionResult CreateProduct(Product newProduct)
        {
            if(ModelState.IsValid)
            {
                Product createProduct = new Product
                {
                    ProductName = newProduct.ProductName,
                    ImageLink = newProduct.ImageLink,
                    Description = newProduct.Description,
                    Quantity = newProduct.Quantity,

                };
                    _context.Products.Add(newProduct);// middle word is supposed to match up with table name
                    _context.SaveChanges();
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Products");
        }

    }
}



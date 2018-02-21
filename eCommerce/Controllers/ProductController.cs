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
        [Route("create")]
        [ImportModelState]
        public IActionResult Create(Product newProduct)
        {
            if(ModelState.IsValid)
            {
  
                Product createProduct = new Product
                {
                    ProductName = newProduct.ProductName,
                    ImageLink = newProduct.ImageLink,
                    Description = newProduct.Description,
                    Quantity = newProduct.Quantity,
                    CreatedDate = DateTime.Now

                };
                    _context.Products.Add(createProduct);// middle word is supposed to match up with table name
                    _context.SaveChanges();
                    return RedirectToAction("Index","Home");
            }
            else
            {
                System.Console.WriteLine(ModelState);
            } 
            return View("Products");
        }

        [HttpGet]
        [Route("products")]
        [ImportModelState]
        public IActionResult Products()
        {
            List<Product> products = _context.Products.ToList();
            ViewBag.Products = products;
            return View();
        }


        [HttpGet]
        [Route("index")]
        [ImportModelState]
        public IActionResult index()
        {
            // return View();
            return RedirectToAction("","Home");
        }    

    }
}

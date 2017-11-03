using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace setupASP

// namespace YourNamespace.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("{firstName}/{lastName}/{age}/{favColor}")]
        public JsonResult Index( string firstName, string lastName, int age, string favColor)
        {
            var JsonObject = new
            {firstName = firstName,
            lastName = lastName,
            age = age,
            favColor = favColor};

            return Json(JsonObject);

            
            
            
            // return "Hello World! Enter fav color";
        } public JsonResult Deault(){
            var output = new{
                enteredrouting = "firstName/lastName/age/favColor"
            };
            return Json(output);
        }
        // A GET method
        // [HttpGet]
        // [Route("index")]
        // public string Home()
        // {
        //    return "Hekki CANk";

            
        // }
            // JSON conversion below
        //      [HttpGet]
        // [Route("")]
        // public JsonResult Example()
        // {
        //     // The Json method convert the object passed to it into JSON
        //     return Json(SomeC#Object);
        // }
    }
}


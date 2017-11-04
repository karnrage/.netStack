using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dojoSurvey.Controllers

{
    public class SurveyController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult home()
        {
            return View();
        }

        [HttpPost]
        [Route("Survey")]
        public IActionResult Method(string yourName, string dojoLocation, string favLanguage, string email, int phoneNum, string comment)
        {
            ViewBag.Name = yourName;
            ViewBag.Location = dojoLocation;
            ViewBag.Languge = favLanguage;
            ViewBag.Email = email;
            ViewBag.Phone = phoneNum;
            ViewBag.Comment = comment;
            return View("Submitted");
        }

        
    }
}

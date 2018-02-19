using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using CNetwork.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

//below name space change to CNetwork.Controllers?
namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private CNetworkContext _context;

        public HomeController(CNetworkContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(RegisterViewModel RegisterUser)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<Users> Hasher = new PasswordHasher<Users>();
                Users NewUser = new Users();
                NewUser.Password = Hasher.HashPassword(NewUser, RegisterUser.Password);
                NewUser.FirstName = RegisterUser.FirstName;
                NewUser.LastName = RegisterUser.LastName;
                NewUser.Email = RegisterUser.Email;
                NewUser.Description = RegisterUser.Description;
                NewUser.CreatedAt = DateTime.Now;
                NewUser.UpdatedAt = DateTime.Now;

                _context.Add(NewUser);
                _context.SaveChanges();
                //taking the last user that was put into the database and setting into a session
                int CurrentUser = _context.Users.Last().idUser;
                HttpContext.Session.SetInt32("CurrentUser", CurrentUser);
                return Redirect("Dashboard");
            }
            else
            {
                return View("Index", RegisterUser);
            }
        }
        [HttpGet]
        [Route("/loginpage")]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel loginUser)
        {
            Users checkemail = _context.Users.SingleOrDefault(x => x.Email == loginUser.Email);
            if (checkemail == null )
            {
                ViewBag.loginErrors = "Email is not found. Please Register.";
                return View("loginpage");
            }
            //find the email
            Users CurrentUser = _context.Users.Where(x => x.Email == loginUser.Email).SingleOrDefault();
            //match email and password
            if (CurrentUser != null && loginUser.Password != null)
            {
                var Hasher = new PasswordHasher<Users>();
                if (0 != Hasher.VerifyHashedPassword(CurrentUser, CurrentUser.Password, loginUser.Password))
                {
                    //set to session and redirect to success
                    HttpContext.Session.SetInt32("CurrentUser", CurrentUser.idUser);
                    return Redirect("Dashboard");
                }
            }
            return View("loginpage");
        }

        [HttpGet]
        [Route("/Dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return RedirectToAction("loginpage");
            }
            else
            {
                int? CurrentUser = HttpContext.Session.GetInt32("CurrentUser");
                Users user = _context.Users.SingleOrDefault(finduser => finduser.idUser == CurrentUser);
                ViewBag.UserProfile = user;

                List <Invite> Pending = _context.Invite.Include(u => u.Requester).Where(x => x.AccepterId == CurrentUser).ToList();
                ViewBag.Pending = Pending; 

                List <Friends> UserFriendships = _context.Friends.Include(u => u.Friend).Where(u => u.idUser == user.idUser).ToList();
                ViewBag.UserFriends = UserFriendships;

                List <Users> allFriends = _context.Users.Include(u => u.Friendship).ThenInclude(u => u.Friend).Where(u =>u.idUser == user.idUser).ToList();
                ViewBag.allFriends = allFriends;
            };
            return View();
        }

        [HttpGet]
        [Route("/logout")]

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("loginpage");
        }
    }

}
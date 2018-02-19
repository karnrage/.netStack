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


namespace CNetwork.Controllers
{
   public class NetworkController : Controller
    {
        private CNetworkContext _context;

        public NetworkController(CNetworkContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/Network")]
        public IActionResult Network()
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return RedirectToAction("loginpage");
            }
            else
            {
                int? CurrentUser = HttpContext.Session.GetInt32("CurrentUser");
                Users user = _context.Users.SingleOrDefault(finduser => finduser.idUser == CurrentUser);
                //grabbing all users but the user logged in 
                List<Users> OtherUser = _context.Users.Where( u => u.idUser != CurrentUser).ToList();

                //Taking all the invations from the invite table
                List<Invite> allInvites = _context.Invite.Include(u => u.Accepter).Include(x => x.Requester).ToList();

                //Taking all the friendships from the friends table
                List<Friends> allFriendships = _context.Friends.Include(u => u.Friend).Include(u => u.User).ToList();

                //if user has a invation with another user....remove them from the network list
                foreach(var checkinvites in allInvites)
                {
                    bool RemoveNetwork = false; 
                    if(checkinvites.Requester.idUser == CurrentUser)
                    {
                        RemoveNetwork = true;
                    }
                    if(RemoveNetwork)
                    {
                        OtherUser.Contains(checkinvites.Accepter);
                        {
                            OtherUser.Remove(checkinvites.Accepter);
                        }
                    }
                }
                ViewBag.Network = OtherUser;
                return View();
            }
        }

        [HttpGet]
        [Route("/invite/{id}")]
        public IActionResult Invite(int id)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return RedirectToAction("loginpage");
            }
            else
            {
                int? CurrentUser = HttpContext.Session.GetInt32("CurrentUser");
                
                Invite newInvite = new Invite
                {
                    AccepterId = id,
                    RequesterId = (int)CurrentUser, 
                };
                _context.Add(newInvite);
                _context.SaveChanges();

                return RedirectToAction("Network");
            }    
        }
 
        [HttpGet]
        [Route("/accept/{id}")]
        public IActionResult Accept(int id)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return RedirectToAction("loginpage");
            }
            else
            {
                int? CurrentUser = HttpContext.Session.GetInt32("CurrentUser");
                //creating relationship between user logged in and the requester
                Friends newFriendship = new Friends
                {
                    idFriend = id,
                    idUser = (int)CurrentUser,
                };

                _context.Add(newFriendship);
                _context.SaveChanges();

                //creating relationship between the requester and the user logged in 
                Friends ReverseFriendShip = new Friends
                {
                    idFriend = (int)CurrentUser,
                    idUser = id,
                };

                _context.Add(ReverseFriendShip);
                _context.SaveChanges();

                //check to see where the relationships that were created above are in the invite table
                List<Invite> deleteInvite = _context.Invite
                .Where(invite => (invite.RequesterId == id && invite.AccepterId == CurrentUser) 
                || (invite.RequesterId == CurrentUser && invite.AccepterId == id)).ToList();

                //because of you have to delete 2 invites in the table a for loop is needed 
                foreach (var invite in deleteInvite)
                {
                    _context.Remove(invite);
                    _context.SaveChanges();
;                }
                return Redirect("/Dashboard");
            }
        }

        [HttpGet]
        [Route("/decline/{id}")]
        public IActionResult Decline(int id)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return RedirectToAction("loginpage");
            }
            else
            {
                int? CurrentUser = HttpContext.Session.GetInt32("CurrentUser");

                Invite decline = _context.Invite.SingleOrDefault(x => x.InviteId == id);
                _context.Remove(decline);
                _context.SaveChanges();

                return Redirect("/Dashboard");
            }
        }

        [HttpGet]
        [Route("/user/{id}")]
        public IActionResult UserProfile(int id)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return RedirectToAction("loginpage");
            }
            else
            {
                Users UserProfile = _context.Users.SingleOrDefault(u => u.idUser == id);
                ViewBag.UserProfile = UserProfile;
                return View();
            }
        }
    }
}


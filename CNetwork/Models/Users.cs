using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CNetwork.Models;

namespace CNetwork.Models
{

    public class Users : BaseEntity
    {
        [Key]
        public int idUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        

        [InverseProperty("friend")]
        public List<Friends> Friendship { get; set;}

        [InverseProperty("Accepter")]
        public List<Invite> FriendAccept {get; set;}

        [InverseProperty("Requester")]
        public List<Invite> FriendRequest { get; set; }

        public Users()
        {
            Friendship = new List<Friends>();
            FriendAccept = new List<Invite>();
            FriendRequest = new List<Invite>();
        }
    }

}
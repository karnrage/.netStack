using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CNetwork.Models;

namespace CNetwork.Models
{
    public class Invite : BaseEntity
    {
        public int InviteId { get; set; }
        public int RequesterId { get; set; }
        public int AccepterId { get; set; }


        public Users Requester { get; set; }
        public Users Accepter { get; set; }
    }
}
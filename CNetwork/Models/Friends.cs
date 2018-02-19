using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CNetwork.Models;

namespace CNetwork.Models
{
   public class Friends : BaseEntity
   {
       public Friends()
       {
           Invite = new List<Invite>();
       }
       [Key]
       public int idFriends { get; set; }
       public int idUser { get; set; }
       public int idFriend { get; set; }
       public DateTime CreatedAt { get; set; }

       public Users User { get; set; }
       public Users Friend { get; set; }

       public List<Invite> Invite { get; set; }
   }
}
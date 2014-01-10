using System;
using System.Collections.Generic;

namespace MusicStore.Core.Models
{
   public class User
   {
      public User()
      {
         AddressBooks = new HashSet<AddressBook>();
      }

      public int UserId { get; set; }
      public int RoleId { get; set; }
      public string Name { get; set; }
      public string UserName { get; set; }
      public string Password { get; set; }
      public string Email { get; set; }
      public ICollection<AddressBook> AddressBooks { get; set; }
      public Role Role { get; set; }
      public decimal TotalCredit { get; set; }
   }
}

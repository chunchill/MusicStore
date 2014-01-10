using System;
using System.Collections.Generic;

namespace MusicStore.Core.Models
{
   public class AddressBook
   {
      public AddressBook()
      {
         Orders = new HashSet<Order>();
      }

      public int AddressId { get; set; }
      public int UserId { get; set; }
      public bool? IsDefault { get; set; }
      public string City { get; set; }
      public string Province { get; set; }
      public string PostalCode { get; set; }
      public string Address { get; set; }
      public string Phone { get; set; }
      public string Email { get; set; }
      public ICollection<Order> Orders { get; set; }
      public User User { get; set; }
   }
}

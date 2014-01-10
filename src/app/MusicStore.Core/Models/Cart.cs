using System;
using System.Collections.Generic;

namespace MusicStore.Core.Models
{
   public class Cart
   {
      public int RecordId { get; set; }
      public string CartId { get; set; }
      public int AlbumId { get; set; }
      public int Count { get; set; }
      public DateTime? CreatedDate { get; set; }
      public Album Album { get; set; }
   }
}

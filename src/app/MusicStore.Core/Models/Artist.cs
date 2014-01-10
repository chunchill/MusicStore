using System;
using System.Collections.Generic;

namespace MusicStore.Core.Models
{
   public class Artist
   {
      public Artist()
      {
         Albums = new HashSet<Album>();
      }

      public int ArtistId { get; set; }
      public string Name { get; set; }
      public ICollection<Album> Albums { get; set; }
   }
}

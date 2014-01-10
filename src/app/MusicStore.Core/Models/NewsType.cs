using System;
using System.Collections.Generic;

namespace MusicStore.Core.Models
{
   public class NewsType
   {
      public NewsType()
      {
         Newses = new HashSet<News>();
      }

      public int NewsTypeId { get; set; }
      public string Name { get; set; }
      public ICollection<News> Newses { get; set; }
   }
}

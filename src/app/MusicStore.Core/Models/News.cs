using System;
using System.Collections.Generic;

namespace MusicStore.Core.Models
{
   public class News
   {
      public int NewsId { get; set; }
      public int NewsTypeId { get; set; }
      public string Title { get; set; }
      public string Content { get; set; }
      public string Link { get; set; }
      public NewsType NewsType { get; set; }
   }
}

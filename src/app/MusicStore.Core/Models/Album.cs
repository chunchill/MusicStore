using System;
using System.Collections.Generic;

namespace MusicStore.Core.Models
{
    public class Album
    {
        public Album()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public string AlbumArtUrl { get; set; }
        public double? Discount { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }


    }
}

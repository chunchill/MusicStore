using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
   {
      public OrderDetailMap()
      {
         // Primary Key
         this.HasKey(t => t.OrderDetailId);

         // Properties
         // Table & Column Mappings
         this.ToTable("OrderDetails");
         this.Property(t => t.OrderDetailId).HasColumnName("OrderDetailId");
         this.Property(t => t.OrderId).HasColumnName("OrderId");
         this.Property(t => t.AlbumId).HasColumnName("AlbumId");
         this.Property(t => t.Quantity).HasColumnName("Quantity");
         this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");

         // Relationships
         this.HasRequired(t => t.Album)
             .WithMany(t => t.OrderDetails)
             .HasForeignKey(d => d.AlbumId);
         this.HasRequired(t => t.Order)
             .WithMany(t => t.OrderDetails)
             .HasForeignKey(d => d.OrderId);

      }
   }
}

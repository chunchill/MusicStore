using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class OrderMap : EntityTypeConfiguration<Order>
   {
      public OrderMap()
      {
         // Primary Key
         this.HasKey(t => t.OrderId);
         // Properties
         // Table & Column Mappings
         this.ToTable("Orders");
         this.Property(t => t.OrderId).HasColumnName("OrderId");
         this.Property(t => t.OrderDate).HasColumnName("OrderDate");
         this.Property(t => t.AddressId).HasColumnName("AddressId");
         this.Property(t => t.Total).HasColumnName("Total");
         // Relationships
         this.HasRequired(t => t.AddressBook)
             .WithMany(t => t.Orders)
             .HasForeignKey(d => d.AddressId);

      }
   }
}

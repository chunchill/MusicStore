using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class AddressBookMap : EntityTypeConfiguration<AddressBook>
   {
      public AddressBookMap()
      {
         // Primary Key
         this.HasKey(t => t.AddressId);

         // Properties
         this.Property(t => t.City)
             .HasMaxLength(50);

         this.Property(t => t.Province)
             .HasMaxLength(50);

         this.Property(t => t.PostalCode)
             .HasMaxLength(50);

         this.Property(t => t.Address)
             .HasMaxLength(50);

         this.Property(t => t.Phone)
             .HasMaxLength(50);

         this.Property(t => t.Email)
             .HasMaxLength(50);

         // Table & Column Mappings
         this.ToTable("AddressBooks");
         this.Property(t => t.AddressId).HasColumnName("AddressId");
         this.Property(t => t.UserId).HasColumnName("UserId");
         this.Property(t => t.IsDefault).HasColumnName("IsDefault");
         this.Property(t => t.City).HasColumnName("City");
         this.Property(t => t.Province).HasColumnName("Province");
         this.Property(t => t.PostalCode).HasColumnName("PostalCode");
         this.Property(t => t.Address).HasColumnName("Address");
         this.Property(t => t.Phone).HasColumnName("Phone");
         this.Property(t => t.Email).HasColumnName("Email");

         // Relationships
         this.HasRequired(t => t.User)
             .WithMany(t => t.AddressBooks)
             .HasForeignKey(d => d.UserId);

      }
   }
}

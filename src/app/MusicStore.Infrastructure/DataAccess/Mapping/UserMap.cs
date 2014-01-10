using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class UserMap : EntityTypeConfiguration<User>
   {
      public UserMap()
      {
         // Primary Key
         this.HasKey(t => t.UserId);

         // Properties
         this.Property(t => t.Name)
             .HasMaxLength(50);

         this.Property(t => t.UserName)
             .HasMaxLength(50);

         this.Property(t => t.Password)
             .HasMaxLength(50);

         this.Property(t => t.Email)
             .HasMaxLength(50);

         // Table & Column Mappings
         this.ToTable("Users");
         this.Property(t => t.UserId).HasColumnName("UserId");
         this.Property(t => t.RoleId).HasColumnName("RoleId");
         this.Property(t => t.Name).HasColumnName("Name");
         this.Property(t => t.UserName).HasColumnName("UserName");
         this.Property(t => t.Password).HasColumnName("Password");
         this.Property(t => t.Email).HasColumnName("Email");

         // Relationships
         this.HasRequired(t => t.Role)
             .WithMany(t => t.Users)
             .HasForeignKey(d => d.RoleId).WillCascadeOnDelete();

      }
   }
}

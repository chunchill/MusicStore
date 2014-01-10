using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class RoleMap : EntityTypeConfiguration<Role>
   {
      public RoleMap()
      {
         // Primary Key
         this.HasKey(t => t.RoleId);

         // Properties
         this.Property(t => t.Name)
             .HasMaxLength(50);

         // Table & Column Mappings
         this.ToTable("Roles");
         this.Property(t => t.RoleId).HasColumnName("RoleId");
         this.Property(t => t.Name).HasColumnName("Name");
      }
   }
}

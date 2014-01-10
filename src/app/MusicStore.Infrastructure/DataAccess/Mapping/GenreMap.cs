using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class GenreMap : EntityTypeConfiguration<Genre>
   {
      public GenreMap()
      {
         // Primary Key
         this.HasKey(t => t.GenreId);

         // Properties
         this.Property(t => t.Name)
             .HasMaxLength(4000);

         this.Property(t => t.Description)
             .HasMaxLength(4000);

         // Table & Column Mappings
         this.ToTable("Genres");
         this.Property(t => t.GenreId).HasColumnName("GenreId");
         this.Property(t => t.Name).HasColumnName("Name");
         this.Property(t => t.Description).HasColumnName("Description");
      }
   }
}

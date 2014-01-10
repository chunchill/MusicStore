using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class ArtistMap : EntityTypeConfiguration<Artist>
   {
      public ArtistMap()
      {
         // Primary Key
         this.HasKey(t => t.ArtistId);

         // Properties
         this.Property(t => t.Name)
             .HasMaxLength(4000);

         // Table & Column Mappings
         this.ToTable("Artists");
         this.Property(t => t.ArtistId).HasColumnName("ArtistId");
         this.Property(t => t.Name).HasColumnName("Name");
      }
   }
}

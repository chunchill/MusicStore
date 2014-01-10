using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class AlbumMap : EntityTypeConfiguration<Album>
   {
      public AlbumMap()
      {
         // Primary Key
         this.HasKey(t => t.AlbumId);
         // Properties
         this.Property(t => t.Title)
             .HasMaxLength(320);
         this.Property(t => t.AlbumArtUrl)
             .HasMaxLength(1024);
         // Table & Column Mappings
         this.ToTable("Albums");
         this.Property(t => t.AlbumId).HasColumnName("AlbumId");
         this.Property(t => t.GenreId).HasColumnName("GenreId");
         this.Property(t => t.ArtistId).HasColumnName("ArtistId");
         this.Property(t => t.Title).HasColumnName("Title");
         this.Property(t => t.Price).HasColumnName("Price");
         this.Property(t => t.AlbumArtUrl).HasColumnName("AlbumArtUrl");
         this.Property(t => t.Discount).HasColumnName("Discount");
         // Relationships
         this.HasRequired(t => t.Artist)
             .WithMany(t => t.Albums)
             .HasForeignKey(d => d.ArtistId);
         this.HasRequired(t => t.Genre)
             .WithMany(t => t.Albums)
             .HasForeignKey(d => d.GenreId);
      }
   }
}

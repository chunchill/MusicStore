using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class NewsMap : EntityTypeConfiguration<News>
   {
      public NewsMap()
      {
         // Primary Key
         this.HasKey(t => t.NewsId);

         // Properties
         this.Property(t => t.Title)
             .IsRequired()
             .HasMaxLength(100);

         this.Property(t => t.Content)
             .IsRequired()
             .HasMaxLength(4000);

         this.Property(t => t.Link)
             .HasMaxLength(500);

         // Table & Column Mappings
         this.ToTable("Newses");
         this.Property(t => t.NewsId).HasColumnName("NewsId");
         this.Property(t => t.NewsTypeId).HasColumnName("NewsTypeId");
         this.Property(t => t.Title).HasColumnName("Title");
         this.Property(t => t.Content).HasColumnName("Content");
         this.Property(t => t.Link).HasColumnName("Link");

         // Relationships
         this.HasRequired(t => t.NewsType)
             .WithMany(t => t.Newses)
             .HasForeignKey(d => d.NewsTypeId);

      }
   }
}

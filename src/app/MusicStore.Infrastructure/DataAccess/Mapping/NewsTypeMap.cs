using MusicStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.Infrastructure.Models.Mapping
{
   public class NewsTypeMap : EntityTypeConfiguration<NewsType>
   {
      public NewsTypeMap()
      {
         // Primary Key
         this.HasKey(t => t.NewsTypeId);

         // Properties
         this.Property(t => t.Name)
             .HasMaxLength(100);

         // Table & Column Mappings
         this.ToTable("NewsTypes");
         this.Property(t => t.NewsTypeId).HasColumnName("NewsTypeId");
         this.Property(t => t.Name).HasColumnName("Name");
      }
   }
}

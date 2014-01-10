using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MusicStore.Core.Interfaces;
using MusicStore.Infrastructure.Models.Mapping;
using MusicStore.Core.Models;

namespace MusicStore.Infrastructure.DataAccess
{
   public class MusicStoreInitializer
      : DropCreateDatabaseIfModelChanges<MusicStoreContext>
   {
   }

   public class MusicStoreContext : DbContext, IUnitOfWork
   {
      static MusicStoreContext()
      {
         Database.SetInitializer(new MusicStoreInitializer());
      }

      public MusicStoreContext()
         : base("Name=MusicStore")//: base("Name=MusicStoreContext")
      {
      }

      public DbSet<AddressBook> AddressBooks { get; set; }
      public DbSet<Album> Albums { get; set; }
      public DbSet<Artist> Artists { get; set; }
      public DbSet<Cart> Carts { get; set; }
      public DbSet<Genre> Genres { get; set; }
      public DbSet<News> Newses { get; set; }
      public DbSet<NewsType> NewsTypes { get; set; }
      public DbSet<OrderDetail> OrderDetails { get; set; }
      public DbSet<Order> Orders { get; set; }
      public DbSet<Role> Roles { get; set; }
      public DbSet<User> Users { get; set; }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
         modelBuilder.Configurations.Add(new AddressBookMap());
         modelBuilder.Configurations.Add(new AlbumMap());
         modelBuilder.Configurations.Add(new ArtistMap());
         modelBuilder.Configurations.Add(new CartMap());
         modelBuilder.Configurations.Add(new GenreMap());
         modelBuilder.Configurations.Add(new NewsMap());
         modelBuilder.Configurations.Add(new NewsTypeMap());
         modelBuilder.Configurations.Add(new OrderDetailMap());
         modelBuilder.Configurations.Add(new OrderMap());
         modelBuilder.Configurations.Add(new RoleMap());
         modelBuilder.Configurations.Add(new UserMap());
      }

      public void Commit()
      {
         SaveChanges();
      }


      public void RollBack()
      {
          throw new System.NotImplementedException();
      }
   }
}

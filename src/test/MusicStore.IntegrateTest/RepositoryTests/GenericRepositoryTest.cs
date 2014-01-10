using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;
using MusicStore.Infrastructure.DataAccess;
using MusicStore.Infrastructure.Repositories;
using NUnit.Framework;

namespace MusicStore.IntegrateTest.RepositoryTests
{
   [TestFixture]
   public class GenericRepositoryTest
   {
      [Test]
      public void ShouldSaveRole()
      {
         using (IRepository<Role> roleRepository = new Repository<Role>(new MusicStoreContext()))
         {
            var role = roleRepository.Query(r => r.Name.Equals("Administrator", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (null != role)
            {
               roleRepository.Delete(role);
            }
            role = roleRepository.Query(r => r.Name.Equals("Administrator", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            Assert.IsNotNull(role);
            roleRepository.Add(new Role
                                  {
                                     Name = "Administrator"
                                  });
            roleRepository.Save();
            role = roleRepository.Query(r => r.Name.Equals("Administrator", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            Assert.IsNotNull(role);
         }
      }

      [Test]
      public void ShouldSaveUserWithRole()
      {
         using (IRepository<Role> roleRepository = new Repository<Role>(new MusicStoreContext()))
         {
            var role = new Role { Name = "Sales" };
            role.Users.Add(new User
                              {
                                 UserName = "Sean",
                                 Password = "aaaaaaaaaa",
                                 Name = "sean.xu",
                                 Email = "sean.xu@sungard.com"
                              });
            roleRepository.Add(role);
            roleRepository.Save();
         }
         Role selectedRole;
         using (IRepository<Role> roleRepository = new Repository<Role>(new MusicStoreContext()))
         {
            selectedRole = roleRepository.Query(role => role.Name.Equals("Sales")).FirstOrDefault();
         }

         using (IRepository<Role> roleRepository = new Repository<Role>(new MusicStoreContext()))
         {
            selectedRole.Name = "Customer";
            selectedRole.Users.Add(new User
                                      {
                                         UserName = "Sean2",
                                         Password = "aaaaaaaaaa2",
                                         Name = "sean.xu2",
                                         Email = "sean.xu2@sungard.com"
                                      });
            roleRepository.Update(selectedRole);
            roleRepository.Save();
         }
         using (IRepository<Role> roleRepository = new Repository<Role>(new MusicStoreContext()))
         {
            selectedRole = roleRepository.Query(role => role.Name.Equals("Customer")).FirstOrDefault();
            roleRepository.Delete(selectedRole);
            roleRepository.Save();
         }
      }
   }
}

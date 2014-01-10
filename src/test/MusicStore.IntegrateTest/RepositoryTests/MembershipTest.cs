using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Infrastructure.SecurityProviders;
using NUnit.Framework;

namespace MusicStore.IntegrateTest.RepositoryTests
{
   [TestFixture]
   public class MembershipTest
   {
      [Test]
      public void ShouldSaveCreatedUserAndDelete()
      {
         var membershipProvider = new MusicStoreMembershipProvider();
         membershipProvider.CreateUser("jasper", "jasper.chiu", "ace.hot-123", "jasper.chiu@163.com", "Administrator");
         membershipProvider.DeleteUser("jasper",true);
      }
   }
}

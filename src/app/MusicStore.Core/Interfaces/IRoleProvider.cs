using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Core.Models;

namespace MusicStore.Core.Interfaces
{
   public interface IRoleProvider
   {
      bool IsUserInRole(string userName, string roleName);
      string[] GetRolesForUser(string userName);
      void CreateRole(string roleName);
      bool DeleteRole(string roleName, bool deleteAllRelatedData);
      bool RoleExists(Role role);
   }
}

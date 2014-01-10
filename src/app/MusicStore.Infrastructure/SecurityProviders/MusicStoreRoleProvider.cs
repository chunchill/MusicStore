using System.IO;
using MusicStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using MusicStore.Core.Models;
using MusicStore.Infrastructure.DataAccess;
using MusicStore.Infrastructure.Repositories;

namespace MusicStore.Infrastructure.SecurityProviders
{
   public class MusicStoreRoleProvider : RoleProvider, IRoleProvider
   {

      public override bool IsUserInRole(string userName, string roleName)
      {
         using (var context = new MusicStoreContext())
         {
            var roleRepository = new Repository<Role>(context);
            var userRepository = new Repository<User>(context);
            var role =
               roleRepository.Query(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (role == null)
               throw new InvalidDataException("sorry,the role does not exsit!");
            var user = userRepository.Query(u => u.UserName.Equals(userName) && u.RoleId.Equals(role.RoleId));
            return user != null;
         }
      }

      public override string[] GetRolesForUser(string userName)
      {
         throw new NotImplementedException();
      }

      public override void AddUsersToRoles(string[] usernames, string[] roleNames)
      {
         throw new NotImplementedException();
      }

      public override string ApplicationName
      {
         get
         {
            throw new NotImplementedException();
         }
         set
         {
            throw new NotImplementedException();
         }
      }

      public override void CreateRole(string roleName)
      {
         using (var context = new MusicStoreContext())
         {
            var roleRepository = new Repository<Role>(context);
            var existedrole = roleRepository.Query(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (existedrole != null)
               throw new InvalidDataException("sorry,the role already exists!");
            roleRepository.Add(new Role { Name = roleName });
            context.Commit();
         }
      }

      public bool RoleExists(Role role)
      {
         if (role == null)
            return false;
         using (var context = new MusicStoreContext())
         {
            var roleRepository = new Repository<Role>(context);
            return roleRepository.Query(r => r.RoleId == role.RoleId || r.Name == role.Name).FirstOrDefault() != null;
         }
      }

      public override bool DeleteRole(string roleName, bool deleteAllRelatedData)
      {
         using (var context = new MusicStoreContext())
         {
            var roleRepository = new Repository<Role>(context);
            var roleToDelete = roleRepository.Query(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (roleToDelete == null)
               throw new InvalidDataException("the role to be deleted does not exist in the app");
            if (deleteAllRelatedData)
            {
               var userRepository = new Repository<User>(context);
               var users = userRepository.Query(u => u.RoleId.Equals(roleToDelete.RoleId));
               foreach (var user in users)
               {
                  userRepository.Delete(user);
               }
            }
            roleRepository.Delete(roleToDelete);
            context.Commit();
         }
         return true;
      }

      public override string[] FindUsersInRole(string roleName, string usernameToMatch)
      {
         throw new NotImplementedException();
      }

      public override string[] GetAllRoles()
      {
         throw new NotImplementedException();
      }

      public override string[] GetUsersInRole(string roleName)
      {
         throw new NotImplementedException();
      }

      public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
      {
         throw new NotImplementedException();
      }

      public override bool RoleExists(string roleName)
      {
         throw new NotImplementedException();
      }
   }
}

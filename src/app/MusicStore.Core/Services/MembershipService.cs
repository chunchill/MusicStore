using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;

namespace MusicStore.Core.Services
{
   public class MembershipService
   {
      private readonly IMembershipProvider _membershipProvider;
      private readonly IRoleProvider _roleProvider;

      public MembershipService(IMembershipProvider membershipProvider, IRoleProvider roleProvider)
      {
         _membershipProvider = membershipProvider;
         _roleProvider = roleProvider;
      }

      public bool ValidateUser(string username, string password)
      {
         return _membershipProvider.ValidateUser(username, password);
      }

      public bool ChangePassword(string username, string oldPassword, string newPassword)
      {
         return _membershipProvider.ChangePassword(username, oldPassword, newPassword);
      }

      public void CreateUser(string username, string name, string password, string email, string roleName)
      {
         _membershipProvider.CreateUser(username, name, password, email, roleName);
      }

      public void DeleteTheUser(string username, bool deleteAllRelatedData = false)
      {
         _membershipProvider.DeleteUser(username, deleteAllRelatedData);
      }

      public bool UserExists(User user)
      {
         return _membershipProvider.UserExists(user);
      }

      public void DeleteTheRole(string rolename, bool deleteAllRelatedData = false)
      {
         _roleProvider.DeleteRole(rolename, deleteAllRelatedData);
      }

      public void CreateRole(string rolename)
      {
         _roleProvider.CreateRole(rolename);
      }

      public bool RoleExists(Role role)
      {
         return _roleProvider.RoleExists(role);
      }

      public bool IsUserInRole(string userName, string roleName)
      {
         return _roleProvider.IsUserInRole(userName, roleName);
      }

   }
}

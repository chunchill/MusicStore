using MusicStore.Core.Models;

namespace MusicStore.Core.Interfaces
{
   public interface IMembershipProvider
   {
      bool ValidateUser(string username, string password);
      bool ChangePassword(string username, string oldPassword, string newPassword);
      void CreateUser(string username, string name, string password, string email, string roleName);
      bool UserExists(User user);
      bool DeleteUser(string username, bool deleteAllRelatedData);
      void UpdateUser(User user);

   }
}

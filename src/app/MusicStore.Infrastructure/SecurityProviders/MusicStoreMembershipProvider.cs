using System.IO;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;
using MusicStore.Infrastructure.DataAccess;
using MusicStore.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Web.Security;

namespace MusicStore.Infrastructure.SecurityProviders
{
   public class MusicStoreMembershipProvider : MembershipProvider, IMembershipProvider
   {
      public override bool ValidateUser(string username, string password)
      {
         if (string.IsNullOrEmpty(password.Trim()) || string.IsNullOrEmpty(username.Trim()))
            return false;
         using (IRepository<User> userRepository = new Repository<User>(new MusicStoreContext()))
         {
            return userRepository.Query(u => u.Password.Equals(password) && u.UserName.Equals(username)).Any();
         }
      }

      public override bool ChangePassword(string username, string oldPassword, string newPassword)
      {
         if (!ValidateUser(username, oldPassword) || string.IsNullOrEmpty(newPassword.Trim()))
            return false;
         using (IRepository<User> userRepository = new Repository<User>(new MusicStoreContext()))
         {
            var user = userRepository.Query(u => u.UserName.Equals(username)).FirstOrDefault();
            if (user == null)
               throw new InvalidDataException("sorry,the user does not exist!");
            user.Password = oldPassword;
            userRepository.Save();
         }
         return true;
      }

      public void CreateUser(string username, string name, string password, string email, string roleName)
      {
         using (var context = new MusicStoreContext())
         {
            var roleRepository = new Repository<Role>(context);
            var userRepository = new Repository<User>(context);
            var existedrole = roleRepository.Query(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (existedrole == null)
               throw new InvalidDataException("sorry,the role of the user does not exist!");
            var existedUser = userRepository.Query(r => r.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && r.RoleId.Equals(existedrole.RoleId)).FirstOrDefault();
            if (existedUser != null)
               throw new InvalidDataException("sorry,the user already exist in the app!");
            var newUser = new User
            {
               UserName = username,
               Name = name,
               Password = password,
               Email = email,
               RoleId = existedrole.RoleId
            };
            userRepository.Add(newUser);
            context.Commit();
         }
      }

      public bool UserExists(User user)
      {
         if (user == null)
            return false;
         using (var context = new MusicStoreContext())
         {
            var userRepository = new Repository<User>(context);
            return userRepository.Query(u => u.UserId == user.UserId || u.UserName == user.UserName).FirstOrDefault() != null;
         }
      }

      public override bool DeleteUser(string username, bool deleteAllRelatedData)
      {
         using (var context = new MusicStoreContext())
         {
            var userRepository = new Repository<User>(context);
            var userToDelete = userRepository.Query(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            userRepository.Delete(userToDelete);
            context.Commit();
         }
         return true;
      }

       public void UpdateUser(User user)
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

      public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
      {
         throw new NotImplementedException();
      }

      public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
      {
         throw new NotImplementedException();
      }

      public override bool EnablePasswordReset
      {
         get { throw new NotImplementedException(); }
      }

      public override bool EnablePasswordRetrieval
      {
         get { throw new NotImplementedException(); }
      }

      public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
      {
         throw new NotImplementedException();
      }

      public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
      {
         throw new NotImplementedException();
      }

      public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
      {
         throw new NotImplementedException();
      }

      public override int GetNumberOfUsersOnline()
      {
         throw new NotImplementedException();
      }

      public override string GetPassword(string username, string answer)
      {
         throw new NotImplementedException();
      }

      public override MembershipUser GetUser(string username, bool userIsOnline)
      {
         throw new NotImplementedException();
      }

      public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
      {
         throw new NotImplementedException();
      }

      public override string GetUserNameByEmail(string email)
      {
         throw new NotImplementedException();
      }

      public override int MaxInvalidPasswordAttempts
      {
         get { throw new NotImplementedException(); }
      }

      public override int MinRequiredNonAlphanumericCharacters
      {
         get { throw new NotImplementedException(); }
      }

      public override int MinRequiredPasswordLength
      {
         get { throw new NotImplementedException(); }
      }

      public override int PasswordAttemptWindow
      {
         get { throw new NotImplementedException(); }
      }

      public override MembershipPasswordFormat PasswordFormat
      {
         get { throw new NotImplementedException(); }
      }

      public override string PasswordStrengthRegularExpression
      {
         get { throw new NotImplementedException(); }
      }

      public override bool RequiresQuestionAndAnswer
      {
         get { throw new NotImplementedException(); }
      }

      public override bool RequiresUniqueEmail
      {
         get { throw new NotImplementedException(); }
      }

      public override string ResetPassword(string username, string answer)
      {
         throw new NotImplementedException();
      }

      public override bool UnlockUser(string userName)
      {
         throw new NotImplementedException();
      }

      public override void UpdateUser(MembershipUser user)
      {
         throw new NotImplementedException();
      }
   }
}

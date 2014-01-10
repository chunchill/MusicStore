using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MusicStore.Core.Models;

namespace MusicStore.Infrastructure.Validators
{
   public class UserValidator : AbstractValidator<User>
   {
      public UserValidator()
      {
         RuleSet("ChangePassword", () =>
                                     {
                                        RuleFor(user => user.UserName).NotEmpty();
                                        RuleFor(user => user.Password).NotEmpty();
                                     });


      }
   }
}

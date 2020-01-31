using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SportGoods.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SportGoods.Domain.Concrete
{
    public class CustomSignInManager : SignInManager<User, string>
    {
        public CustomSignInManager(CustomUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((CustomUserManager)UserManager);
        }

        public static CustomSignInManager Create(IdentityFactoryOptions<CustomSignInManager> options, IOwinContext context)
        {
            return new CustomSignInManager(context.GetUserManager<CustomUserManager>(), context.Authentication);
        }
    }
}

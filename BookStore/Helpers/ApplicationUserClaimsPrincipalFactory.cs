using BookStore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Helpers
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);
            claimsIdentity.AddClaim(new Claim("UserFirstName", user.FirstName ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("UserLastName", user.LastName ?? string.Empty));

            return claimsIdentity;
        }
        //public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        //{
        //    ClaimsPrincipal claimsPrincipal = await base.CreateAsync(user);
        //    ((ClaimsIdentity)claimsPrincipal.Identity).AddClaim(new Claim("UserFirstName", user.FirstName ?? string.Empty));
        //    ((ClaimsIdentity)claimsPrincipal.Identity).AddClaim(new Claim("UserLastName", user.LastName ?? string.Empty));

        //    return claimsPrincipal;
        //}
    }
}

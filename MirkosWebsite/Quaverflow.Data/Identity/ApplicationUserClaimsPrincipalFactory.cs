using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicTechnologies.Data
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<QuaverflowUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<QuaverflowUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options):
            base (userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(QuaverflowUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("DOB", user.DOB.ToShortDateString()));
            identity.AddClaim(new Claim("FullName", user.FullName));

            return identity;
        }
    }
}

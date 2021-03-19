using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UdemyIdentity.Models;

namespace UdemyIdentity.ClaimProvider
{
    public class ClaimProvider : IClaimsTransformation
    {
        public UserManager<AppUser> userManager { get; set; }

        public RoleManager<AppRole> roleManager { get; set; }

        public ClaimProvider(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal != null && principal.Identity.IsAuthenticated)
            {
                var identity = principal.Identity as ClaimsIdentity;

                var user = await userManager.FindByNameAsync(identity.Name);

                if (user != null)
                {
                    //var roles = await userManager.GetRolesAsync(user);
                    //foreach (var roleName in roles)
                    //{
                    //    //identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                    //    if (roleManager.SupportsRoleClaims)
                    //    {
                    //        var role = await roleManager.FindByNameAsync(roleName);
                    //        if (role != null)
                    //        {
                    //            identity.AddClaims(await roleManager.GetClaimsAsync(role));
                    //        }
                    //    }
                    //}

                    if (user.BirthDay != null)
                    {
                        var today = DateTime.Today;
                        var age = today.Year - user.BirthDay?.Year;

                        if (age > 15)
                        {
                            var violenceClaim = new Claim("violence", true.ToString(), ClaimValueTypes.String, "Internal");
                            identity.AddClaim(violenceClaim);
                        }
                    }

                    if (user.City != null)
                    {
                        if (!principal.HasClaim(c => c.Type == "city"))
                        {
                            var cityClaim = new Claim("city", user.City, ClaimValueTypes.String, "Internal");
                            identity.AddClaim(cityClaim);
                        }
                    }
                }
            }

            return principal;
        }
    }
}
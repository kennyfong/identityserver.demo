using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Demo.IdentityServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace IdentityServer4.Demo.IdentityServer.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //repository to get user from db
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository; //DI
        }

        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                var user = await _userRepository.FindAsync(context.UserName, context.Password);
                if (user != null)
                {
                    //set the result
                    context.Result = new GrantValidationResult(
                        subject: user.UserId.ToString(),
                        authenticationMethod: "custom",
                        claims: GetUserClaims(user));

                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        //build claims array from user data
        public static Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
            new Claim("user_id", user.UserId.ToString() ?? ""),
            new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName)) ? (user.FirstName + " " + user.LastName) : ""),
            new Claim(JwtClaimTypes.GivenName, user.FirstName  ?? ""),
            new Claim(JwtClaimTypes.FamilyName, user.LastName  ?? ""),
            new Claim(JwtClaimTypes.Email, user.Email  ?? ""),

            //roles
            //new Claim(JwtClaimTypes.Role, user.Role)
            };
        }

    }
}
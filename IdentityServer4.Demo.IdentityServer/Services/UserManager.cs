using Dapper;
using IdentityModel;
using IdentityServer4.Demo.IdentityServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4.Demo.IdentityServer.Services
{
    public class AutocabUserManager : IProfileService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public AutocabUserManager(IOptionsSnapshot<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<User> Find(string username, string password)
        {
            const string query = @"Select UserId, UserName, Email, FirstName, LastName
                                From Users
                                Where Username = @Username and Password = @Password";

            using (var conn = new SqlConnection(_appSettings.ConnectionString))
            {
                await conn.OpenAsync();

                return (await conn.QueryAsync<User>(query, new { Username = username, Password = password })).FirstOrDefault();
            }

        }

        public Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("FirstName", user.FirstName)
            };

            //custom database call here to where you store your claims.
            //var myClaims = ...Your Database call here...
        //var claimGroupName = "SomeCustomName";

            //if (security != null && security.Count() > 0)
            //{
            //    foreach (var claim in security)
            //    {
            //        //Add the value from the field Security_Id from the database to the claim group "SomeCustomName".
            //        claims.Add(new Claim("FirstName", claim.SECURITY_ID));
            //    }
            //}

            return Task.FromResult(claims);


        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //throw new NotImplementedException();
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //var user = _userRepository.FindByUserIdAsync(context.Subject.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Id));
            //context.IsActive = bool.Parse(context.Subject.Claims.FirstOrDefault(x => x.Type == "IsActive").Value);

            return Task.FromResult(0);
        }
    }
}

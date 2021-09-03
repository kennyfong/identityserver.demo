using Dapper;
using IdentityServer4.Demo.IdentityServer.Models;
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
    public class UserManager
    {
        private SecurityContext _context;
        private readonly AppSettings _appSettings;

        public UserManager(SecurityContext context, IOptionsSnapshot<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<User> Find(string username, string password)
        {
            const string query = @"Select UserId, UserName, Email, FirstName, LastName
                                From Users
                                Where Username = @Username";

            using (var conn = new SqlConnection(_appSettings.ConnectionString))
            {
                await conn.OpenAsync();

                return (await conn.QueryAsync<User>(query, new { Username = username })).FirstOrDefault();
            }

        }

        public Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>();

            //custom database call here to where you store your claims.
            var myClaims = ...Your Database call here...
        var claimGroupName = "SomeCustomName";

            if (security != null && security.Count() > 0)
            {
                foreach (var claim in security)
                {
                    //Add the value from the field Security_Id from the database to the claim group "SomeCustomName".
                    claims.Add(new Claim(claimGroupName, claim.SECURITY_ID));
                }
            }

            return Task.FromResult(claims);


        }

    }
}

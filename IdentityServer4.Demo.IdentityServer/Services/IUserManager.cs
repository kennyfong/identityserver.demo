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
    public interface IUserManager
    {
        Task<User> Find(string username, string password);


        Task<List<Claim>> GetClaimsAsync(User user);

    }
}

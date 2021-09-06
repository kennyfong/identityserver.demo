using IdentityServer4.Demo.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Demo.IdentityServer.Services
{
    public interface IUserRepository
    {
        Task<User> FindAsync(string userName, string password);
        Task<User> FindByUsernameAsync(string userName);
        Task<User> FindByUserIdAsync(int userId);
    }
}

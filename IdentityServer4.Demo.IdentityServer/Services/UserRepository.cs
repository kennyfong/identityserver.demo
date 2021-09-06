using Dapper;
using IdentityServer4.Demo.IdentityServer.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Demo.IdentityServer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;

        public UserRepository(IOptionsSnapshot<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<User> FindAsync(string username, string password)
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

        public async Task<User> FindByUsernameAsync(string username)
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

        public async Task<User> FindByUserIdAsync(int userId)
        {
            const string query = @"Select UserId, UserName, Email, FirstName, LastName
                                From Users
                                Where UserId = @UserId";

            using (var conn = new SqlConnection(_appSettings.ConnectionString))
            {
                await conn.OpenAsync();

                return (await conn.QueryAsync<User>(query, new { UserId = userId })).FirstOrDefault();
            }
        }
    }
}
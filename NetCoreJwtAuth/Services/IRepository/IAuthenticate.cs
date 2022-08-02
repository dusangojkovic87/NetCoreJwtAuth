using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreJwtAuth.Entities;

namespace NetCoreJwtAuth.Services.IRepository
{
    public interface IAuthenticate
    {
        bool isRegistered(User user);
        Task<bool> RegisterUser(User user);
        string HashPassword(string password);
        User GetUserByEmail(string email);
        bool CheckPassword(string passwordFromDb, string password);
    }
}
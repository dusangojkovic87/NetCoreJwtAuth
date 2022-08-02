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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreJwtAuth.DataContext;
using NetCoreJwtAuth.Entities;
using NetCoreJwtAuth.Services.IRepository;

namespace NetCoreJwtAuth.Services.Repository
{
    public class Authenticate : IAuthenticate
    {
        private AplicationDbContext _context;
        public Authenticate(AplicationDbContext context)
        {
            _context = context;

        }


        public bool isRegistered(User user)
        {
            var result = _context.User.FirstOrDefault(x => x.Email == user.Email);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RegisterUser(User user)
        {
            await _context.User.AddAsync(user);
            var result = _context.SaveChangesAsync();
            if (result.IsCompletedSuccessfully)
            {
                return true;

            }

            return false;
        }
    }
}
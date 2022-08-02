using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreJwtAuth.DataContext;
using NetCoreJwtAuth.Entities;
using NetCoreJwtAuth.Services.IRepository;
using BC = BCrypt.Net.BCrypt;

namespace NetCoreJwtAuth.Services.Repository
{
    public class Authenticate : IAuthenticate
    {
        private AplicationDbContext _context;
        public Authenticate(AplicationDbContext context)
        {
            _context = context;

        }

        public bool CheckPassword(string passwordFromDb, string password)
        {
            return BC.Verify(password, passwordFromDb);
        }

        public User GetUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(x => x.Email == email);
        }

        public string HashPassword(string password)
        {
            return BC.HashPassword(password);
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
            var result = await _context.SaveChangesAsync() > 0;
            if (result)
            {
                return true;

            }

            return false;
        }
    }
}
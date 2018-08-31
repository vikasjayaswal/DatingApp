using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            var user  = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return null;
            }

            

            return user;
            //throw new System.NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var new_user = new User();
            new_user.PasswordHash = passwordHash;
            new_user.PasswordSalt = passwordSalt;
            new_user.Username = user.Username;
            
            // Register User

            await this._context.Users.AddAsync(new_user);
            await this._context.SaveChangesAsync();

            //if(!VerifyPassword(password,user.PasswordHash, user.PasswordSalt))
         //   { 
          //      return null;
          //  }
            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
        
        using(var hmac =  new HMACSHA512())
         {
             var computed_hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            if(passwordHash.Equals(computed_hash) == false)
            {
                return false;
            }  else 
            {
                return true;
            }       
         } 
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
          
         using(var hmac =  new HMACSHA512())
         {
             passwordSalt = hmac.Key;
             passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
         }
          
        }


        public async Task<bool> UserExists(string username)
        {
           if(await _context.Users.AnyAsync(user => user.Username == username))
           {
               return true;
           }
            return false;
        }
    }
}
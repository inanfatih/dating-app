using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        //Asagida DataContext inject edilmis oluyor.
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            // asagidaki out ile passwordHash ve passwordSalt yukaridakilere refer edilmesini sagliyor. Boylece method'a gonderilenler degistirilince, yukaridakiler de degismis oluyor. 
            //Normalde CreatePasswordHash method u birsey return etmiyor. Fakat, method'un parametrelerindeki password u alip, yine parametreler icindeki passwordHash ve passwordSalt u donup yukarida tanimlanmis bu variable lari guncelliyor.
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //Asagidaki method ile Users tablosuna async olarak user i ekliyoruz.
            await _context.Users.AddAsync(user);
            //Asagidaki method ile db de yapilan degisiklikleri save ediyoruz.
            await _context.SaveChangesAsync();

            //Bu method ile ozetle user ve password u alip, user'a PasswordHash ve PasswordSalt ekleyip bu user i return ediyoruz.
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Asagidaki HMACSHA512 icerisinde dispose methodu var. Bu method u kullanmak icin asagidaki sekilde using() icine koyduk hmac i. Bu sayede {} icindeki seyler islendikten sonra dispose hemen edilecek
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // randomly generated key
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username)) return true;
            return false;
        }
    }
}
using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public static class PasswordHasher
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {

                passwordSalt = hmac.Key;

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);//string to byte[]

                passwordHash = hmac.ComputeHash(passwordBytes);//hashlama emeliyyati
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {

            using (var hmac = new HMACSHA512(storedSalt))
            {

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] computedHash = hmac.ComputeHash(passwordBytes);

                bool hashesMatch = computedHash.SequenceEqual(storedHash);

                return hashesMatch;
            }
        }
    }
}

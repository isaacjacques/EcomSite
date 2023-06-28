using System.Security.Cryptography;
using System.Text;

namespace EcomAPI.Models
{
    public class Customer
    {
        public long CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public IEnumerable<CustomerCart> Cart { get; set; }
        public DateTime CreationTime { get; set; }

        public void SetPassword(string password)
        {
            this.PasswordSalt = GenerateSalt(16);
            this.PasswordHash = GenerateHash(password, this.PasswordSalt);
        }

        private string GenerateSalt(int length)
        {
            byte[] salt = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        private string GenerateHash(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, passwordBytes.Length, saltBytes.Length);

            using (var hashAlgorithm = SHA256.Create())
            {
                byte[] hashBytes = hashAlgorithm.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool ValidatePassword(string password)
        {
            return (PasswordHash == GenerateHash(password, this.PasswordSalt));
        }
    }
}

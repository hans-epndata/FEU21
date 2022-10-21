using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Models.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] Hash { get; private set; }
        public byte[] Salt { get; private set; }

        public void CreatePassword(string password)
        {
            using var hmac = new HMACSHA512();
            Salt = hmac.Key;
            Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public bool ValidatePassword(string password)
        {
            using var hmac = new HMACSHA512(Salt);
            var _hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for(int i=0; i < _hash.Length; i++)
                if (_hash[i] != Hash[i])
                    return false;

            return true;
        }
    }
}
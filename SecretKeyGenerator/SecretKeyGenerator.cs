using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SecretKeyGenerator
{
    public class SecretKeyGenerator
    {
        public static string GenerateSecretKey(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[length];
                rng.GetBytes(byteArray);
                return Convert.ToBase64String(byteArray);
            }
        }
        public static void Main(string[] args)
        {
            var secretKey = GenerateSecretKey(32); // יצירת מפתח סודי באורך 32 בייט (256 ביט)
            Console.WriteLine("Your secret key: " + secretKey);
        }

}
}

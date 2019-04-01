using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BeerTicket.API.BusinessLayer
{
    public class Cryption
    {

        /// <param name="InputText">Data to be encrypted</param>
        /// <param name="Password">The string to used for making the key.The same string
        public static string GetEncryptedSHA256(string strPassword)
        {
            string guid = System.Guid.NewGuid().ToString();
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(guid);
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            var token = Regex.Replace(result.ToString(), "[^0-9a-zA-Z]+", "");
            return token;
        }

    }
}
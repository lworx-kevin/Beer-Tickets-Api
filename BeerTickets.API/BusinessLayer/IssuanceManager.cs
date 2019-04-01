using System;

namespace BeerTicket.API.BusinessLayer
{
    public class IssuanceManager
    {
        /// <summary>
        /// this  is created an Encripted token 
        /// </summary>
        /// <returns></returns>
        public string CreateToken()
        {
            byte[] gb = Guid.NewGuid().ToByteArray();
            long l = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                l *= ((int)b + 1);
            }
            String RandomNumb = l.ToString().Replace("-", "").Substring(1, 8);
            return Cryption.GetEncryptedSHA256(RandomNumb);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GenerateToken(string token)
        {
            
            return "";
        }

    }
}
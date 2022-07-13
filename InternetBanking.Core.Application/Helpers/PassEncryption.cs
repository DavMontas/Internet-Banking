using System.Security.Cryptography;
using System.Text;

namespace InternetBanking.Core.Application.Helpers
{
    public class PassEncryption
    {
        public static string ComputeSha256Hasg(string pass)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));

                StringBuilder sB = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sB.Append(bytes[i].ToString("x2"));
                }
                return sB.ToString();
            }

        }
    }
}

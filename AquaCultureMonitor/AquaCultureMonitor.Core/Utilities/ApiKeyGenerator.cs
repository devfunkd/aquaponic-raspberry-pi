using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AquaCultureMonitor.Core.Utilities
{
    public class ApiKeyGenerator
    {
        public static string Generate()
        {
            const string secretKey = "secretkey";

            var salt = Guid.NewGuid().ToString();
            var hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(salt));

            var encodedSignature = Convert.ToBase64String(signature);
            return System.Web.HttpUtility.UrlEncode(encodedSignature);
        }
    }
}

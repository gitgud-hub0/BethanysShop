using System;
using System.Security.Cryptography;
using System.Text;

namespace BethanysShop.Helpers
{
    public static class ExtensionHelper
    {
        public static Guid ToMD5Hash(this string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }
    }
}

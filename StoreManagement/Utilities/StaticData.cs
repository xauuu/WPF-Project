using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Utilities
{
    public class StaticData
    {
        public static int UserID;

        public static int RoleID;

        public static User User;

        public static int ONEYEAR = 365;

        public static string[] FilterLabels = { "Tất cả", "Giá tăng dần", "Giá giảm dần" };

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                byte[] b = Convert.FromBase64String(plainText);
                return System.Text.Encoding.UTF8.GetString(b);
            }
            return null;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}

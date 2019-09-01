using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.DAL
{
    public class Utility
    {
        public string HashData(string data)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
            sha512.Clear();
            return Convert.ToBase64String(EncryptedSHA512);
        }

        public bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }

    }
}
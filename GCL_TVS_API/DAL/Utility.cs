using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        public string Postclient(object obj)
        {
            string baseAddress = System.Configuration.ConfigurationManager.AppSettings["ExternalUrl"];
            var result = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var _response = client.PostAsync(baseAddress, new StringContent(JsonConvert.SerializeObject(obj).ToString(), Encoding.UTF8, "application/json")).Result;

                    if (_response.IsSuccessStatusCode)
                    {
                        result = _response.Content.ReadAsAsync<string>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
             
            return result;
        }
    }
}
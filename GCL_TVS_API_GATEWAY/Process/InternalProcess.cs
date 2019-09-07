using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace GCL_TVS_API_GATEWAY.Process
{
    public class InternalProcess
    {
        public string PostclientGetToken(object ObjData)
        {
            string functionName = "SystemAuthen";
            string baseAddress = System.Configuration.ConfigurationManager.AppSettings["ExternalUrl"] + functionName;
            //requestToken.SystemID = System.Configuration.ConfigurationManager.AppSettings["SystemID"];
            try
            {
                using (var client = new HttpClient())
                {
                    var _response = client.PostAsync(baseAddress, new StringContent(JsonConvert.SerializeObject(ObjData).ToString(), Encoding.UTF8, "application/json")).Result;
                    
                    if (_response.IsSuccessStatusCode)
                    {
                        //result = _response.Content.ReadAsAsync<ObjData>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "";
        }

        public T PostObjToAPI<T>(ref bool isSuccess, object obj, string baseAddress, string requstUri) where T : new()
        {
            T objResult;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    var _response = client.PostAsync(requstUri, new StringContent(JsonConvert.SerializeObject(obj).ToString(), Encoding.UTF8, "application/json")).Result;

                    if (_response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }

                    objResult = _response.Content.ReadAsAsync<T>().Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return objResult;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web;
namespace CIMB.DSE.ML.API.Gateway.Controllers
{
    public class BaseController : ApiController
    {
        private string _baseAddress = System.Configuration.ConfigurationManager.AppSettings["InternalAPIUrl"];
        private string baseAddress
        {
            set { _baseAddress = value; }
            get { return _baseAddress; }
        }
        
        protected string apiPathAndQuery { get; set; }
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);


            HttpRequestMessage request = controllerContext.Request;
            // request.GetRouteData

            var controller = request.GetRouteData().Values["controller"];
            var acction = request.GetRouteData().Values["action"];
            var RequestURLParams = request.RequestUri.Query;           

            apiPathAndQuery = string.Format("/{0}/{1}{2}", controller, acction, RequestURLParams);
        }

        protected T GetDataFromAPINotAuthen<T>(string pathAndQuery) where T : new()
        {
            T ObjReturn = new T();
            bool isSuccess = false;
            var _obj = GetObjFormAPI<T>(ref isSuccess, baseAddress, pathAndQuery);
            if (_obj != null && _obj is T)
            {
                ObjReturn = _obj;
            }
            else if (_obj != null)
            {
                ObjReturn = _obj;
            }

            //helper.MarkPrivateNumber<T>(ref ObjReturn);
            return ObjReturn;
        }

        protected T PostDataToAPINotAuth<T>(string _requstUri, object objPost) where T : new()
        {
            T ObjReturn = new T();
            bool isSuccess = false;
            var _obj = PostObjToAPI<T>(ref isSuccess, objPost, baseAddress, _requstUri);
            if (_obj != null && _obj is T)
            {
                ObjReturn = _obj;
            }
            else if (_obj != null)
            {
                ObjReturn = _obj;
            }
            return ObjReturn;
        }

        protected T GetObjFormAPI<T>(ref bool isSuccess, string baseAddress, string requstUri) where T : new()
        {
            T objResult;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    requstUri = baseAddress.TrimEnd('/') + requstUri;
                    HttpResponseMessage _response = client.GetAsync(requstUri).Result;

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

        protected T PostObjToAPI<T>(ref bool isSuccess, object obj, string baseAddress, string requstUri) where T : new()
        {
            T objResult;

            try
            {
                using (var client = new HttpClient())
                {
                    requstUri = baseAddress.TrimEnd('/') + requstUri;
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

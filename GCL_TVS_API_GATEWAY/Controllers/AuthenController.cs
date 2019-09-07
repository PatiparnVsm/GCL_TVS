using CIMB.DSE.ML.API.Gateway.Controllers;
using GCL_TVS_API_GATEWAY.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API_GATEWAY.Controllers
{
    public class AuthenController : BaseController
    {
        private static AuthenProcess _process = null;
        private static AuthenProcess process
        {
            get { return (_process == null) ? _process = new AuthenProcess() : _process; }
        }

        [AllowAnonymous]
        [HttpPost]
        public dynamic SystemAuthen([FromBody] RequestToken data)
        {
            dynamic res = null;

            try
            { 
                var resInternal = base.PostDataToAPINotAuth<dynamic>(base.apiPathAndQuery, data);
                if (resInternal.access_token != null)
                {
                    res = process.GenerateToken(data);
                    if (res.access_token == null)
                    {
                        res = new ErrorAuthen();
                        res.status = new StatusError();
                        res.status.code = "204";
                        res.status.message = "SystemID invalid";
                    }
                }
                else
                {
                    res = resInternal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [AllowAnonymous]
        [HttpPost]
        public dynamic UserAuthen([FromBody] AuthenByUser data)
        {
            dynamic res = null;

           try
            { 
                var resInternal = base.PostDataToAPINotAuth<dynamic>(base.apiPathAndQuery, data);
                if (resInternal.access_token != null)
                {
                    res = process.GenerateTokenByUser(resInternal);
                    if (res.access_token == null)
                    {
                        res = new ErrorAuthen();
                        res.status = new StatusError();
                        res.status.code = "99";
                        res.status.message = "System Error!";
                    }
                }
                else
                {
                    res = resInternal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}

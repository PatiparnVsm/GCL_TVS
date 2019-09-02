using GCL_TVS_API.Filters;
using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.Controllers
{
    public class AuthenController : ApiController
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
                res = process.GenerateToken(data);
                if(res.access_token == null)
                {
                    res = new ErrorAuthen();
                    res.status = new StatusError();
                    res.status.code = "01";
                    res.status.code = "SystemID invalid";
                }
            }
            catch(Exception ex)
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
                res = new ResponseTokenByUser();
                res = process.GenerateTokenByUser(data);
                if (res.access_token == null)
                {
                    res = new ErrorAuthen();
                    res.status = new StatusError();
                    res.status.code = "01";
                    res.status.code = "Username or Password invalid";
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

using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.Controllers
{
    public class TokenController : ApiController
    {
        private static TokenProcess _process = null;
        private static TokenProcess process
        {
            get { return (_process == null) ? _process = new TokenProcess() : _process; }
        }

        [HttpPost]
        public ResponseToken RequestToken([FromBody] RequestToken data)
        {
            ResponseToken res = new ResponseToken();

            try
            {
                res = process.GenerateToken(data);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}

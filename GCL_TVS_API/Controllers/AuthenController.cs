using GCL_TVS_API.DAL;
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

        [HttpPost]
        public ResponseToken SystemAuthen([FromBody] RequestToken data)
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

        [HttpPost]
        public ResponseTokenByUser UserAuthen([FromBody] AuthenByUser data)
        {
            ResponseTokenByUser res = new ResponseTokenByUser();
            
            try
            {                
                res = process.GenerateTokenByUser(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}

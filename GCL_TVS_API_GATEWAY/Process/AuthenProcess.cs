using System;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API_GATEWAY.Process
{
    public class AuthenProcess
    {
        public ResponseToken GenerateToken(RequestToken data)
        {
            ResponseToken res = new ResponseToken();

            try
            {
                res = JwtManager.GenerateToken();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public dynamic GenerateTokenByUser(object data)
        {
            //ResponseTokenByUser res = new ResponseTokenByUser();
            dynamic res = data;
            try
            {
                if (res.UserID != null)
                {
                    var token = JwtManager.GenerateToken();
                    res.access_token = token.access_token;
                    res.expires_in = token.expires_in;
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
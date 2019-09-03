using GCL_TVS_API.DAL;
using System;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.Process
{
    public class AuthenProcess
    {
        private static AuthenDAL _tokenDAL = null;
        private static AuthenDAL tokenDAL
        {
            get { return (_tokenDAL == null) ? _tokenDAL = new AuthenDAL() : _tokenDAL; }
        }

        private static Utility _util = null;
        private static Utility util
        {
            get { return (_util == null) ? _util = new Utility() : _util; }
        }


        //private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public ResponseToken GenerateToken(RequestToken data)
        {
            ResponseToken res = new ResponseToken();

            try
            {
                if (util.IsGuid(data.SystemID))
                {
                    var resultValidate = tokenDAL.ValidateSystemId(data.SystemID);
                    if (resultValidate == true)
                    {
                        res = JwtManager.GenerateToken();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public ResponseTokenByUser GenerateTokenByUser(AuthenByUser data)
        {
            ResponseTokenByUser res = new ResponseTokenByUser();

            try
            {
                var TokenId = util.PostclientGetToken();
                //Hash Password
                var hashPassword = util.PostclientGetHash(data, TokenId.access_token);

                res = tokenDAL.GetUserDetails(data.UserName, hashPassword);
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
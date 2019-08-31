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

        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        private static string _expireMinutes = null;
        private static string expireMinutes
        {
            get
            {
                if (_expireMinutes == null)
                {
                    _expireMinutes = System.Configuration.ConfigurationManager.AppSettings["tokenExpiry"];
                }
                return _expireMinutes;
            }
        }

        public ResponseToken GenerateToken(RequestToken data)
        {
            ResponseToken res = new ResponseToken();

            try
            {
                if (IsGuid(data.systemId))
                {
                    res.tokenId = tokenDAL.GetToken(data.systemId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public ResponseToken GenerateTokenByUser(RequestToken data)
        {
            ResponseToken res = new ResponseToken();

            try
            {
                //Hash Password
                res.tokenId = tokenDAL.GetTokenbyUser(data.userName, data.password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }

    }
}
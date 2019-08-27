using GCL_TVS_API.DAL;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.Process
{
    public class TokenProcess
    {
        private static TokenDAL _tokenDAL = null;
        private static TokenDAL tokenDAL
        {
            get { return (_tokenDAL == null) ? _tokenDAL = new TokenDAL() : _tokenDAL; }
        }

        public ResponseToken GenerateToken(RequestToken data)
        {
            ResponseToken res = new ResponseToken();

            // Hash password
            data.password = "";

            if (tokenDAL.AuthenCheck(data))
            {
                //GenerateToken & Insert Record To Db
                res.tokenId = "";
            }

            return res;
        }
    }
}
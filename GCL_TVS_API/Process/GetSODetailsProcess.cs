using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCL_TVS_API.DAL;
using static GCL_TVS_API.Models.SODetailsUrl;

namespace GCL_TVS_API.Process
{
    public class GetSODetailsProcess
    {
        private static SODAL _SODAL = null;
        private static SODAL SODAL
        {
            get { return (_SODAL == null) ? _SODAL = new SODAL() : _SODAL; }
        }

        public ResponseUrl GenerateUrl(RequestUrl data)
        {
            ResponseUrl response = new ResponseUrl();
            

            if (SODAL.AuthenCheckTokenExpire(data.tokenId))
            {
                
            }
            else
            {
                response.responseCode = "99";
                response.soUrl = "";
                response.responseMSG = "tokenId expire or invalid";
            }

            return response;
        }
    }
}
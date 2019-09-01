using GCL_TVS_API.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Process
{
    public class EPODProcess
    {
        private static AuthenDAL _AuthenDal = null;
        private static AuthenDAL AuthenDal
        {
            get { return (_AuthenDal == null) ? _AuthenDal = new AuthenDAL() : _AuthenDal; }
        }
        private static EPODDAL _EPODDAL = null;
        private static EPODDAL EPODDAL
        {
            get { return (_EPODDAL == null) ? _EPODDAL = new EPODDAL() : _EPODDAL; }
        }
        private static Utility _Utility = null;
        private static Utility Utility
        {
            get { return (_Utility == null) ? _Utility = new Utility() : _Utility; }
        }

        public ResponseSODetails GetdataSOFromCustAndSo(RequestSODetailsFromCustAndSo data)
        {
            ResponseSODetails response = new ResponseSODetails();
            try
            {
                if (Utility.IsGuid(data.TokenID) && AuthenDal.ValidateToken(data.TokenID))
                {
                    response.sODetails = SODAL.GetSODetailsFromCustAndSo(data);
                    response.responseCode = "00";
                    response.responseMSG = "Success";
                }
                else
                {
                    response.responseCode = "99";
                    response.responseMSG = "tokenId expire or invalid";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

    }
}
using GCL_TVS_API.DAL;
using System;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.SODetailsService;

namespace GCL_TVS_API.Process
{
    public class EPODProcess
    {
        private static EPODDAL _EPODDal = null;
        private static EPODDAL EPODDal
        {
            get { return (_EPODDal == null) ? _EPODDal = new EPODDAL() : _EPODDal; }
        }

        private static AuthenDAL _AuthenDal = null;
        private static AuthenDAL AuthenDal
        {
            get { return (_AuthenDal == null) ? _AuthenDal = new AuthenDAL() : _AuthenDal; }
        }

        private static Utility _Utility = null;
        private static Utility Utility
        {
            get { return (_Utility == null) ? _Utility = new Utility() : _Utility; }
        }

        public ResponseSODetails GetdataFromJobnoAndSo(RequestJobDetailsFromJobnoAndSo data)
        {
            ResponseSODetails response = new ResponseSODetails();
            try
            {
                if (Utility.IsGuid(data.TokenID) && AuthenDal.ValidateToken(data.TokenID))
                {
                    response.sODetails = EPODDal.GetJobDetailsFromJobnoAndSo(data);
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
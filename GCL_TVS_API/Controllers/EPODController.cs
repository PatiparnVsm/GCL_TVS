using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.Picture;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.Controllers
{
    public class EPODController : ApiController
    {
        private static GetSODetailsProcess _process = null;
        private static GetSODetailsProcess process
        {
            get { return (_process == null) ? _process = new GetSODetailsProcess() : _process; }
        }

        [HttpPost]
        public ResponsePictureSize GetPictureSize([FromBody] RequestPictureSize data)
        {
            ResponsePictureSize res = new ResponsePictureSize();

            //try
            //{
            //    res = process.GetdataSOFromCustAndSo(data);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return res;
        }

        //[HttpPost]
        //public ResponseSODetails GetSoDetailsFromJobnoAndSo([FromBody] RequestSODetailsFromJobnoAndSo data)
        //{
        //    ResponseSODetails res = new ResponseSODetails();

        //    try
        //    {
        //        res = process.GetdataSOFromJobnoAndSo(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return res;
        //}
    }
}

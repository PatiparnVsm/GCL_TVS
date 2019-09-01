using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.Picture;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.Controllers
{
    public class EPODController : ApiController
    {
        private static EPODProcess _process = null;
        private static EPODProcess process
        {
            get { return (_process == null) ? _process = new EPODProcess() : _process; }
        }

        [HttpPost]
        public ResponsePictureSize GetPictureSize([FromBody] RequestPictureSize data)
        {
            ResponsePictureSize res = new ResponsePictureSize();

            try
            {
                res = process.GetPictureSize(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [HttpPost]
        public ResponsePictureSize GetPicturesList([FromBody] RequestPictureSize data)
        {
            ResponsePictureSize res = new ResponsePictureSize();

            try
            {
                res = process.GetPictureSize(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

    }
}

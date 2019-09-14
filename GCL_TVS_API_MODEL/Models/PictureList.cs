using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class PictureList<T> where T : class

    {
        public Guid? TVPictureID { get; set; }
        public int PictureID { get; set; }
        public int PictureSequence { get; set; }
        public string PictureName { get; set; }
        public T PictureImage { get; set; }
        public bool PictureApprovedStatus { get; set; }
        public int ProcessStatusID { get; set; }
        public string ProcessStatusName { get; set; }
        public bool IsRequire { get; set; }






    }
}
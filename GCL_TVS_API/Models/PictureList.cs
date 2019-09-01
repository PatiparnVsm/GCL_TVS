using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class PictureList
    {
        public Guid TVPictureID { get; set; }
        public int PictureID { get; set; }
        public int PictureSequence { get; set; }
        public int PictureName { get; set; }

    }
}
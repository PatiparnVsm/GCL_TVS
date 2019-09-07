using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class ObjT<T> where T : class
    {
        public List<T> List { get; set; }

    }
}
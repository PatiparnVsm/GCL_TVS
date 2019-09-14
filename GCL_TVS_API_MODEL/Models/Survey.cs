using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCL_TVS_API_MODEL.Models
{
    public class Survey
    {
        public Guid TVSurveyID { get; set; }
        public Int32 SurveyID { get; set; }
        public Int16 SurveySequence { get; set; }
        public String SurveyName { get; set; }
        public Int32 SurveyResult { get; set; }
    }
}

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
        public String AnswerType { get; set; }
        public Int16 SurveySequence { get; set; }
        public String SurveyName { get; set; }
        public String SurveyAnswerChoice { get; set; }
        public String SurveyAnswerInput { get; set; }
    }
}

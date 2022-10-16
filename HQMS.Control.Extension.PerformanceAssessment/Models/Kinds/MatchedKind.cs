using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Control.Extension.PerformanceAssessment.Models
{
    public class MatchedKind : CategoryKind
    {
        public string LocalCode { get; set; }
        public string LocalName { get; set; }
        public string StandardCode { get; set; }
        public string StandardName { get; set; }
    }
}

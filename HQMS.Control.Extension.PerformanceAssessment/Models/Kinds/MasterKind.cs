using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npoi.Mapper.Attributes;

namespace HQMS.Control.Extension.PerformanceAssessment.Models
{
    public class MasterKind
    {
        [Column("年月")]
        public string FREPORTDATESTR { get; set; }

        [Column("类别")]
        public string FCODE { get; set; }

        [Column("项目")]
        public string FNAME { get; set; }

        [Column("内容")]
        public string FCONTENT { get; set; }

        [Ignore]
        public int FEXTEND { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.ExportService.Dtos.Input
{
    public class ColumnRule {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string Caption { get; set; }
        public int Width { get; set; }
        public string Align { get; set; }
    }

    public class ExportExcelInputDto
    {
        public string TableName { get; set; }
        public string Heading { get; set; }
        public List<ColumnRule> Columns { get; set; }
        public string KeySearch { get; set; }
    }
}

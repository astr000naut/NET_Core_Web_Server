using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    public class Column
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public int Width { get; set; }
        public string Align { get; set; }
        public string Type { get; set; }
    }
    public class ExportExcelDto
    {
        public string FileName { get; set; }
        public string TableHeading { get; set; }
        public  List<Column> Columns { get; set; }
        public string KeySearch { get; set; }
    }
}

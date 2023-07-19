using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    public class Column
    {
        // Thuộc tính của trường thông tin
        public string Name { get; set; }

        // Tên cột
        public string Caption { get; set; }

        // độ rộng
        public int Width { get; set; }

        // align text
        public string Align { get; set; }

        // kiểu dữ liệu
        public string Type { get; set; }
    }
    public class ExportExcelDto
    {
        // tên file trả về
        public string FileName { get; set; }

        // tiêu đề của bảng
        public string TableHeading { get; set; }

        // danh sách các cột hiển thị
        public  List<Column> Columns { get; set; }

        // chuỗi tìm kiếm
        public string KeySearch { get; set; }
    }
}

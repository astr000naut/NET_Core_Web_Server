using MISA.WebFresher032023.Demo.ExportService.Dtos.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.ExportService.Services
{
    public interface IExportService { 
        Task<byte[]> ExportExcelAsync(ExportExcelInputDto exportExcelInputDto);
    }
}


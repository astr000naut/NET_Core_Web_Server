using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Output
{
    public class Receipt : BaseOutputEntity
    {
      public Guid receiptId { get; set; }
      public Guid? customerId { get; set; }
      public string? customerCode { get; set; }
      public string? customerName { get; set; }
      public string? contactName { get; set; }
      public string? customerAddress { get; set; }
      public Guid? employeeId { get; set; }
      public string? employeeName { get; set; }
      public string? reason { get; set; }
      public int? documentIncluded { get; set; }
      public DateTime postedDate { get; set; }
      public DateTime receiptDate { get; set; }
      public string receiptNo { get; set; }
      public long totalAmount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Output
{
    public class Customer : BaseOutputEntity
    {
        public Guid CustomerId { get; set; }
        public int CustomerType { get; set; }
        public bool IsProvider { get; set; }
        public string CustomerTIN { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerFullName { get; set; }    
        public Guid? EmployeeId { get; set; }   
        public string? EmployeeFullName { get; set; }
        public string Address { get; set; } 
        public string PhoneNumber { get; set; }
        public string Website { get; set; } 
        public string ContactNamePrefix { get; set; }
        public string ContactName { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string LandLineNumber { get; set; }
        public string LegalRepresentative { get; set; }
        public string EnvoiceContactName { get; set; }
        public string EnvoiceContactEmail { get; set; }
        public string EnvoiceContactMobile { get; set; }
        public string PaymentTermName { get; set; }
        public int? DueTime { get; set; }
        public int? MaximizeDebtAmount { get; set; }
        public string ReceiveAccount { get; set; }
        public string PayAccount { get; set; }
        public Guid? PayAccountId { get; set; }
        public Guid? ReceiveAccountId { get; set; }

        public string BankAccountList { get; set; }
        public string Country { get ; set; }
        public string ProvinceOrCity { get; set; }
        public string District { get; set; }
        public string WardOrCommune { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceOrCityId { get; set; }
        public int? DistrictId { get; set; }
        public int? WardOrCommuneId { get; set; }
        public string ShippingAddressList { get; set; }
        public string Description { get; set; } 
        public string IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string IdentityPlace { get; set; }

        public string GroupCodeList { get; set; }

    }
}

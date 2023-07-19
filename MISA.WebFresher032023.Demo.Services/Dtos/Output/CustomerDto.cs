using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class CustomerDto
    {
        // id khách hàng
        public Guid CustomerId { get; set; }

        // loại khách hàng 
        public int CustomerType { get; set; }

        // là nhà cung cấp
        public bool IsProvider { get; set; }

        // mã số thuế
        public string CustomerTIN { get; set; }

        // mã khách hàng
        public string CustomerCode { get; set; }

        // tên khách hàng
        public string CustomerFullName { get; set; }

        // id nhân viên bán hàng
        public Guid? EmployeeId { get; set; }

        // tên nhân viên bán hàng
        public string? EmployeeFullName { get; set; }

        // địa chỉ
        public string Address { get; set; }

        // số điện thoại
        public string PhoneNumber { get; set; }

        // địa chỉ website
        public string Website { get; set; }

        // xưng hô
        public string ContactNamePrefix { get; set; }

        // tên người liên hệ
        public string ContactName { get; set; }

        // số điện thoại người liên hệ
        public string ContactMobile { get; set; }

        // email người liên hệ
        public string ContactEmail { get; set; }

        // số điện thoại cố định
        public string LandLineNumber { get; set; }

        // người đại diện theo pháp luật
        public string LegalRepresentative { get; set; }

        // tên người nhận hóa đơn
        public string EnvoiceContactName { get; set; }

        // email người nhận hóa đơn
        public string EnvoiceContactEmail { get; set; }

        // sđt người nhận hóa đơn
        public string EnvoiceContactMobile { get; set; }

        // điều khoản thanh toán
        public string PaymentTermName { get; set; }

        // số ngày được nợ
        public int? DueTime { get; set; }

        // số nợ tối đa
        public int? MaximizeDebtAmount { get; set; }

        // tài khoản công nợ phải thu
        public string ReceiveAccount { get; set; }

        // tài khoản công nợ phải trả
        public string PayAccount { get; set; }

        // id tài khoản công nợ phải trâ
        public Guid? PayAccountId { get; set; }


        // id tài khoản công nợ phải thu
        public Guid? ReceiveAccountId { get; set; }

        // danh sách tài khoản ngân hàng JSON
        public string BankAccountList { get; set; }

        // quốc gia
        public string Country { get; set; }

        // tỉnh thành phố
        public string ProvinceOrCity { get; set; }

        // quận huyện
        public string District { get; set; }

        // xã phường
        public string WardOrCommune { get; set; }

        // id quốc gia
        public int? CountryId { get; set; }

        // id tỉnh thành phố
        public int? ProvinceOrCityId { get; set; }

        // id quận huyện
        public int? DistrictId { get; set; }

        // id xã phường
        public int? WardOrCommuneId { get; set; }

        // địa chỉ giao hàng JSON
        public string ShippingAddressList { get; set; }

        // ghi chú
        public string Description { get; set; }

        // số CMND
        public string IdentityNumber { get; set; }

        // ngày cấp
        public DateTime? IdentityDate { get; set; }

        //nơi cấp
        public string IdentityPlace { get; set; }
        
        // danh sách nhóm
        public string GroupCodeList { get; set; }

    }
}

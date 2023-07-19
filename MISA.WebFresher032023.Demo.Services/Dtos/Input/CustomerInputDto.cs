using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    public class CustomerInputDto
    {
        // id của khách hàng
        public Guid CustomerId { get; set; }

        // loại khách hàng 0 - tổ chức ; 1 - cá nhân
        public int CustomerType { get; set; }
        
        // là nhà cung cấp
        public bool IsProvider { get; set; }

        // mã số thuế
        [StringLength(100)]
        public string CustomerTIN { get; set; }

        // mã khách hàng
        [StringLength(50)]
        public string CustomerCode { get; set; }

        // tên khách hàng
        [StringLength(100)]
        public string CustomerFullName { get; set; }

        // id nhân viên bán hàng
        public Guid? EmployeeId { get; set; }

        // địa chỉ
        [StringLength(255)]
        public string Address { get; set; }

        // số điện thoại
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        // website
        [StringLength(255)]
        public string Website { get; set; }

        // xưng hô
        [StringLength(50)]
        public string ContactNamePrefix { get; set; }

        // tên người liên hệ
        [StringLength(100)]
        public string ContactName { get; set; }

        // sđt người liên hệ
        [StringLength(50)]
        public string ContactMobile { get; set; }

        // email người liên hệ
        [StringLength(100)]
        public string ContactEmail { get; set; }

        // số đt cố định
        [StringLength(50)]
        public string LandLineNumber { get; set; }

        // người đại diện theo pháp luật
        [StringLength(100)]
        public string LegalRepresentative { get; set; }

        // tên người nhận hóa đơn điện tử
        [StringLength(100)]
        public string EnvoiceContactName { get; set; }

        // email người nhận hóa đơn điện tử
        [StringLength(100)]
        public string EnvoiceContactEmail { get; set; }

        // sđt người nhận hóa đơn điện tử
        [StringLength(100)]
        public string EnvoiceContactMobile { get; set; }

        // tên điều khoản thanh toán
        [StringLength(100)]
        public string PaymentTermName { get; set; }

        // số ngày được nợ
        public int? DueTime { get; set; }

        // số nợ tối đa
        public int? MaximizeDebtAmount { get; set; }

        // tài khoản công nợ phải thu
        [StringLength(100)]
        public string ReceiveAccount { get; set; }

        // tài khoản công nợ phải trả
        [StringLength(100)]
        public string PayAccount { get; set; }

        // id tài khoản công nợ phải thu
        public Guid? PayAccountId { get; set; }

        // id tài khoản công nợ phải trả
        public Guid? ReceiveAccountId { get; set; }

        // danh sách tài khoản ngân hàng JSON
        public string? BankAccountList { get; set; }

        // quốc gia
        [StringLength(50)]
        public string Country { get; set; }

        // tỉnh, thành phố
        [StringLength(50)]
        public string ProvinceOrCity { get; set; }

        // quận huyện
        [StringLength(50)]
        public string District { get; set; }

        // xã phường
        [StringLength(50)]
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
        public string? ShippingAddressList { get; set; }

        //  Ghi chú
        [StringLength(255)]
        public string Description { get; set; }

        // Số CMND
        [StringLength(25)]
        public string IdentityNumber { get; set; }

        // ngày cấp
        public DateTime? IdentityDate { get; set; }

        // Nơi cấp
        [StringLength(255)]
        public string IdentityPlace { get; set; }

        // danh sách mã nhóm
        [StringLength(255)]
        public string GroupCodeList { get; set; }
    }
}

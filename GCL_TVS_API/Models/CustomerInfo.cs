using System;
using System.Collections.Generic;

namespace GCL_TVS_API.Models
{
    public class RequestCustomerInfo
    {
        public string CustomerCode { get; set; }
    }

    public class ResponseCustomerInfo
    {
        public List<CustomerInfo> ResCustomerInfo { get; set; }
    }

    public class CustomerInfo
    {
        public int CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string SubDistrictID { get; set; }
        public string DistrictID { get; set; }
        public string ProvinceID { get; set; }
        public string PostCodeID { get; set; }
        public string TaxID { get; set; }
        public string TaxBranchNo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CompanyTypeCode { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string SubDistrictName { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public string PostCode { get; set; }
    }
}
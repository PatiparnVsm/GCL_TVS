﻿using System;

namespace GCL_TVS_API.Models
{
    public class SODetails
    {
        public Guid JobOrderID { get; set; }
        public string hashValueWH { get; set; }
        public string hashValueDock { get; set; }
        public string jobNo { get; set; }
        public string tmsNo { get; set; }
        public string ci_so { get; set; }
        public string siteCode { get; set; }
        public string warehouseCode { get; set; }
        public string doNo { get; set; }
        public string dockNo { get; set; }
        public string coLoad { get; set; }
        public string coJob { get; set; }
        public string dsNo { get; set; }
        public string loadingType { get; set; }
        public string materialGC { get; set; }
        public string materialDescription { get; set; }
        public string grade { get; set; }
        public string containerSize { get; set; }
        public string qty { get; set; }
        public string uom { get; set; }
        public string size { get; set; }
        public string tmLoadingDatePlan { get; set; }
        public string tmArrivalTimePlan { get; set; }
        public string agentCode { get; set; }
        public string agentName { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string soldToCode { get; set; }
        public string soldToName { get; set; }
        public string poNo { get; set; }
        public string bookingNo { get; set; }
        public string shippingLine { get; set; }
        public string destination { get; set; }
        public string country { get; set; }
        public string etd { get; set; }
        public string eta { get; set; }
        public string transportationMode { get; set; }
        public string transporterCode { get; set; }
        public string transporterName { get; set; }
        public string containerNumber { get; set; }
        public string sealAgent { get; set; }
        public string sealgcl { get; set; }
        public string driverCode { get; set; }
        public string driverName { get; set; }
        public string truckType { get; set; }
        public string vehicleLicenseHeadCode { get; set; }
        public string vehicleLicenseHead { get; set; }
        public string vehicleLicenseTailCode { get; set; }
        public string vehicleLicenseTail { get; set; }
        public string closingTime { get; set; }
        public string deliveryPlanDate { get; set; }
        public string shipToCode { get; set; }
        public string shipToName { get; set; }
        public string shipToAddress { get; set; }
        public string Labour { get; set; }
        public string sendContainerInfoStatus { get; set; }
        public string cyDate { get; set; }
        public string cyPlace { get; set; }
        public string cyPic { get; set; }
        public string mthCYPicCTC { get; set; }
        public string portOfLoadingCode { get; set; }
        public string portOfLoading { get; set; }
        public string portOfDischargeCode { get; set; }
        public string postOfDischarge { get; set; }
        public string returnDateFrom { get; set; }
        public string returnDateTo { get; set; }
        public string returnPlace { get; set; }
        public string returnPic { get; set; }
        public string IddReturnCYPicCTC { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
using DataTransferObject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Customer_Device_Services
{
   public class Customer_Device_ServicesDTO
    {
        public string RefNo { get; set; }

        //Customer 
        [Display(Name ="Customer Name")]
        public string customerName { get; set; }
        [Display(Name ="Address")]
        public string Address { get; set; }
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Display(Name ="Phone Number")]
        public string PhonNumber { get; set; }
        //Device
        [Display(Name ="IMEI 1")]
        public string Imei_1 { get; set; }
        [Display(Name ="IMEI 2")]
        public string Imei_2 { get; set; }
        [Display(Name ="Brand")]
        public string Brand { get; set; }
        [Display(Name ="Model")]
        public string Model { get; set; }
        [Display(Name ="Fault")]
        public string Fault { get; set; }
        [Display(Name ="Submit Date")]
        public string SubmittDate { get; set; }
        [Display(Name ="Repairing Cost")]
        public string RepairingCost { get; set; }
        [Display(Name ="Deliver Date")]
        public string DeliverDate { get; set; }
        [Display(Name = "Customer Signature")]
        public string CustomerSignature { get; set; }
        public List<ServiceDTO> services { get; set; }

    }
}

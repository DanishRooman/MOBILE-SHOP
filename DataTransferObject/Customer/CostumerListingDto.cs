using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Customer
{
    public class CostumerListingDto
    {
        public int id { get; set; }
        [Display(Name="Bill No")]
        public string BillNo { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public string FAULT { get; set; }
        [Display(Name="Device Delivered")]
        public string isDelivered { get; set; }
    }
}

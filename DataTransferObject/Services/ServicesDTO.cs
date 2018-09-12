using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Services
{
   public class ServiceDTO
    {
        public int id { get; set; }
        [Required]
        [Display(Name="Service Name")]
        public string serviceName { get; set; }
        [Required]
        [Display(Name ="Service Charges")]
        public string serviceCharges { get; set; }

    }

}

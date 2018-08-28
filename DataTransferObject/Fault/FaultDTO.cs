using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Fault
{
   public class FaultDTO
    {
        [Display(Name ="id")]
        public int id { get; set; }
        [Required]
        [Display(Name ="Fault Name")]
        public string faultName { get; set; }
    }
}

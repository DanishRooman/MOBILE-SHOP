using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Brand
{
    public class BrandDTO
    {
        [Display(Name ="id")]
        public int id { get; set; }
        [Required]
        [Display(Name ="Brand Name")]
        public string BrandName { get; set; }
    }
}

using DataTransferObject.Brand;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.mbModel
{
   public class ModelDTO
    {
        [Display(Name ="id")]
        public int id { get; set; }
        [Required]
        [Display(Name ="Model Name")]
        public string modelName { get; set; }


        [Display(Name ="Brand Name")]
        public int Brand { get; set; }
        public List<BrandDTO> brandList { get; set; }
    }
}

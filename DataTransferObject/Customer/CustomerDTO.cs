using DataTransferObject.Brand;
using DataTransferObject.Fault;
using DataTransferObject.mbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Customer
{
   public class CustomerDTO
    {
       
        [Display(Name = "id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name="Fault")]
        public string Fault { get; set; }
        [Required]
        [Display(Name ="Model")]
        public string Model { get; set; } 
        //Device
        [Required]
        [Display(Name = "Serial No")]
        public string Serial { get; set; }
        [Required]
        [Display(Name = "IMEI 1")]
        public string imei_1 { get; set; }
        [Display(Name = "IMEI 2")]
        public string imei_2 { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public int Brand { get; set; }
        public List<BrandDTO> brandList { get; set; }
        [Required]
        [Display(Name = "Model")]
        public int model { get; set; }
        public List<ModelDTO> modelList { get; set; }
        [Required]
        [Display(Name = "Fault")]
        public int fault { get; set; }
        public List<FaultDTO> faultList { get; set; }
        [Required]
        [Display(Name = "Date")]
        public string datetime { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}

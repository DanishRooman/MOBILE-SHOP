using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Customer
{
    public class CostumerListingDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public string FAULT { get; set; }
    }
}

using DataAccessLayer.DBContext;
using DataTransferObject.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBILESHOP.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        public object CustomerDTO { get; private set; }

        // GET: Customer
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult AddCustomer()
        {
            CustomerDTO dt = new CustomerDTO();

            return View();
        }
        public ActionResult AddorUpdateCustomer(CustomerDTO dto) {

            try
            {
                using (MOBILESHOPEntities dbcontext =new MOBILESHOPEntities())
                {
                    mbshop_customer_details mbshop = new mbshop_customer_details()
                    {
                        customer_name=dto.Name,
                        customer_address=dto.Address,
                        customer_email=dto.Email,
                        customer_mobile_number=dto.Mobile
                        
                       
                    };
                    dbcontext.mbshop_customer_details.Add(mbshop);
                    dbcontext.SaveChanges();
                    return jaso();

                };
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }





  
}
using DataTransferObject.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBILESHOP.Controllers
{
    public class CustomerController : Controller
    {
        public object CustomerDTO { get; private set; }

        // GET: Customer
        public ActionResult Index()
        {
            CustomerDTO  dto = new CustomerDTO();
            return View();
        }
        public ActionResult AddCustomer()
        {
            CustomerDTO dt = new CustomerDTO();

            return View();
        }
    }

  
}
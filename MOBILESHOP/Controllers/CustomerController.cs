using DataAccessLayer.DBContext;
using DataTransferObject.Brand;
using DataTransferObject.Customer;
using DataTransferObject.mbModel;
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
        // GET: Customer
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            CustomerDTO dt = new CustomerDTO();
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {
                dt.brandList = dbcontext.mb_brand_detail.AsEnumerable().Select(x => new BrandDTO
                {
                    id = x.brand_key,
                    BrandName = x.brand_name
                }).ToList();
                dt.modelList = dbcontext.mb_model_detail.AsEnumerable().Select(x => new ModelDTO
                {
                    id = x.model_key,
                    modelName = x.model_name
                }).ToList();
                dt.faultList = dbcontext.mb_fault_detail.AsEnumerable().Select(x => new DataTransferObject.Fault.FaultDTO
                {
                    id = x.fault_key,
                    faultName = x.fault_name
                }).ToList();
            };

            return View("AddCustomer", dt);
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerDTO dto)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    if (dto.id == 0)
                    {
                        mbshop_customer_details mbshop = new mbshop_customer_details()
                        {

                            customer_name = dto.Name,
                            customer_address = dto.Address,
                            customer_email = dto.Email,
                            customer_mobile_number = dto.Mobile
                        };
                        dbcontext.mbshop_customer_details.Add(mbshop);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Customer added successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var data = dbcontext.mbshop_customer_details.Find(dto.id);
                        if (data != null)
                        {
                            data.customer_name = dto.Name;
                            data.customer_address = dto.Address;
                            data.customer_email = dto.Email;
                            data.customer_mobile_number = dto.Mobile;
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "Customer update successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { key = false, value = "Customer is not found" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                };
            }
            catch (Exception ex)
            {

                return Json(new { key = false, value = "Unable to save the Customer" }, JsonRequestBehavior.AllowGet); ;
            }
        }


        public ActionResult EditCostumer(int id)
        {
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {
                var row = dbcontext.mbshop_customer_details.Find(id);
                CustomerDTO dto = new CustomerDTO()
                {
                    id = row.customer_id,
                    Address = row.customer_address,
                    Email = row.customer_email,
                    Mobile = row.customer_mobile_number,
                    Name = row.customer_name
                };
                return View("AddCustomer", dto);
            };
        }



        [HttpGet]
        public ActionResult CustomerListing()
        {
            try
            {
                List<CustomerDTO> CstmrList = new List<CustomerDTO>();
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    CstmrList = dbcontext.mbshop_customer_details.AsEnumerable().OrderByDescending(x => x.customer_id).Select(x => new CustomerDTO
                    {
                        id = x.customer_id,
                        Name = x.customer_name,
                        Address = x.customer_address,
                        Email = x.customer_email,
                        Mobile = x.customer_mobile_number

                    }).ToList();

                };

                return PartialView("_CustomerListing", CstmrList);

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    var cstmr = dbcontext.mbshop_customer_details.Find(id);
                    if (cstmr != null)
                    {
                        dbcontext.mbshop_customer_details.Remove(cstmr);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Customer deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Customer not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                };

            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to the Customer" }, JsonRequestBehavior.AllowGet);
            }

        }
    }






}
using DataAccessLayer.DBContext;
using DataTransferObject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBILESHOP.Controllers
{
    public class ServiceController : Controller
    {

        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddService()
        {
            ServiceDTO dt = new ServiceDTO();
            return PartialView("AddService", dt);
        }
        [HttpGet]
        public ActionResult ServiceListing()
        {
            try
            {
                List<ServiceDTO> serviceList = new List<ServiceDTO>();
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    serviceList = dbcontext.mbshop_service_detail.AsEnumerable().OrderByDescending(x => x.service_key).Select(x => new ServiceDTO
                    {
                        id = x.service_key,
                        serviceName = x.service_name,
                        serviceCharges = x.service_charges

                    }).ToList();

                };
                return PartialView("_ServiceListing", serviceList);
            }
            catch (Exception)
            {
                return Json(new { key = false, value = "Service is not found" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult AddorUpdateService(ServiceDTO dto)
        {
            try
            {
                if (dto.id != 0)
                {
                    using (MOBILESHOPEntities dbcontext=new MOBILESHOPEntities())
                    {
                        int key = dto.id;
                        var service = dbcontext.mbshop_service_detail.Find(key);
                        if (service !=null)
                        {
                            service.service_name = dto.serviceName;
                            service.service_charges = dto.serviceCharges;
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "Service updated successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { key = false, value = "service not found" }, JsonRequestBehavior.AllowGet);
                        }
                       
                    };
                }
                else
                {
                    using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                    {
                        mbshop_service_detail mbservice = new mbshop_service_detail()
                        {
                            service_name = dto.serviceName,
                            service_charges = dto.serviceCharges
                        };
                        dbcontext.mbshop_service_detail.Add(mbservice);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Service added successfully" }, JsonRequestBehavior.AllowGet);
                    };
                }
              

            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Service is not found" }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpGet]
        public ActionResult DeleteService(int id)
        {

            try
            {
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    var service = dbcontext.mbshop_service_detail.Find(id);
                    if (service != null)
                    {
                        dbcontext.mbshop_service_detail.Remove(service);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "service deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = true, value = "service not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
     [HttpGet]
        public ActionResult EditService(int id)
        {
            try
            {
                ServiceDTO dto = new ServiceDTO();
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    var service = dbcontext.mbshop_service_detail.Find(id);
                    dto.id = service.service_key;
                    dto.serviceName = service.service_name;
                    dto.serviceCharges = service.service_charges;
                };
                return PartialView("AddService", dto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
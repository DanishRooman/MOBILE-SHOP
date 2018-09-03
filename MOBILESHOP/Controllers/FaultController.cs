using DataAccessLayer.DBContext;
using DataTransferObject.Fault;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBILESHOP.Controllers
{
    [Authorize]
    public class FaultController : Controller
    {
        public object MobileDTO { get; private set; }

        // GET: Fault
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddFault()
        {
            FaultDTO dt = new FaultDTO();
            return PartialView("AddFault", dt);
        }
        [HttpGet]
        public ActionResult FaultListing()
        {
            try
            {
                List<FaultDTO> faultList = new List<FaultDTO>();
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    faultList = dbcontext.mb_fault_detail.AsEnumerable().OrderByDescending(x => x.fault_key).Select(x => new FaultDTO
                    {
                        id = x.fault_key,
                        faultName = x.fault_name
                    }).ToList();
                };
                return PartialView("_FaultListing", faultList);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public ActionResult AddOrUpdateFault(FaultDTO dto)
        {
            try
            {
                if (dto.id != 0)
                {
                    using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                    {
                        int key = dto.id;
                        var fault = dbcontext.mb_fault_detail.Find(key);
                        if (fault != null)
                        {
                            fault.fault_name = dto.faultName;
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "Fault updated successfully" }, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            return Json(new { key = true, value = "Fault not found" }, JsonRequestBehavior.AllowGet);
                        }
                    };
                }
                else
                {
                    using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                    {
                        mb_fault_detail mbfault = new mb_fault_detail()
                        {
                            fault_name = dto.faultName
                        };
                        dbcontext.mb_fault_detail.Add(mbfault);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "fault added successfully" }, JsonRequestBehavior.AllowGet);
                    };
                }

            }
            catch (Exception)
            {

                return Json(new { key = false, value = "fault is not found" }, JsonRequestBehavior.AllowGet);
            }

       
        }
        public ActionResult DeleteFault(int id)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    var fault = dbcontext.mb_fault_detail.Find(id);
                    if (fault != null)
                    {
                        dbcontext.mb_fault_detail.Remove(fault);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Fault deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Fault not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }

                };
            }
            catch (Exception)
            {

                throw;
            }
           
           

        }
       
        public ActionResult UpdateFault(int id)
        {
            FaultDTO dt = new FaultDTO();
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {

                var fault = dbcontext.mb_fault_detail.Find(id);
                dt.id = fault.fault_key;
                dt.faultName = fault.fault_name;
             };
            return PartialView("AddFault",dt);

        }
    }
}
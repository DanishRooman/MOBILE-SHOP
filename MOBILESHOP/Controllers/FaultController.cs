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
        // GET: Fault
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddFault()
        {
            FaultDTO dt = new FaultDTO();
            return View("AddFault",dt);
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
        public ActionResult AddOrUpdateFault(FaultDTO dto)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext=new MOBILESHOPEntities())
                {
                    mb_fault_detail mbfault = new mb_fault_detail()
                    {
                        fault_name=dto.faultName
                    };
                    dbcontext.mb_fault_detail.Add(mbfault);
                    dbcontext.SaveChanges();
                    return Json(new { key = true, value = "fault added successfully" }, JsonRequestBehavior.AllowGet);
                };
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "fault is not found" }, JsonRequestBehavior.AllowGet);
            }
          

        }
    }
}
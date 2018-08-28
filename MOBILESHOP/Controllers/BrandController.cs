using DataAccessLayer.DBContext;
using DataTransferObject.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBILESHOP.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
      public ActionResult AddBrand()
        {
            BrandDTO dt = new BrandDTO();

            return View("AddBrand", dt);
        }
        [HttpPost]
        public ActionResult AddOrUpdateBrand(BrandDTO dt)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext =new MOBILESHOPEntities())
                {
                    mb_brand_detail mbbrand = new mb_brand_detail()
                    {
                        brand_name=dt.BrandName

                    };
                    dbcontext.mb_brand_detail.Add(mbbrand);
                    dbcontext.SaveChanges();
                    return Json(new { key = true, value = "brand added successfully" }, JsonRequestBehavior.AllowGet);

                };

            }
            catch (Exception ex)
            {

                return Json(new { key = false, value = "Unable to save the brand" }, JsonRequestBehavior.AllowGet); ;
            }

           
        }
        
    }

    
}
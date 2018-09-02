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
            return PartialView("AddBrand", dt);
        }
        [HttpPost]
        public ActionResult AddOrUpdateBrand(BrandDTO dt)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    mb_brand_detail mbbrand = new mb_brand_detail()
                    {
                        brand_name = dt.BrandName

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
        [HttpGet]
        public ActionResult BrandListing()
        {
            List<BrandDTO> brandlist = new List<BrandDTO>();
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {
                brandlist = dbcontext.mb_brand_detail.AsEnumerable().OrderByDescending(x => x.brand_key).Select(x => new BrandDTO
                {
                    id = x.brand_key,
                    BrandName = x.brand_name
                }).ToList();
            };
            return PartialView("BrandListing", brandlist);
        }
        public ActionResult DeleteBrand(int id)
        {
            try
            {

                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    var brnd = dbcontext.mb_brand_detail.Find(id);
                    if (brnd != null)
                    {
                        dbcontext.mb_brand_detail.Remove(brnd);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "brand deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "brand not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                };
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to the brand" }, JsonRequestBehavior.AllowGet);
            }

        }

    }


}
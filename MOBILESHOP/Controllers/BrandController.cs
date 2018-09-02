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
                if (dt.id != 0)
                {
                    using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                    {
                        int key = dt.id;
                        var brand = dbcontext.mb_brand_detail.Find(key);
                        if (brand != null)
                        {
                            brand.brand_name = dt.BrandName;
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "Brand updated successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { key = true, value = "Brand not found" }, JsonRequestBehavior.AllowGet);
                        }

                    };
                }
                else
                {
                    using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                    {
                        mb_brand_detail mbbrand = new mb_brand_detail()
                        {
                            brand_name = dt.BrandName

                        };
                        dbcontext.mb_brand_detail.Add(mbbrand);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Brand added successfully" }, JsonRequestBehavior.AllowGet);

                    };
                }
            }
            catch (Exception ex)
            {

                return Json(new { key = false, value = "Unable to save the Brand" }, JsonRequestBehavior.AllowGet); ;
            }


        }

        public ActionResult UpdateBrand(int id)
        {
            BrandDTO dt = new BrandDTO();
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {
                var brand = dbcontext.mb_brand_detail.Find(id);
                dt.id = brand.brand_key;
                dt.BrandName = brand.brand_name;
            };
            return PartialView("AddBrand", dt);
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
                        return Json(new { key = true, value = "Brand deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Brand not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                };
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to the Brand" }, JsonRequestBehavior.AllowGet);
            }

        }

    }


}
using DataAccessLayer.DBContext;
using DataTransferObject.Brand;
using DataTransferObject.mbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBILESHOP.Controllers
{
    public class ModelController : Controller
    {
        // GET: Model
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddModel()
        {
            ModelDTO dt = new ModelDTO();
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {
                //Brand
                dt.brandList = dbcontext.mb_brand_detail.AsEnumerable().Select(x => new BrandDTO
                {
                    id = x.brand_key,
                    BrandName = x.brand_name
                }).ToList();

            };
            return View("AddModel", dt);
        }
        [HttpGet]
        public ActionResult ModelListing()
        {
            try
            {
                List<ModelDTO> modelList = new List<ModelDTO>();
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    modelList = dbcontext.mb_model_detail.AsEnumerable().OrderByDescending(x => x.model_key).Select(x => new ModelDTO
                    {
                        id = x.model_key,
                        modelName = x.model_name,
                        Brand=x.model_brand_key
                    }).ToList();
                    return PartialView("ModelListing", modelList);
                };
            }
            catch (Exception)
            {

                throw;
            }

           
        }
        public ActionResult AddOrUpdateModel(ModelDTO dto)
        {
            try
            {
                using (MOBILESHOPEntities dbcontex = new MOBILESHOPEntities())
                {
                    mb_model_detail mbModel = new mb_model_detail()
                    {

                        model_name = dto.modelName,
                        model_brand_key = dto.Brand
                    };
                    dbcontex.mb_model_detail.Add(mbModel);
                    dbcontex.SaveChanges();
                    return Json(new { key = true, value = "model added successfully" }, JsonRequestBehavior.AllowGet);
                };
            }
            catch (Exception)
            {

                return Json(new { key = true, value = "Unable to save the model" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
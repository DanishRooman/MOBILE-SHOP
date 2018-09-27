using DataAccessLayer.DBContext;
using DataTransferObject.Brand;
using DataTransferObject.mbModel;
using System;
using System.Activities.Expressions;
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
            return PartialView("AddModel", dt);
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
                        BrandName = x.mb_brand_detail.brand_name
                    }).ToList();
                    return PartialView("ModelListing", modelList);
                };
            }
            catch (Exception)
            {

                throw;
            }


        }
        [HttpPost]
        public ActionResult AddOrUpdateModel(ModelDTO dto)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                    {
                        if (dto.id == 0)
                        {

                            mb_model_detail mbModel = new mb_model_detail()
                            {

                                model_name = dto.modelName,
                                model_brand_key = dto.Brand
                            };
                            dbcontext.mb_model_detail.Add(mbModel);
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "model added successfully" }, JsonRequestBehavior.AllowGet);

                        }

                        else
                        {
                            var model = dbcontext.mb_model_detail.Find(dto.id);
                            if (model != null)
                            {
                                model.model_name = dto.modelName;
                                model.model_brand_key = dto.Brand;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Model updated successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = true, value = "Model not found" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    };
                }
                else
                {
                    return Json(new { key = false, value = "Please enter correct data" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to save the Model" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UpdateModel(int id)
        {
            ModelDTO dt = new ModelDTO();
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {
                var model = dbcontext.mb_model_detail.Find(id);
                dt.id = model.model_key;
                dt.modelName = model.model_name;
                dt.Brand = model.model_brand_key;
                dt.brandList = dbcontext.mb_brand_detail.AsEnumerable().Select(x => new BrandDTO
                {
                    id = x.brand_key,
                    BrandName = x.brand_name
                }).ToList();
            };
            return PartialView("AddModel", dt);
        }
        public ActionResult DeleteModel(int id)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    var model = dbcontext.mb_model_detail.Find(id);
                    if (model != null)
                    {
                        dbcontext.mb_model_detail.Remove(model);
                        dbcontext.SaveChanges();
                        return Json(new { Key = true, value = "Model deleted successfully" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { key = false, value = "Model not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }

                };

            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to the Model" }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}
using DataAccessLayer.DBContext;
using DataTransferObject.Brand;
using DataTransferObject.Customer;
using DataTransferObject.mbModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                ViewBag.servicesList = dbcontext.mbshop_service_detail.AsEnumerable().Select(x => new DataTransferObject.Services.ServiceDTO
                {
                    id = x.service_key,
                    serviceName = x.service_name,
                    serviceCharges = x.service_charges,
                }).ToList();
            };

            return View("AddCustomer", dt);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(CustomerDTO dto)
        {
            try
            {
                DateTime submitDate = DateTime.ParseExact(dto.datetime, "MM/dd/yyyy h:mm tt", null);
                DateTime deliverDate = DateTime.ParseExact(dto.datetime, "MM/dd/yyyy h:mm tt", null);
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
                        int costmerID = mbshop.customer_id;
                        mbshop_device_detail device = new mbshop_device_detail()
                        {
                            device_costumer = costmerID,
                            device_serial_number = dto.Serial,
                            device_imei_number_1 = dto.imei_1,
                            device_imei_number_2 = dto.imei_2,
                            device_model_key = dto.model,
                            device_fault_key = dto.fault,
                            device_date_submitt = submitDate,
                            device_description = dto.Description,
                            device_deliver_date = deliverDate,
                            device_repairing_cost = dto.repairingCost,


                        };
                        dbcontext.mbshop_device_detail.Add(device);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Customer added successfully", id = device.device_key }, JsonRequestBehavior.AllowGet);
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


        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {

                    String paths = Server.MapPath("~/Uploads"); //Path

                    //Check if directory exist
                    if (!System.IO.Directory.Exists(paths))
                    {
                        Directory.CreateDirectory(paths); //Create directory if it doesn't exist
                    }

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    int devicekey = Convert.ToInt32(Request.Form["deviceKey"]);
                    Boolean isCapture = Convert.ToBoolean(Request.Form["iscapture"]);

                    if (isCapture)
                    {
                        string base64image = Request.Form["CameraImage"].ToString();
                        var t = base64image.Substring(23);
                        var randomFileName = Guid.NewGuid().ToString().Substring(0, 4) + ".png";
                        string imgPath = Path.Combine(paths, randomFileName);
                        string filePath = "/Uploads/" + randomFileName;
                        byte[] bytes = Convert.FromBase64String(t);
                        System.IO.File.WriteAllBytes(imgPath, bytes);
                        using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                        {

                            mb_device_images images = new mb_device_images()
                            {
                                device_id = devicekey,
                                image_path = filePath
                            };
                            dbcontext.mb_device_images.Add(images);
                            dbcontext.SaveChanges();

                        };
                    }

                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = Guid.NewGuid() + file.FileName;
                        }
                        string filePath = "/Uploads/" + fname;
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);

                        using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                        {

                            mb_device_images images = new mb_device_images()
                            {
                                device_id = devicekey,
                                image_path = filePath
                            };
                            dbcontext.mb_device_images.Add(images);
                            dbcontext.SaveChanges();

                        };



                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult EditCostumer(int id)
        {
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {
                var row = dbcontext.mbshop_device_detail.Find(id);
                CustomerDTO dto = new CustomerDTO()
                {
                    id = row.device_key,
                    Address = row.mbshop_customer_details.customer_address,
                    Email = row.mbshop_customer_details.customer_email,
                    Mobile = row.mbshop_customer_details.customer_mobile_number,
                    Name = row.mbshop_customer_details.customer_name,
                    Brand = row.mb_model_detail.mb_brand_detail.brand_key,
                    model = row.device_model_key,
                    datetime = row.device_date_submitt.ToString("MM/dd/yyyy h:mm tt"),
                    Description = row.device_description,
                    fault = row.device_fault_key,
                    Serial = row.device_serial_number,
                    imei_1 = row.device_imei_number_1,
                    imei_2 = row.device_imei_number_2,

                };
                dto.brandList = dbcontext.mb_brand_detail.AsEnumerable().Select(x => new BrandDTO
                {
                    id = x.brand_key,
                    BrandName = x.brand_name
                }).ToList();
                dto.modelList = dbcontext.mb_model_detail.AsEnumerable().Select(x => new ModelDTO
                {
                    id = x.model_key,
                    modelName = x.model_name
                }).ToList();
                dto.faultList = dbcontext.mb_fault_detail.AsEnumerable().Select(x => new DataTransferObject.Fault.FaultDTO
                {
                    id = x.fault_key,
                    faultName = x.fault_name
                }).ToList();

                ViewBag.images = dbcontext.mb_device_images.AsEnumerable().Select(x => new DeviceImagesDTO
                {
                    Id = x.device_id,
                    path = x.image_path
                }).ToList();

                return View("EditCostumer", dto);
            };
        }



        [HttpGet]
        public ActionResult CustomerListing()
        {
            try
            {
                List<CostumerListingDto> CstmrList = new List<CostumerListingDto>();
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    CustomerDTO dt = new CustomerDTO();
                    CstmrList = dbcontext.mbshop_device_detail.AsEnumerable().OrderByDescending(x => x.device_key).Select(x => new CostumerListingDto
                    {
                        id = x.device_key,
                        Name = x.mbshop_customer_details.customer_name,
                        BRAND = x.mb_model_detail.mb_brand_detail.brand_name,
                        MODEL = x.mb_model_detail.model_name,
                        FAULT = x.mb_fault_detail.fault_name

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
                    var cstmr = dbcontext.mbshop_device_detail.Find(id);
                    if (cstmr != null)
                    {
                        dbcontext.mbshop_device_detail.Remove(cstmr);
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
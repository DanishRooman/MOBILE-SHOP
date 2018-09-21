using DataAccessLayer.DBContext;
using DataTransferObject.Brand;
using DataTransferObject.Customer;
using DataTransferObject.Customer_Device_Services;
using DataTransferObject.mbModel;
using DataTransferObject.Services;
using Rotativa.Core.Options;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace MOBILESHOP.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        public object dto { get; private set; }

        // GET: Customer
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult PrintBill(int id)
        {
            Customer_Device_ServicesDTO dto = new Customer_Device_ServicesDTO();
            using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
            {

                var cstmr = dbcontext.mbshop_device_detail.Find(id);
                if (cstmr != null)
                {
                    dto.RefNo = cstmr.device_key.ToString();
                    dto.customerName = cstmr.mbshop_customer_details.customer_name;
                    dto.Address = cstmr.mbshop_customer_details.customer_address;
                    dto.Email = cstmr.mbshop_customer_details.customer_email;
                    dto.PhonNumber = cstmr.mbshop_customer_details.customer_mobile_number;
                    dto.Imei_1 = cstmr.device_imei_number_1;
                    dto.Imei_2 = cstmr.device_imei_number_2;
                    dto.Brand = cstmr.mb_model_detail.mb_brand_detail.brand_name;
                    dto.Model = cstmr.mb_model_detail.model_name;
                    dto.Fault = cstmr.mb_fault_detail.fault_name;
                    dto.SubmittDate = cstmr.device_date_submitt.ToString("MM/dd/yyyy h:mm tt");
                    dto.RepairingCost = cstmr.device_repairing_cost;
                    dto.DeliverDate = Convert.ToDateTime(cstmr.device_deliver_date).ToString("MM/dd/yyyy h:mm tt");
                    dto.CustomerSignature = cstmr.device_customer_signature != null ? cstmr.device_customer_signature : "~/images/300px-No_image_available.svg (1).png";

                }
                int i = 1;
                dto.services = dbcontext.Costumer_Device_Services.Where(x => x.cds_device_key == id).AsEnumerable().Select(x => new ServiceDTO
                {
                    id = (i++),
                    serviceName = x.mbshop_service_detail.service_name,
                    serviceCharges = x.mbshop_service_detail.service_charges
                }).ToList();
            };


            return View("CustomerBill", dto);
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
                DateTime receiveDate = DateTime.ParseExact(dto.receivedDate, "MM/dd/yyyy h:mm tt", null);
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
                            device_customer_signature = dto.customerSignature,


                        };
                        dbcontext.mbshop_device_detail.Add(device);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Device Details added successfully", id = device.device_key }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var device = dbcontext.mbshop_device_detail.Find(dto.id);
                        if(device != null)
                        {
                            device.device_customer_signature = dto.customerSignature;
                            device.device_date_submitt = submitDate;
                            device.device_deliver_date = deliverDate;
                            device.device_description = dto.Description;
                            device.device_serial_number = dto.Serial;
                            device.device_imei_number_1 = dto.imei_1;
                            device.device_imei_number_2 = dto.imei_2;
                            device.device_model_key = dto.model;
                            device.device_fault_key = dto.fault;
                            device.device_repairing_cost = dto.repairingCost;
                            device.device_customer_signature = dto.customerSignature;
                            device.device_receiver_signature = dto.receivedSignature;
                            device.device_receiver_name = dto.receivedName;
                            device.device_receiving_date = receiveDate;
                            device.device_is_delivered = dto.deviceDelivered == 1 ? true : false;
                        }
                        dbcontext.SaveChanges();
                        int customerID = device.device_costumer;
                        var customer = dbcontext.mbshop_customer_details.Find(customerID);
                        if (customer != null)
                        {
                            customer.customer_name = dto.Name;
                            customer.customer_address = dto.Address;
                            customer.customer_email = dto.Email;
                            customer.customer_mobile_number = dto.Mobile;
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "Device Details updated successfully" , id = device.device_key }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { key = false, value = "Device Details is not found" }, JsonRequestBehavior.AllowGet);
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
            try
            {
                int devicekey = Convert.ToInt32(Request.Form["deviceKey"]);
                String paths = Server.MapPath("~/Uploads"); //Path

                //Check if directory exist
                if (!System.IO.Directory.Exists(paths))
                {
                    Directory.CreateDirectory(paths); //Create directory if it doesn't exist
                }


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

                if (Request.Files.Count > 0)
                {


                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;


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
                else
                {
                    return Json("No files selected.");
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
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
                    customerSignature = row.device_customer_signature != null ? row.device_customer_signature : "/images/300px-No_image_available.svg (1).png",
                    deliverDate = Convert.ToDateTime(row.device_deliver_date).ToString("MM/dd/yyyy h:mm tt"),
                    repairingCost = row.device_repairing_cost,
                    receivedSignature = row.device_receiver_signature != null ? row.device_receiver_signature : "/images/300px-No_image_available.svg (1).png",
                    receivedDate = Convert.ToDateTime(row.device_receiving_date).ToString("MM/dd/yyyy h:mm tt"),
                    receivedName = row.device_receiver_name,
                    deviceDelivered = row.device_is_delivered == true ? 1 : 0
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

                ViewBag.images = dbcontext.mb_device_images.Where(d=>d.device_id==id).AsEnumerable().Select(d => new DeviceImagesDTO
                {
                    Id = d.image_key,
                    path = d.image_path
                }).ToList();
               


                ViewBag.servicesList = dbcontext.mbshop_service_detail.AsEnumerable().Select(x => new DataTransferObject.Services.ServiceDTO
                {
                    id = x.service_key,
                    serviceName = x.service_name,
                    serviceCharges = x.service_charges,
                }).ToList();

                ViewBag.selected_servicesList = dbcontext.Costumer_Device_Services.AsEnumerable().Select(x => new DataTransferObject.Services.ServiceDTO
                {
                    id = x.mbshop_service_detail.service_key,
                    serviceName = x.mbshop_service_detail.service_name,
                    serviceCharges = x.mbshop_service_detail.service_charges,
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
                        BillNo = x.device_key.ToString(),
                        Name = x.mbshop_customer_details.customer_name,
                        BRAND = x.mb_model_detail.mb_brand_detail.brand_name,
                        MODEL = x.mb_model_detail.model_name,
                        FAULT = x.mb_fault_detail.fault_name,
                       Mobile=x.mbshop_customer_details.customer_mobile_number

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
                        var customer = dbcontext.mbshop_customer_details.Find(cstmr.mbshop_customer_details.customer_id);
                        dbcontext.mbshop_device_detail.Remove(cstmr);
                        dbcontext.SaveChanges();
                        if (customer != null)
                        {
                            dbcontext.mbshop_customer_details.Remove(customer);
                            dbcontext.SaveChanges();
                        }
                        return Json(new { key = true, value = "Device Details deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Device Details not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                };

            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to the Customer" }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult SaveServices(int deviceId, string[] servicesId)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    var list = dbcontext.Costumer_Device_Services.Where(x => x.cds_device_key == deviceId).ToList();
                    if (list.Any())
                    {
                        foreach (var item in list)
                        {
                            dbcontext.Costumer_Device_Services.Remove(item);
                            dbcontext.SaveChanges();
                        }
                    }
                    foreach (var item in servicesId)
                    {
                        int serviceKey = Convert.ToInt32(item);
                        Costumer_Device_Services service = new Costumer_Device_Services()
                        {
                            cds_service_key = serviceKey,
                            cds_device_key = deviceId,
                        };
                        dbcontext.Costumer_Device_Services.Add(service);
                        dbcontext.SaveChanges();
                    }
                    return Json(new { key = true, value = "Services added successfully" }, JsonRequestBehavior.AllowGet);
                };
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request please contact to your admin." }, JsonRequestBehavior.AllowGet);
            }


        }



        public ActionResult DeleteImage(int imageKey)
        {
            try
            {
                using (MOBILESHOPEntities dbcontext = new MOBILESHOPEntities())
                {
                    var item = dbcontext.mb_device_images.Find(imageKey);
                    if (item != null)
                    {
                        var filePath = "~" + item.image_path;
                        if (System.IO.File.Exists(Server.MapPath(filePath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath));
                        }

                        dbcontext.mb_device_images.Remove(item);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Image deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Image not found" }, JsonRequestBehavior.AllowGet);
                    }
                };
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request please contact to your admin." }, JsonRequestBehavior.AllowGet);
            }

        }


    }






}
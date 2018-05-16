using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GCCSI_CO2RE.DAL;
using GCCSI_CO2RE.Models;

namespace GCCSI_CO2RE.Controllers
{
    public class ReferenceDataController : Controller
    {

        private GCCSIContext db = new GCCSIContext();


        // GET: ReferenceData
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.PageNum = 0;
            return View();
        }

        [ActionName("IndexOption")]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? id)
        {
            ViewBag.PageNum = id;
            return View("Index");
        }


        //REGION getter for index. 1
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetRegionPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_Region/RegionIndex.cshtml", db.RD_RegionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRegion(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Region region = db.RD_RegionSet.Where(m => m.RegionID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Region/_RegionEdit.cshtml", region);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_Region/_RegionEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Region region = db.RD_RegionSet.Where(m => m.RegionID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Region/RegionEdit.cshtml", region);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_Region/RegionEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRegion(RD_Region regionDataObject, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        regionDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_RegionSet.Add(regionDataObject);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 1 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_Region newRegionDataObject = db.RD_RegionSet.Where(m => m.RegionID == regionDataObject.RegionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newRegionDataObject != null)

                        {

                            newRegionDataObject.RegionName = regionDataObject.RegionName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 1 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_Region/_RegionEdit.cshtml", regionDataObject);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_Region/RegionEdit.cshtml", regionDataObject);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRegion(int id)

        {

            RD_Region region = db.RD_RegionSet.Where(m => m.RegionID == id).FirstOrDefault();

            if (region != null)

            {

                try

                {

                    db.RD_RegionSet.Remove(region);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 1 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RegionDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Region region = db.RD_RegionSet.Where(m => m.RegionID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Region/_RegionDetails.cshtml", region);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_Region/_RegionDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Region region = db.RD_RegionSet.Where(m => m.RegionID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Region/RegionDetails.cshtml", region);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_Region/RegionDetails.cshtml");

            }

        }



        //COUNTRY getter for index. 2
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetLocationPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_Country/CountryIndex.cshtml", db.RD_CountrySet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditCountry(int? id)

        {

            //Generic List Setter -- used for relational objects needed for selection.
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Country country = db.RD_CountrySet.Where(m => m.CountryID == id).FirstOrDefault();
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", country.RegionID);

                    return PartialView("~/Views/ReferenceData/RD_Country/_CountryEdit.cshtml", country);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_Country/_CountryEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Country country = db.RD_CountrySet.Where(m => m.CountryID == id).FirstOrDefault();
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", country.RegionID);

                    return PartialView("~/Views/ReferenceData/RD_Country/CountryEdit.cshtml", country);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_Country/CountryEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditCountry(RD_Country countryDataObject, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        countryDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_CountrySet.Add(countryDataObject);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 2 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_Country newCountryObject = db.RD_CountrySet.Where(m => m.CountryID == countryDataObject.RegionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCountryObject != null)

                        {

                            newCountryObject.CountryName = countryDataObject.CountryName;

                            newCountryObject.RegionID = countryDataObject.RegionID;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 2 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_Country/_CountryEdit.cshtml", countryDataObject);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_Country/CountryEdit.cshtml", countryDataObject);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCountry(int id)

        {

            RD_Country country = db.RD_CountrySet.Where(m => m.CountryID == id).FirstOrDefault();

            if (country != null)

            {

                try

                {

                    db.RD_CountrySet.Remove(country);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 2 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CountryDetails(int? id)

        {

            //Generic List Setter -- used for relational objects needed for selection.
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Country country = db.RD_CountrySet.Where(m => m.CountryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Country/_CountryDetails.cshtml", country);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_Country/_CountryDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Country country = db.RD_CountrySet.Where(m => m.CountryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Country/CountryDetails.cshtml", country);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_Country/CountryDetails.cshtml");

            }

        }


        //Facility Industry getter for index. 3
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetFacilityIndustryPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_FacilityIndustry/FacilityIndustryIndex.cshtml", db.RD_FacilityIndustrySet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityIndustry(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityIndustry facilityIndustry = db.RD_FacilityIndustrySet.Where(m => m.FacilityIndustryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityIndustry/_FacilityIndustryEdit.cshtml", facilityIndustry);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityIndustry/_FacilityIndustryEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityIndustry facilityIndustry = db.RD_FacilityIndustrySet.Where(m => m.FacilityIndustryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityIndustry/FacilityIndustryEdit.cshtml", facilityIndustry);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityIndustry/FacilityIndustryEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityIndustry(RD_FacilityIndustry facilityIndustryDataObj, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        facilityIndustryDataObj.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_FacilityIndustrySet.Add(facilityIndustryDataObj);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 3 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_FacilityIndustry newFacilityIndustry = db.RD_FacilityIndustrySet.Where(m => m.FacilityIndustryID == facilityIndustryDataObj.FacilityIndustryID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newFacilityIndustry != null)

                        {

                            newFacilityIndustry.FacilityIndustryName = facilityIndustryDataObj.FacilityIndustryName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 3 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_FacilityIndustry/_FacilityIndustryEdit.cshtml", facilityIndustryDataObj);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_FacilityIndustry/FacilityIndustryEdit.cshtml", facilityIndustryDataObj);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFacilityIndustry(int id)

        {

            RD_FacilityIndustry facilityIndustryObject = db.RD_FacilityIndustrySet.Where(m => m.FacilityIndustryID == id).FirstOrDefault();

            if (facilityIndustryObject != null)

            {

                try

                {

                    db.RD_FacilityIndustrySet.Remove(facilityIndustryObject);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 3 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult FacilityIndustryDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityIndustry facilityIndustryObj = db.RD_FacilityIndustrySet.Where(m => m.FacilityIndustryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityIndustry/_FacilityIndustryDetails.cshtml", facilityIndustryObj);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityIndustry/_FacilityIndustryDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityIndustry facilityIndustryObj = db.RD_FacilityIndustrySet.Where(m => m.FacilityIndustryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityIndustry/FacilityIndustryDetails.cshtml", facilityIndustryObj);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityIndustry/FacilityIndustryDetails.cshtml");

            }

        }


        //Facility Capture Type section 4
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetCaptureTypePartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_FacilityCaptureType/CaptureTypeIndex.cshtml", db.RD_FacilityCaptureTypeSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditCaptureType(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityCaptureType facilityCapture = db.RD_FacilityCaptureTypeSet.Where(m => m.FacilityCaptureTypeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityCaptureType/_CaptureTypeEdit.cshtml", facilityCapture);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityCaptureType/_CaptureTypeEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityCaptureType facilityCapture = db.RD_FacilityCaptureTypeSet.Where(m => m.FacilityCaptureTypeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityCaptureType/CaptureTypeEdit.cshtml", facilityCapture);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityCaptureType/CaptureTypeEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditCaptureType(RD_FacilityCaptureType facilityCaptureType, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        facilityCaptureType.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_FacilityCaptureTypeSet.Add(facilityCaptureType);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 4 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_FacilityCaptureType newCaptureType = db.RD_FacilityCaptureTypeSet.Where(m => m.FacilityCaptureTypeID == facilityCaptureType.FacilityCaptureTypeID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCaptureType != null)

                        {

                            newCaptureType.FacilityCaptureTypeName = facilityCaptureType.FacilityCaptureTypeName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 4 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_FacilityCaptureType/_CaptureTypeEdit.cshtml", facilityCaptureType);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_FacilityCaptureType/CaptureTypeEdit.cshtml", facilityCaptureType);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCaptureType(int id)

        {

            RD_FacilityCaptureType facilityCaptureObject = db.RD_FacilityCaptureTypeSet.Where(m => m.FacilityCaptureTypeID == id).FirstOrDefault();

            if (facilityCaptureObject != null)

            {

                try

                {

                    db.RD_FacilityCaptureTypeSet.Remove(facilityCaptureObject);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 4 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CaptureTypeDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityCaptureType captureTypeObject = db.RD_FacilityCaptureTypeSet.Where(m => m.FacilityCaptureTypeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityCaptureType/_CaptureTypeDetails.cshtml", captureTypeObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityCaptureType/_CaptureTypeDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityCaptureType captureTypeObject = db.RD_FacilityCaptureTypeSet.Where(m => m.FacilityCaptureTypeID == id).FirstOrDefault();

                    return PartialView("CaptureTypeDetails", captureTypeObject);

                }

                ViewBag.IsUpdate = false;

                return View("CaptureTypeDetails");

            }

        }


        //Facility Status section 5
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetFacilityStatusPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_FacilityStatus/FacilityStatusIndex.cshtml", db.RD_FacilityStatusSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityStatus(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityStatus facilityStatus = db.RD_FacilityStatusSet.Where(m => m.FacilityStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityStatus/_FacilityStatusEdit.cshtml", facilityStatus);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityStatus/_FacilityStatusEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityStatus facilityStatus = db.RD_FacilityStatusSet.Where(m => m.FacilityStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityStatus/_FacilityStatusEdit.cshtml", facilityStatus);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityStatus/FacilityStatusEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityStatus(RD_FacilityStatus facilityStatus, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        facilityStatus.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_FacilityStatusSet.Add(facilityStatus);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 5 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_FacilityStatus newStatus = db.RD_FacilityStatusSet.Where(m => m.FacilityStatusID == facilityStatus.FacilityStatusID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newStatus != null)

                        {

                            newStatus.FacilityStatusName = facilityStatus.FacilityStatusName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 5 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_FacilityStatus/_FacilityStatusEdit.cshtml", facilityStatus);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_FacilityStatus/FacilityStatusEdit.cshtml", facilityStatus);

            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFacilityStatus(int id)

        {

            RD_FacilityStatus facilityStatusObject = db.RD_FacilityStatusSet.Where(m => m.FacilityStatusID == id).FirstOrDefault();

            if (facilityStatusObject != null)

            {

                try

                {

                    db.RD_FacilityStatusSet.Remove(facilityStatusObject);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 5 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult FacilityStatusDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityStatus facilityStatus = db.RD_FacilityStatusSet.Where(m => m.FacilityStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityStatus/_FacilityStatusDetails.cshtml", facilityStatus);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityStatus/_FacilityStatusDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityStatus facilityStatus = db.RD_FacilityStatusSet.Where(m => m.FacilityStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityStatus/FacilityStatusEdit.cshtml", facilityStatus);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityStatus/FacilityStatusEdit.cshtml");

            }

        }


        //Facility Category section 6
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetFacilityCategoryPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_FacilityCategory/FacilityCategoryIndex.cshtml", db.RD_FacilityCategorySet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityCategory(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityCategory facilityCategory = db.RD_FacilityCategorySet.Where(m => m.FacilityCategoryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityCategory/_FacilityCategoryEdit.cshtml", facilityCategory);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityCategory/_FacilityCategoryEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityCategory facilityCategory = db.RD_FacilityCategorySet.Where(m => m.FacilityCategoryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityCategory/FacilityCategoryEdit.cshtml", facilityCategory);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityCategory/FacilityCategoryEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityCategory(RD_FacilityCategory facilityCategory, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        facilityCategory.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_FacilityCategorySet.Add(facilityCategory);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 6 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_FacilityCategory newCategory = db.RD_FacilityCategorySet.Where(m => m.FacilityCategoryID == facilityCategory.FacilityCategoryID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCategory != null)

                        {

                            newCategory.FacilityCategoryName = facilityCategory.FacilityCategoryName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 6 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_FacilityCategory/_FacilityCategoryEdit.cshtml", facilityCategory);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_FacilityCategory/FacilityCategoryEdit.cshtml", facilityCategory);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFacilityCategory(int id)

        {

            RD_FacilityCategory facilityCategory = db.RD_FacilityCategorySet.Where(m => m.FacilityCategoryID == id).FirstOrDefault();

            if (facilityCategory != null)

            {

                try

                {

                    db.RD_FacilityCategorySet.Remove(facilityCategory);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 6 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult FacilityCategoryDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityCategory facilityCategory = db.RD_FacilityCategorySet.Where(m => m.FacilityCategoryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityCategory/_FacilityCategoryDetails.cshtml", facilityCategory);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityCategory/_FacilityCategoryDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityCategory facilityCategory = db.RD_FacilityCategorySet.Where(m => m.FacilityCategoryID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityCategory/FacilityCategoryDetails.cshtml", facilityCategory);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityCategory/FacilityCategoryDetails.cshtml");

            }

        }


        //Facility Storage Code section 7
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetFacilityStorageCodePartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_FacilityStorageCode/FacilityStorageCodeIndex.cshtml", db.RD_FacilityStorageCodeSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityStorageCode(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityStorageCode storageCode = db.RD_FacilityStorageCodeSet.Where(m => m.FacilityStorageCodeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityStorageCode/_FacilityStorageCodeEdit.cshtml", storageCode);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityStorageCode/_FacilityStorageCodeEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_FacilityStorageCode storageCode = db.RD_FacilityStorageCodeSet.Where(m => m.FacilityStorageCodeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityStorageCode/FacilityStorageCodeEdit.cshtml", storageCode);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityStorageCode/FacilityStorageCodeEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityStorageCode(RD_FacilityStorageCode storageCode, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        storageCode.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_FacilityStorageCodeSet.Add(storageCode);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 7 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_FacilityStorageCode newStorageCode = db.RD_FacilityStorageCodeSet.Where(m => m.FacilityStorageCodeID == storageCode.FacilityStorageCodeID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newStorageCode != null)

                        {

                            newStorageCode.FacilityStorageCodeName = storageCode.FacilityStorageCodeName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 7 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_FacilityStorageCode/_FacilityStorageCodeEdit.cshtml", storageCode);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_FacilityStorageCode/FacilityStorageCodeEdit.cshtml", storageCode);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFacilityStorageCode(int id)

        {

            RD_FacilityStorageCode storageCode = db.RD_FacilityStorageCodeSet.Where(m => m.FacilityStorageCodeID == id).FirstOrDefault();

            if (storageCode != null)

            {

                try

                {

                    db.RD_FacilityStorageCodeSet.Remove(storageCode);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 7 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult FacilityStorageCodeDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityStorageCode storageCode = db.RD_FacilityStorageCodeSet.Where(m => m.FacilityStorageCodeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityStorageCode/_FacilityStorageCodeDetails.cshtml", storageCode);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityStorageCode/_FacilityStorageCodeDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityStorageCode storageCode = db.RD_FacilityStorageCodeSet.Where(m => m.FacilityStorageCodeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityStorageCode/FacilityStorageCodeDetails.cshtml", storageCode);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityStorageCode/FacilityStorageCodeDetails.cshtml");

            }

        }


        //Facility Transport section 8
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetFacilityTransportCodePartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/FacilityTransportCodeIndex.cshtml", db.RD_FacilityTransportCodeSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityTransportCode(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityTransportCode transportCode = db.RD_FacilityTransportCodeSet.Where(m => m.FacilityTransportCodeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/_FacilityTransportCodeEdit.cshtml", transportCode);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/_FacilityTransportCodeEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_FacilityTransportCode transportCode = db.RD_FacilityTransportCodeSet.Where(m => m.FacilityTransportCodeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/FacilityTransportCodeEdit.cshtml", transportCode);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/FacilityTransportCodeEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditFacilityTransportCode(RD_FacilityTransportCode transportCode, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        transportCode.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_FacilityTransportCodeSet.Add(transportCode);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 8 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_FacilityTransportCode newTransportCode = db.RD_FacilityTransportCodeSet.Where(m => m.FacilityTransportCodeID == transportCode.FacilityTransportCodeID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newTransportCode != null)

                        {

                            newTransportCode.FacilityTransportCodeName = transportCode.FacilityTransportCodeName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 8 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/_FacilityTransportCodeEdit.cshtml", transportCode);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_FacilityTransportCode/FacilityTransportCodeEdit.cshtml", transportCode);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFacilityTransportCode(int id)

        {

            RD_FacilityTransportCode transportCode = db.RD_FacilityTransportCodeSet.Where(m => m.FacilityTransportCodeID == id).FirstOrDefault();

            if (transportCode != null)

            {

                try

                {

                    db.RD_FacilityTransportCodeSet.Remove(transportCode);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 8 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult FacilityTransportCodeDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityTransportCode transportCode = db.RD_FacilityTransportCodeSet.Where(m => m.FacilityTransportCodeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/_FacilityTransportCodeDetails.cshtml", transportCode);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/_FacilityTransportCodeDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_FacilityTransportCode transportCode = db.RD_FacilityTransportCodeSet.Where(m => m.FacilityTransportCodeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_FacilityTransportCode/FacilityTransportCodeDetails.cshtml", transportCode);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_FacilityTransportCode/FacilityTransportCodeDetails.cshtml");

            }

        }


        //LRBand section 9
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetLRBandPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_LRBand/LRBandIndex.cshtml", db.LRBandSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditLRBand(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_LRBand lrBand = db.LRBandSet.Where(m => m.LRBandID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_LRBand/_LRBandEdit.cshtml", lrBand);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_LRBand/_LRBandEdit.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_LRBand lrBand = db.LRBandSet.Where(m => m.LRBandID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_LRBand/LRBandEdit.cshtml", lrBand);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_LRBand/LRBandEdit.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditLRBand(RD_LRBand lrBand, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        lrBand.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.LRBandSet.Add(lrBand);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 9 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_LRBand newLrBand = db.LRBandSet.Where(m => m.LRBandID == lrBand.LRBandID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newLrBand != null)

                        {

                            newLrBand.BandName = lrBand.BandName;
                            newLrBand.BandValue = lrBand.BandValue;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 9 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_LRBand/_LRBandEdit.cshtml", lrBand);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_LRBand/LRBandEdit.cshtml", lrBand);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteLRBand(int id)

        {

            RD_LRBand lrBand = db.LRBandSet.Where(m => m.LRBandID == id).FirstOrDefault();

            if (lrBand != null)

            {

                try

                {

                    db.LRBandSet.Remove(lrBand);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 9 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult LRBandDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_LRBand lrBand = db.LRBandSet.Where(m => m.LRBandID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_LRBand/_LRBandDetails.cshtml", lrBand);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_LRBand/_LRBandDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_LRBand lrBand = db.LRBandSet.Where(m => m.LRBandID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_LRBand/LRBandDetails.cshtml", lrBand);

                }

                ViewBag.IsUpdate = false;

                return View("~/Views/ReferenceData/RD_LRBand/LRBandDetails.cshtml");

            }

        }



        //Policy List Option section 10
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPolicyListOptionPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_PolicyListOption/PolicyListOptionIndex.cshtml", db.RD_PolicyListOptionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyListOption(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyListOption polListOption = db.RD_PolicyListOptionSet.Where(m => m.PolicyListOptionID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyListOption/_EditPolicyListOption.cshtml", polListOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyListOption/_EditPolicyListOption.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_PolicyListOption polListOption = db.RD_PolicyListOptionSet.Where(m => m.PolicyListOptionID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyListOption/EditPolicyListOption.cshtml", polListOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyListOption/EditPolicyListOption.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyListOption(RD_PolicyListOption polListOption, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        polListOption.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_PolicyListOptionSet.Add(polListOption);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 10 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_PolicyListOption newPolicyListOption = db.RD_PolicyListOptionSet.Where(m => m.PolicyListOptionID == polListOption.PolicyListOptionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newPolicyListOption != null)

                        {

                            newPolicyListOption.PolicyListOptionName = polListOption.PolicyListOptionName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 10 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_PolicyListOption/_EditPolicyListOption.cshtml", polListOption);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_PolicyListOption/EditPolicyListOption.cshtml", polListOption);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePolicyListOption(int id)

        {

            RD_PolicyListOption polListOption = db.RD_PolicyListOptionSet.Where(m => m.PolicyListOptionID == id).FirstOrDefault();

            if (polListOption != null)

            {

                try

                {

                    db.RD_PolicyListOptionSet.Remove(polListOption);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 10 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyListOptionDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyListOption polListOption = db.RD_PolicyListOptionSet.Where(m => m.PolicyListOptionID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyListOption/_PolicyListOptionDetails.cshtml", polListOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyListOption/_PolicyListOptionDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyListOption polListOption = db.RD_PolicyListOptionSet.Where(m => m.PolicyListOptionID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyListOption/PolicyListOptionDetails.cshtml", polListOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyListOption/PolicyListOptionDetails.cshtml");

            }

        }




        //Policy List Status section 11
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPolicyListStatusPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/PolicyListStatusIndex.cshtml", db.RD_PolicyListStatusSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyListStatus(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyListStatus polListStatus = db.RD_PolicyListStatusSet.Where(m => m.PolicyListStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/_EditPolicyListStatus.cshtml", polListStatus);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/_EditPolicyListStatus.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_PolicyListStatus polListStatus = db.RD_PolicyListStatusSet.Where(m => m.PolicyListStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/EditPolicyListStatus.cshtml", polListStatus);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/EditPolicyListStatus.cshtml");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyListStatus(RD_PolicyListStatus polListStatus, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        polListStatus.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_PolicyListStatusSet.Add(polListStatus);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 11 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_PolicyListStatus newPolicyListStatus = db.RD_PolicyListStatusSet.Where(m => m.PolicyListStatusID == polListStatus.PolicyListStatusID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newPolicyListStatus != null)

                        {

                            newPolicyListStatus.PolicyListStatusName = polListStatus.PolicyListStatusName;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 11 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/_EditPolicyListStatus.cshtml", polListStatus);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_PolicyListStatus/EditPolicyListStatus.cshtml", polListStatus);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePolicyListStatus(int id)

        {

            RD_PolicyListStatus policyListStatus = db.RD_PolicyListStatusSet.Where(m => m.PolicyListStatusID == id).FirstOrDefault();

            if (policyListStatus != null)

            {

                try

                {

                    db.RD_PolicyListStatusSet.Remove(policyListStatus);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 11 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyListStatusDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyListStatus polStatusOption = db.RD_PolicyListStatusSet.Where(m => m.PolicyListStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/_PolicyListStatusDetails.cshtml", polStatusOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/_PolicyListStatusDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_PolicyListStatus polStatusOption = db.RD_PolicyListStatusSet.Where(m => m.PolicyListStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/PolicyListStatusDetails.cshtml", polStatusOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/PolicyListStatusDetails.cshtml");

            }

        }


        //Policy Option section 12
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPolicyOptionPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_PolicyOption/PolicyOptionIndex.cshtml", db.RD_PolicyOptionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyOption(int? id)

        {

            //Generic List Setter -- used for relational objects needed for selection.
            ViewBag.PolicyTypeID = new SelectList(db.RD_PolicyTypeSet, "PolicyTypeID", "PolicyTypeName");

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyOption policyOption = db.RD_PolicyOptionSet.Where(m => m.PolicyOptionID == id).FirstOrDefault();
                    ViewBag.PolicyTypeID = new SelectList(db.RD_PolicyTypeSet, "PolicyTypeID", "PolicyTypeName", policyOption.PolicyTypeID);

                    return PartialView("~/Views/ReferenceData/RD_PolicyOption/_EditPolicyOption.cshtml", policyOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyOption/_EditPolicyOption.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyOption policyOption = db.RD_PolicyOptionSet.Where(m => m.PolicyOptionID == id).FirstOrDefault();
                    ViewBag.PolicyTypeID = new SelectList(db.RD_PolicyTypeSet, "PolicyTypeID", "PolicyTypeName", policyOption.PolicyTypeID);

                    return PartialView("~/Views/ReferenceData/RD_PolicyOption/EditPolicyOption.cshtml", policyOption);
                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyOption/EditPolicyOption.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyOption(RD_PolicyOption polOption, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        polOption.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_PolicyOptionSet.Add(polOption);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 12 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_PolicyOption newPolOption = db.RD_PolicyOptionSet.Where(m => m.PolicyOptionID == polOption.PolicyOptionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newPolOption != null)

                        {

                            newPolOption.PolicyOptionName = polOption.PolicyOptionName;

                            newPolOption.PolicyTypeID = polOption.PolicyTypeID;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 12 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_PolicyListStatus/_EditPolicyListStatus.cshtml", polOption);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_PolicyListStatus/EditPolicyListStatus.cshtml", polOption);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePolicyOption(int id)

        {

            RD_PolicyOption policyOption = db.RD_PolicyOptionSet.Where(m => m.PolicyTypeID == id).FirstOrDefault();

            if (policyOption != null)

            {

                try

                {

                    db.RD_PolicyOptionSet.Remove(policyOption);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 12 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyOptionDetails(int? id)

        {
            //Generic List Setter -- used for relational objects needed for selection.
            ViewBag.PolicyTypeID = new SelectList(db.RD_PolicyTypeSet, "PolicyTypeID", "PolicyTypeName");

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyOption policyOption = db.RD_PolicyOptionSet.Where(m => m.PolicyOptionID == id).FirstOrDefault();
                    ViewBag.PolicyTypeID = new SelectList(db.RD_PolicyTypeSet, "PolicyTypeID", "PolicyTypeName", policyOption.PolicyTypeID);

                    return PartialView("~/Views/ReferenceData/RD_PolicyOption/_EditPolicyOption.cshtml", policyOption);


                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyOption/_EditPolicyOption.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_PolicyOption policyOption = db.RD_PolicyOptionSet.Where(m => m.PolicyOptionID == id).FirstOrDefault();
                    ViewBag.PolicyTypeID = new SelectList(db.RD_PolicyTypeSet, "PolicyTypeID", "PolicyTypeName", policyOption.PolicyTypeID);

                    return PartialView("~/Views/ReferenceData/RD_PolicyOption/EditPolicyOption.cshtml", policyOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyOption/EditPolicyOption.cshtml");

            }

        }


        //Policy Status section 13
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPolicyStatusPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_PolicyStatus/PolicyStatusIndex.cshtml", db.RD_PolicyStatusSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyStatus(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyStatus policyStatus = db.RD_PolicyStatusSet.Where(m => m.PolicyStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyStatus/_EditPolicyStatus.cshtml", policyStatus);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyStatus/_EditPolicyStatus.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyStatus policyStatus = db.RD_PolicyStatusSet.Where(m => m.PolicyStatusID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyStatus/EditPolicyStatus.cshtml", policyStatus);
                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyStatus/EditPolicyStatus.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyStatus(RD_PolicyStatus polStatus, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        polStatus.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_PolicyStatusSet.Add(polStatus);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 13 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_PolicyStatus newPolStatus = db.RD_PolicyStatusSet.Where(m => m.PolicyStatusID == polStatus.PolicyStatusID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newPolStatus != null)

                        {

                            newPolStatus.PolicyStatusName = polStatus.PolicyStatusName;

                            newPolStatus.PolicyStatusScore = polStatus.PolicyStatusScore;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 13 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_PolicyStatus/_EditPolicyStatus.cshtml", polStatus);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_PolicyStatus/EditPolicyStatus.cshtml", polStatus);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePolicyStatus(int id)

        {

            RD_PolicyStatus policyStatus = db.RD_PolicyStatusSet.Where(m => m.PolicyStatusID == id).FirstOrDefault();

            if (policyStatus != null)

            {

                try

                {

                    db.RD_PolicyStatusSet.Remove(policyStatus);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 13 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyStatusDetails(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyStatus policyOption = db.RD_PolicyStatusSet.Where(m => m.PolicyStatusID == id).FirstOrDefault();
                    return PartialView("~/Views/ReferenceData/RD_PolicyStatus/_PolicyStatusDetails.cshtml", policyOption);


                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyStatus/_PolicyStatusDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_PolicyStatus policyOption = db.RD_PolicyStatusSet.Where(m => m.PolicyStatusID == id).FirstOrDefault();
                    return PartialView("~/Views/ReferenceData/RD_PolicyStatus/PolicyStatusDetails.cshtml", policyOption);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyStatus/PolicyStatusDetails.cshtml");

            }

        }


        //Policy Type section 14
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPolicyTypePartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_PolicyType/PolicyTypeIndex.cshtml", db.RD_PolicyTypeSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyType(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyType policyType = db.RD_PolicyTypeSet.Where(m => m.PolicyTypeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyType/_EditPolicyType.cshtml", policyType);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyType/_EditPolicyType.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyType policyType = db.RD_PolicyTypeSet.Where(m => m.PolicyTypeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyType/EditPolicyType.cshtml", policyType);
                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyType/EditPolicyType.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyType(RD_PolicyType polType, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        polType.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_PolicyTypeSet.Add(polType);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 14 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_PolicyType newPolType = db.RD_PolicyTypeSet.Where(m => m.PolicyTypeID == polType.PolicyTypeID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newPolType != null)

                        {

                            newPolType.PolicyTypeName = polType.PolicyTypeName;

                            newPolType.PreCommercialDemoPercentage = polType.PreCommercialDemoPercentage;

                            newPolType.DeploymentPercentage = polType.DeploymentPercentage;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 14 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_PolicyType/_EditPolicyType.cshtml", polType);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_PolicyType/EditPolicyType.cshtml", polType);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePolicyType(int id)

        {

            RD_PolicyType polType = db.RD_PolicyTypeSet.Where(m => m.PolicyTypeID == id).FirstOrDefault();

            if (polType != null)

            {

                try

                {

                    db.RD_PolicyTypeSet.Remove(polType);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 14 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyTypeDetails(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyType policyType = db.RD_PolicyTypeSet.Where(m => m.PolicyTypeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyType/_PolicyTypeDetails.cshtml", policyType);


                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyType/_PolicyTypeDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_PolicyType policyType = db.RD_PolicyTypeSet.Where(m => m.PolicyTypeID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyType/PolicyTypeDetails.cshtml", policyType);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyType/PolicyTypeDetails.cshtml");

            }

        }



        //Policy Weight section 15
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPolicyWeightPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_PolicyWeight/PolicyWeightIndex.cshtml", db.RD_PolicyWeightSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyWeight(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyWeight policyWeight = db.RD_PolicyWeightSet.Where(m => m.PolicyWeightID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyWeight/_EditPolicyWeight.cshtml", policyWeight);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyWeight/_EditPolicyWeight.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyWeight policyWeight = db.RD_PolicyWeightSet.Where(m => m.PolicyWeightID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyWeight/EditPolicyWeight.cshtml", policyWeight);
                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyWeight/EditPolicyWeight.cshtml");

            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyWeight(RD_PolicyWeight polWeight, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        polWeight.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_PolicyWeightSet.Add(polWeight);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 15 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_PolicyWeight newPolWeight = db.RD_PolicyWeightSet.Where(m => m.PolicyWeightID == polWeight.PolicyWeightID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newPolWeight != null)

                        {

                            newPolWeight.PolicyWeightName = polWeight.PolicyWeightName;

                            newPolWeight.PolicyWeightPercentage = polWeight.PolicyWeightPercentage;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 15 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_PolicyWeight/_EditPolicyWeight.cshtml", polWeight);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_PolicyWeight/EditPolicyWeight.cshtml", polWeight);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePolicyWeight(int id)

        {

            RD_PolicyWeight polWeight = db.RD_PolicyWeightSet.Where(m => m.PolicyWeightID == id).FirstOrDefault();

            if (polWeight != null)

            {

                try

                {

                    db.RD_PolicyWeightSet.Remove(polWeight);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 15 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyWeightDetails(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_PolicyWeight policyWeight = db.RD_PolicyWeightSet.Where(m => m.PolicyWeightID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyWeight/_PolicyWeightDetails.cshtml", policyWeight);


                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyWeight/_PolicyWeightDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    RD_PolicyWeight policyWeight = db.RD_PolicyWeightSet.Where(m => m.PolicyWeightID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_PolicyWeight/PolicyWeightDetails.cshtml", policyWeight);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_PolicyWeight/PolicyWeightDetails.cshtml");

            }

        }




        //Storage Criteria section 16
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetStorageCriteriaPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_StorageCriteria/StorageCriteriaIndex.cshtml", db.RD_StorageCriteriaSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditStorageCriteria(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_StorageCriteria storageCriteria = db.RD_StorageCriteriaSet.Where(m => m.StorageCriteriaID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_StorageCriteria/_EditStorageCriteria.cshtml", storageCriteria);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_StorageCriteria/_EditStorageCriteria.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_StorageCriteria storageCriteria = db.RD_StorageCriteriaSet.Where(m => m.StorageCriteriaID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_StorageCriteria/EditStorageCriteria.cshtml", storageCriteria);
                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_StorageCriteria/EditStorageCriteria.cshtml");

            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditStorageCriteria(RD_StorageCriteria storageCriteria, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        storageCriteria.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_StorageCriteriaSet.Add(storageCriteria);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 16 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        RD_StorageCriteria newStorageCriteria = db.RD_StorageCriteriaSet.Where(m => m.StorageCriteriaID == storageCriteria.StorageCriteriaID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newStorageCriteria != null)

                        {

                            newStorageCriteria.StorageCriteriaCode = storageCriteria.StorageCriteriaCode;

                            newStorageCriteria.StorageCriteriaValue = storageCriteria.StorageCriteriaValue;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 16 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_StorageCriteria/_EditStorageCriteria.cshtml", storageCriteria);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_StorageCriteria/EditStorageCriteria.cshtml", storageCriteria);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteStorageCriteria(int id)

        {

            RD_StorageCriteria storageCriteria = db.RD_StorageCriteriaSet.Where(m => m.StorageCriteriaID == id).FirstOrDefault();

            if (storageCriteria != null)

            {

                try

                {

                    db.RD_StorageCriteriaSet.Remove(storageCriteria);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 16 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult StorageCriteriaDetails(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_StorageCriteria storageCriteria = db.RD_StorageCriteriaSet.Where(m => m.StorageCriteriaID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_StorageCriteria/_StorageCriteriaDetails.cshtml", storageCriteria);


                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_StorageCriteria/_StorageCriteriaDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;



                    RD_StorageCriteria storageCriteria = db.RD_StorageCriteriaSet.Where(m => m.StorageCriteriaID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_StorageCriteria/StorageCriteriaDetails.cshtml", storageCriteria);


                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_StorageCriteria/StorageCriteriaDetails.cshtml");

            }

        }



        //Year section 17
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetYearPartialView()
        {
            return PartialView("~/Views/ReferenceData/RD_Year/YearIndex.cshtml", db.RD_YearDataSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditYear(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Year year = db.RD_YearDataSet.Where(m => m.YearID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Year/_EditYear.cshtml", year);

                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_Year/_EditYear.cshtml");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Year year = db.RD_YearDataSet.Where(m => m.YearID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Year/EditYear.cshtml", year);
                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_Year/EditYear.cshtml");

            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditYear(RD_Year year, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        year.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.RD_YearDataSet.Add(year);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 17 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {
                        RD_Year newYear = db.RD_YearDataSet.Where(m => m.YearID == year.YearID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newYear != null)

                        {

                            newYear.YearName = year.YearName;

                            newYear.YearStartDate = year.YearStartDate;
                            newYear.YearEndDate = year.YearEndDate;
                            newYear.YearHasData = year.YearHasData;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 17 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_Year/_EditYear.cshtml", year);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_Year/EditYear.cshtml", year);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteYear(int id)

        {

            RD_Year year = db.RD_YearDataSet.Where(m => m.YearID == id).FirstOrDefault();

            if (year != null)

            {

                try

                {

                    db.RD_YearDataSet.Remove(year);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 17 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult YearDetails(int? id)

        {
            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    RD_Year year = db.RD_YearDataSet.Where(m => m.YearID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Year/_YearDetails.cshtml", year);


                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_Year/_YearDetails.cshtml");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;



                    RD_Year year = db.RD_YearDataSet.Where(m => m.YearID == id).FirstOrDefault();

                    return PartialView("~/Views/ReferenceData/RD_Year/YearDetails.cshtml", year);


                }

                ViewBag.IsUpdate = false;

                return PartialView("~/Views/ReferenceData/RD_Year/YearDetails.cshtml");

            }

        }


        //Inherent Interest - Coal Consumption section 18
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetInherentCoalConsumptionView()
        {
            return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/Index.cshtml", db.InherentCoalConsumptionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentCoalConsumption(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestCoalConsumption coalCons = db.InherentCoalConsumptionSet.Where(m => m.InherentInterestCoalConsumptionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Edit.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Edit.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Edit.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Edit.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentCoalConsumption(RD_InherentInterestCoalConsumption coalCons, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        coalCons.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.InherentCoalConsumptionSet.Add(coalCons);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 18 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {
                        RD_InherentInterestCoalConsumption newCoalCons = db.InherentCoalConsumptionSet.Where(m => m.InherentInterestCoalConsumptionID == coalCons.InherentInterestCoalConsumptionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCoalCons != null)

                        {

                            newCoalCons.CoalConsumptionPercentageShare = coalCons.CoalConsumptionPercentageShare;
                            newCoalCons.CoalConsumptionScore = coalCons.CoalConsumptionScore;
                            newCoalCons.CoalConsumptionBand = coalCons.CoalConsumptionBand;
                            newCoalCons.CoalConsumptionTier = coalCons.CoalConsumptionTier;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 18 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Edit.cshtml", coalCons);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Edit.cshtml", coalCons);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteInherentCoalConsumption(int id)

        {

            RD_InherentInterestCoalConsumption coalCons = db.InherentCoalConsumptionSet.Where(m => m.InherentInterestCoalConsumptionID == id).FirstOrDefault();

            if (coalCons != null)

            {

                try

                {

                    db.InherentCoalConsumptionSet.Remove(coalCons);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 18 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult InherentCoalConsumptionDetails(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestCoalConsumption coalCons = db.InherentCoalConsumptionSet.Where(m => m.InherentInterestCoalConsumptionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Details.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Details.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Details.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestCoalConsumption/_Details.cshtml");
            }

        }



        //Inherent Interest - Coal Production section 19
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetInherentCoalProductionView()
        {
            return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalProduction/Index.cshtml", db.InherentCoalProductionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentCoalProduction(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestCoalProduction coalCons = db.InherentCoalProductionSet.Where(m => m.InherentInterestCoalProductionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Edit.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Edit.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Edit.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Edit.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentCoalProduction(RD_InherentInterestCoalProduction coalCons, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        coalCons.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.InherentCoalProductionSet.Add(coalCons);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 19 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {
                        RD_InherentInterestCoalProduction newCoalCons = db.InherentCoalProductionSet.Where(m => m.InherentInterestCoalProductionID == coalCons.InherentInterestCoalProductionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCoalCons != null)

                        {

                            newCoalCons.CoalProductionPercentageShare = coalCons.CoalProductionPercentageShare;
                            newCoalCons.CoalProductionScore = coalCons.CoalProductionScore;
                            newCoalCons.CoalProductionBand = coalCons.CoalProductionBand;
                            newCoalCons.CoalProductionTier = coalCons.CoalProductionTier;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 19 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Edit.cshtml", coalCons);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Edit.cshtml", coalCons);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteInherentCoalProduction(int id)

        {

            RD_InherentInterestCoalProduction coalCons = db.InherentCoalProductionSet.Where(m => m.InherentInterestCoalProductionID == id).FirstOrDefault();

            if (coalCons != null)

            {

                try

                {

                    db.InherentCoalProductionSet.Remove(coalCons);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 19 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult InherentCoalProductionDetails(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestCoalProduction coalCons = db.InherentCoalProductionSet.Where(m => m.InherentInterestCoalProductionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Details.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Details.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Details.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestCoalProduction/_Details.cshtml");
            }

        }




        //Inherent Interest - Gas Consumption section 20
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetInherentGasConsumptionView()
        {
            return PartialView("~/Views/ReferenceData/RD_InherentInterestGasConsumption/Index.cshtml", db.InherentGasConsumptionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentGasConsumption(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestGasConsumption coalCons = db.InherentGasConsumptionSet.Where(m => m.InherentInterestGasConsumptionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Edit.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Edit.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Edit.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Edit.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentGasConsumption(RD_InherentInterestGasConsumption coalCons, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        coalCons.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.InherentGasConsumptionSet.Add(coalCons);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 20 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {
                        RD_InherentInterestGasConsumption newCoalCons = db.InherentGasConsumptionSet.Where(m => m.InherentInterestGasConsumptionID == coalCons.InherentInterestGasConsumptionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCoalCons != null)

                        {

                            newCoalCons.GasConsumptionPercentageShare = coalCons.GasConsumptionPercentageShare;
                            newCoalCons.GasConsumptionScore = coalCons.GasConsumptionScore;
                            newCoalCons.GasConsumptionBand = coalCons.GasConsumptionBand;
                            newCoalCons.GasConsumptionTier = coalCons.GasConsumptionTier;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 20 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Edit.cshtml", coalCons);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Edit.cshtml", coalCons);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteInherentGasConsumption(int id)

        {

            RD_InherentInterestGasConsumption coalCons = db.InherentGasConsumptionSet.Where(m => m.InherentInterestGasConsumptionID == id).FirstOrDefault();

            if (coalCons != null)

            {

                try

                {

                    db.InherentGasConsumptionSet.Remove(coalCons);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 20 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult InherentGasConsumptionDetails(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestGasConsumption coalCons = db.InherentGasConsumptionSet.Where(m => m.InherentInterestGasConsumptionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Details.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Details.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Details.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestGasConsumption/_Details.cshtml");
            }

        }



        //Inherent Interest - Coal Production section 21
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetInherentGasProductionView()
        {
            return PartialView("~/Views/ReferenceData/RD_InherentInterestGasProduction/Index.cshtml", db.InherentGasProductionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentGasProduction(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestGasProduction coalCons = db.InherentGasProductionSet.Where(m => m.InherentInterestGasProductionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Edit.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Edit.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Edit.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Edit.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentCoalProduction(RD_InherentInterestGasProduction coalCons, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        coalCons.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.InherentGasProductionSet.Add(coalCons);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 21 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {
                        RD_InherentInterestGasProduction newCoalCons = db.InherentGasProductionSet.Where(m => m.InherentInterestGasProductionID == coalCons.InherentInterestGasProductionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCoalCons != null)

                        {

                            newCoalCons.GasProductionPercentageShare = coalCons.GasProductionPercentageShare;
                            newCoalCons.GasProductionScore = coalCons.GasProductionScore;
                            newCoalCons.GasProductionBand = coalCons.GasProductionBand;
                            newCoalCons.GasProductionTier = coalCons.GasProductionTier;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 21 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Edit.cshtml", coalCons);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Edit.cshtml", coalCons);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteInherentGasProduction(int id)

        {

            RD_InherentInterestGasProduction coalCons = db.InherentGasProductionSet.Where(m => m.InherentInterestGasProductionID == id).FirstOrDefault();

            if (coalCons != null)

            {

                try

                {

                    db.InherentGasProductionSet.Remove(coalCons);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 21 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult InherentGasProductionDetails(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestGasProduction coalCons = db.InherentGasProductionSet.Where(m => m.InherentInterestGasProductionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Details.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Details.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Details.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestGasProduction/_Details.cshtml");
            }

        }



        //Inherent Interest - Oil Consumption section 22
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetInherentOilConsumptionView()
        {
            return PartialView("~/Views/ReferenceData/RD_InherentInterestOilConsumption/Index.cshtml", db.InherentOilConsumptionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentOilConsumption(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestOilConsumption coalCons = db.InherentOilConsumptionSet.Where(m => m.InherentInterestOilConsumptionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Edit.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Edit.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Edit.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Edit.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentOilConsumption(RD_InherentInterestOilConsumption coalCons, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        coalCons.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.InherentOilConsumptionSet.Add(coalCons);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 22 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {
                        RD_InherentInterestOilConsumption newCoalCons = db.InherentOilConsumptionSet.Where(m => m.InherentInterestOilConsumptionID == coalCons.InherentInterestOilConsumptionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCoalCons != null)

                        {

                            newCoalCons.OilConsumptionPercentageShare = coalCons.OilConsumptionPercentageShare;
                            newCoalCons.OilConsumptionScore = coalCons.OilConsumptionScore;
                            newCoalCons.OilConsumptionBand = coalCons.OilConsumptionBand;
                            newCoalCons.OilConsumptionTier = coalCons.OilConsumptionTier;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 22 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Edit.cshtml", coalCons);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Edit.cshtml", coalCons);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteInherentOilConsumption(int id)

        {

            RD_InherentInterestOilConsumption coalCons = db.InherentOilConsumptionSet.Where(m => m.InherentInterestOilConsumptionID == id).FirstOrDefault();

            if (coalCons != null)

            {

                try

                {

                    db.InherentOilConsumptionSet.Remove(coalCons);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 22 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult InherentOilConsumptionDetails(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestOilConsumption coalCons = db.InherentOilConsumptionSet.Where(m => m.InherentInterestOilConsumptionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Details.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Details.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Details.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestOilConsumption/_Details.cshtml");
            }

        }



        //Inherent Interest - Oil Production section 23
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetInherentOilProductionView()
        {
            return PartialView("~/Views/ReferenceData/RD_InherentInterestOilProduction/Index.cshtml", db.InherentOilProductionSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentOilProduction(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestOilProduction coalCons = db.InherentOilProductionSet.Where(m => m.InherentInterestOilProductionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Edit.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Edit.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Edit.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Edit.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentOilProduction(RD_InherentInterestOilProduction coalCons, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        coalCons.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.InherentOilProductionSet.Add(coalCons);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 23 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {
                        RD_InherentInterestOilProduction newCoalCons = db.InherentOilProductionSet.Where(m => m.InherentInterestOilProductionID == coalCons.InherentInterestOilProductionID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCoalCons != null)

                        {

                            newCoalCons.OilProductionPercentageShare = coalCons.OilProductionPercentageShare;
                            newCoalCons.OilProductionScore = coalCons.OilProductionScore;
                            newCoalCons.OilProductionBand = coalCons.OilProductionBand;
                            newCoalCons.OilProductionTier = coalCons.OilProductionTier;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 23 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Edit.cshtml", coalCons);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Edit.cshtml", coalCons);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteInherentOilProduction(int id)

        {

            RD_InherentInterestOilProduction coalCons = db.InherentOilProductionSet.Where(m => m.InherentInterestOilProductionID == id).FirstOrDefault();

            if (coalCons != null)

            {

                try

                {

                    db.InherentOilProductionSet.Remove(coalCons);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 23 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult InherentOilProductionDetails(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestOilProduction coalCons = db.InherentOilProductionSet.Where(m => m.InherentInterestOilProductionID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Details.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Details.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Details.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestOilProduction/_Details.cshtml");
            }

        }



        //Inherent Interest - Weight section 24
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetInherentWeightView()
        {
            return PartialView("~/Views/ReferenceData/RD_InherentInterestWeight/Index.cshtml", db.InherentWeightSet.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentWeight(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestWeight coalCons = db.InherentWeightSet.Where(m => m.InherentInterestWeightID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestWeight/_Edit.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestWeight/_Edit.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestWeight/_Edit.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestWeight/_Edit.cshtml");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentWeight(RD_InherentInterestWeight coalCons, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        coalCons.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.InherentWeightSet.Add(coalCons);

                        db.SaveChanges();

                        return RedirectToAction("IndexOption", new { id = 24 });

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {
                        RD_InherentInterestWeight newCoalCons = db.InherentWeightSet.Where(m => m.InherentInterestWeightID == coalCons.InherentInterestWeightID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCoalCons != null)

                        {

                            newCoalCons.InherentInterestWeightName = coalCons.InherentInterestWeightName;
                            newCoalCons.ProductionWeightScore = coalCons.ProductionWeightScore;
                            newCoalCons.ConsumptionWeightScore = coalCons.ConsumptionWeightScore;

                            db.SaveChanges();

                        }

                        return RedirectToAction("IndexOption", new { id = 24 });

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("~/Views/ReferenceData/RD_InherentInterestWeight/_Edit.cshtml", coalCons);

            }

            else

            {

                return View("~/Views/ReferenceData/RD_InherentInterestWeight/_Edit.cshtml", coalCons);

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteInherentWeight(int id)

        {

            RD_InherentInterestWeight coalCons = db.InherentWeightSet.Where(m => m.InherentInterestWeightID == id).FirstOrDefault();

            if (coalCons != null)

            {

                try

                {

                    db.InherentWeightSet.Remove(coalCons);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("IndexOption", new { id = 24 });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult InherentWeightDetails(int? id)

        {
            if (id != null)

            {

                ViewBag.IsUpdate = true;

                RD_InherentInterestWeight coalCons = db.InherentWeightSet.Where(m => m.InherentInterestWeightID == id).FirstOrDefault();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/ReferenceData/RD_InherentInterestWeight/_Details.cshtml", coalCons);
                }
                else
                {
                    return View("~/Views/ReferenceData/RD_InherentInterestWeight/_Details.cshtml", coalCons);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/ReferenceData/RD_InherentInterestWeight/_Details.cshtml");
            }
            else
            {
                return View("~/Views/ReferenceData/RD_InherentInterestWeight/_Details.cshtml");
            }

        }

    }
}

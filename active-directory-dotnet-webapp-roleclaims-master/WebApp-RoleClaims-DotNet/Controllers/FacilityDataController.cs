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
    public class FacilityDataController : Controller
    {
        private GCCSIContext db = new GCCSIContext();

        // GET: FacilityData
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
                    var facilityData = db.FacilityData.Include(f => f.Country).Include(f => f.FacilityCategory).Include(f => f.FacilityIndustry).Include(f => f.FacilityStatus).Include(f => f.FacilityStorageCode).Include(f => f.FacilityTransportCode).Include(f => f.Region).Include(f => f.Year).Include(f => f.FacilityCaptureType);
                    return View(facilityData.ToList());
                }
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetSelectCountriesList(int regionID, int? countryID)
        {
            string htmlElementsOption = "";
            foreach (var country in db.RD_CountrySet)
            {
                if (country.RegionID == regionID)
                {
                    if (countryID != null && country.CountryID == countryID.Value)
                    {
                        //Add selected tag.
                        htmlElementsOption = htmlElementsOption + "<option value=" + country.CountryID + " selected>" + country.CountryName + "</option>";
                    }
                    else
                    {
                        //Append to string.
                        htmlElementsOption = htmlElementsOption + "<option value=" + country.CountryID + ">" + country.CountryName + "</option>";
                    }
                }
            }

            return Content(htmlElementsOption);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(int? id)

        {
            //Generic List Setter
            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.FacilityCategoryID = new SelectList(db.RD_FacilityCategorySet, "FacilityCategoryID", "FacilityCategoryName");
            ViewBag.FacilityIndustryID = new SelectList(db.RD_FacilityIndustrySet, "FacilityIndustryID", "FacilityIndustryName");
            ViewBag.FacilityStatusID = new SelectList(db.RD_FacilityStatusSet, "FacilityStatusID", "FacilityStatusName");
            ViewBag.FacilityStorageCodeID = new SelectList(db.RD_FacilityStorageCodeSet, "FacilityStorageCodeID", "FacilityStorageCodeName");
            ViewBag.FacilityTransportCodeID = new SelectList(db.RD_FacilityTransportCodeSet, "FacilityTransportCodeID", "FacilityTransportCodeName");
            ViewBag.FacilityCaptureTypeID = new SelectList(db.RD_FacilityCaptureTypeSet, "FacilityCaptureTypeID", "FacilityCaptureTypeName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (Request.IsAjaxRequest())

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    FacilityData facilityDataObject = db.FacilityData.Where(m => m.FacilityDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", facilityDataObject.CountryID);
                    ViewBag.FacilityCategoryID = new SelectList(db.RD_FacilityCategorySet, "FacilityCategoryID", "FacilityCategoryName", facilityDataObject.FacilityCategoryID);
                    ViewBag.FacilityIndustryID = new SelectList(db.RD_FacilityIndustrySet, "FacilityIndustryID", "FacilityIndustryName", facilityDataObject.FacilityIndustryID);
                    ViewBag.FacilityStatusID = new SelectList(db.RD_FacilityStatusSet, "FacilityStatusID", "FacilityStatusName", facilityDataObject.FacilityStatusID);
                    ViewBag.FacilityStorageCodeID = new SelectList(db.RD_FacilityStorageCodeSet, "FacilityStorageCodeID", "FacilityStorageCodeName", facilityDataObject.FacilityStorageCodeID);
                    ViewBag.FacilityTransportCodeID = new SelectList(db.RD_FacilityTransportCodeSet, "FacilityTransportCodeID", "FacilityTransportCodeName", facilityDataObject.FacilityTransportCodeID);
                    ViewBag.FacilityCaptureTypeID = new SelectList(db.RD_FacilityCaptureTypeSet, "FacilityCaptureTypeID", "FacilityCaptureTypeName", facilityDataObject.FacilityCaptureTypeID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", facilityDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", facilityDataObject.YearID);

                    return PartialView("_Edit", facilityDataObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Edit");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    FacilityData facilityDataObject = db.FacilityData.Where(m => m.FacilityDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", facilityDataObject.CountryID);
                    ViewBag.FacilityCategoryID = new SelectList(db.RD_FacilityCategorySet, "FacilityCategoryID", "FacilityCategoryName", facilityDataObject.FacilityCategoryID);
                    ViewBag.FacilityIndustryID = new SelectList(db.RD_FacilityIndustrySet, "FacilityIndustryID", "FacilityIndustryName", facilityDataObject.FacilityIndustryID);
                    ViewBag.FacilityStatusID = new SelectList(db.RD_FacilityStatusSet, "FacilityStatusID", "FacilityStatusName", facilityDataObject.FacilityStatusID);
                    ViewBag.FacilityStorageCodeID = new SelectList(db.RD_FacilityStorageCodeSet, "FacilityStorageCodeID", "FacilityStorageCodeName", facilityDataObject.FacilityStorageCodeID);
                    ViewBag.FacilityTransportCodeID = new SelectList(db.RD_FacilityTransportCodeSet, "FacilityTransportCodeID", "FacilityTransportCodeName", facilityDataObject.FacilityTransportCodeID);
                    ViewBag.FacilityCaptureTypeID = new SelectList(db.RD_FacilityCaptureTypeSet, "FacilityCaptureTypeID", "FacilityCaptureTypeName", facilityDataObject.FacilityCaptureTypeID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", facilityDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", facilityDataObject.YearID);

                    return PartialView("Edit", facilityDataObject);

                }

                ViewBag.IsUpdate = false;

                return View("Edit");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(FacilityData facilityDataObject, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        facilityDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.FacilityData.Add(facilityDataObject);

                        db.SaveChanges();

                        return RedirectToAction("Index");

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

                        FacilityData newFacilityDataObj = db.FacilityData.Where(m => m.FacilityDataID == facilityDataObject.FacilityDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newFacilityDataObj != null)

                        {

                            newFacilityDataObj.Title = facilityDataObject.Title;

                            newFacilityDataObj.FacilityCategoryID = facilityDataObject.FacilityCategoryID;

                            newFacilityDataObj.FacilityStatusID = facilityDataObject.FacilityStatusID;

                            newFacilityDataObj.CountryID = facilityDataObject.CountryID;

                            newFacilityDataObj.OperationDate = facilityDataObject.OperationDate;

                            newFacilityDataObj.FacilityIndustryID = facilityDataObject.FacilityIndustryID;

                            newFacilityDataObj.CaptureCapacityMin = facilityDataObject.CaptureCapacityMin;

                            newFacilityDataObj.CaptureCapacityMax = facilityDataObject.CaptureCapacityMax;

                            newFacilityDataObj.CaptureCapacity = facilityDataObject.CaptureCapacity;

                            newFacilityDataObj.ShortDescription = facilityDataObject.ShortDescription;

                            newFacilityDataObj.Proponents = facilityDataObject.Proponents;

                            newFacilityDataObj.Location = facilityDataObject.Location;

                            newFacilityDataObj.Industry_Feedstock = facilityDataObject.Industry_Feedstock;

                            newFacilityDataObj.FacilityCaptureTypeID = facilityDataObject.FacilityCaptureTypeID;

                            newFacilityDataObj.CaptureSource = facilityDataObject.CaptureSource;

                            newFacilityDataObj.CaptureMethod = facilityDataObject.CaptureMethod;

                            newFacilityDataObj.NewBuildOrRetrofit = facilityDataObject.NewBuildOrRetrofit;

                            newFacilityDataObj.StorageType = facilityDataObject.StorageType;

                            newFacilityDataObj.StorageFormationAndDepth = facilityDataObject.StorageFormationAndDepth;

                            newFacilityDataObj.UtilisationType = facilityDataObject.UtilisationType;

                            newFacilityDataObj.TransportationType = facilityDataObject.TransportationType;

                            newFacilityDataObj.TransportationDistance = facilityDataObject.TransportationDistance;

                            newFacilityDataObj.FacilityDescription = facilityDataObject.FacilityDescription;

                            newFacilityDataObj.KeyMilestone = facilityDataObject.KeyMilestone;

                            newFacilityDataObj.CurrencyDate = facilityDataObject.CurrencyDate;

                            newFacilityDataObj.ReferenceLink = facilityDataObject.ReferenceLink;

                            newFacilityDataObj.FacilityStorageCodeID = facilityDataObject.FacilityStorageCodeID;

                            newFacilityDataObj.FacilityTransportCodeID = facilityDataObject.FacilityTransportCodeID;

                            newFacilityDataObj.RegionID = facilityDataObject.RegionID;

                            newFacilityDataObj.ExcludeFromMap_HideInformation = facilityDataObject.ExcludeFromMap_HideInformation;

                            newFacilityDataObj.CaptureLatitude = facilityDataObject.CaptureLatitude;

                            newFacilityDataObj.CaptureLongitude = facilityDataObject.CaptureLongitude;

                            newFacilityDataObj.StorageLatitude = facilityDataObject.StorageLatitude;

                            newFacilityDataObj.StorageLongitude = facilityDataObject.StorageLongitude;

                            newFacilityDataObj.Summary = facilityDataObject.Summary;

                            newFacilityDataObj.Note = facilityDataObject.Note;

                            newFacilityDataObj.YearID = facilityDataObject.YearID;

                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_Edit", facilityDataObject);

            }

            else

            {

                return View("Edit", facilityDataObject);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)

        {
            //Generic List Setter
            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.FacilityCategoryID = new SelectList(db.RD_FacilityCategorySet, "FacilityCategoryID", "FacilityCategoryName");
            ViewBag.FacilityIndustryID = new SelectList(db.RD_FacilityIndustrySet, "FacilityIndustryID", "FacilityIndustryName");
            ViewBag.FacilityStatusID = new SelectList(db.RD_FacilityStatusSet, "FacilityStatusID", "FacilityStatusName");
            ViewBag.FacilityStorageCodeID = new SelectList(db.RD_FacilityStorageCodeSet, "FacilityStorageCodeID", "FacilityStorageCodeName");
            ViewBag.FacilityTransportCodeID = new SelectList(db.RD_FacilityTransportCodeSet, "FacilityTransportCodeID", "FacilityTransportCodeName");
            ViewBag.FacilityCaptureTypeID = new SelectList(db.RD_FacilityCaptureTypeSet, "FacilityCaptureTypeID", "FacilityCaptureTypeName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    FacilityData facilityDataObject = db.FacilityData.Where(m => m.FacilityDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", facilityDataObject.CountryID);
                    ViewBag.FacilityCategoryID = new SelectList(db.RD_FacilityCategorySet, "FacilityCategoryID", "FacilityCategoryName", facilityDataObject.FacilityCategoryID);
                    ViewBag.FacilityIndustryID = new SelectList(db.RD_FacilityIndustrySet, "FacilityIndustryID", "FacilityIndustryName", facilityDataObject.FacilityIndustryID);
                    ViewBag.FacilityStatusID = new SelectList(db.RD_FacilityStatusSet, "FacilityStatusID", "FacilityStatusName", facilityDataObject.FacilityStatusID);
                    ViewBag.FacilityStorageCodeID = new SelectList(db.RD_FacilityStorageCodeSet, "FacilityStorageCodeID", "FacilityStorageCodeName", facilityDataObject.FacilityStorageCodeID);
                    ViewBag.FacilityTransportCodeID = new SelectList(db.RD_FacilityTransportCodeSet, "FacilityTransportCodeID", "FacilityTransportCodeName", facilityDataObject.FacilityTransportCodeID);
                    ViewBag.FacilityCaptureTypeID = new SelectList(db.RD_FacilityCaptureTypeSet, "FacilityCaptureTypeID", "FacilityCaptureTypeName", facilityDataObject.FacilityCaptureTypeID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", facilityDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", facilityDataObject.YearID);

                    return PartialView("_Details", facilityDataObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Details");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    FacilityData facilityDataObject = db.FacilityData.Where(m => m.FacilityDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", facilityDataObject.CountryID);
                    ViewBag.FacilityCategoryID = new SelectList(db.RD_FacilityCategorySet, "FacilityCategoryID", "FacilityCategoryName", facilityDataObject.FacilityCategoryID);
                    ViewBag.FacilityIndustryID = new SelectList(db.RD_FacilityIndustrySet, "FacilityIndustryID", "FacilityIndustryName", facilityDataObject.FacilityIndustryID);
                    ViewBag.FacilityStatusID = new SelectList(db.RD_FacilityStatusSet, "FacilityStatusID", "FacilityStatusName", facilityDataObject.FacilityStatusID);
                    ViewBag.FacilityStorageCodeID = new SelectList(db.RD_FacilityStorageCodeSet, "FacilityStorageCodeID", "FacilityStorageCodeName", facilityDataObject.FacilityStorageCodeID);
                    ViewBag.FacilityTransportCodeID = new SelectList(db.RD_FacilityTransportCodeSet, "FacilityTransportCodeID", "FacilityTransportCodeName", facilityDataObject.FacilityTransportCodeID);
                    ViewBag.FacilityCaptureTypeID = new SelectList(db.RD_FacilityCaptureTypeSet, "FacilityCaptureTypeID", "FacilityCaptureTypeName", facilityDataObject.FacilityCaptureTypeID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", facilityDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", facilityDataObject.YearID);

                    return PartialView("Details", facilityDataObject);

                }

                ViewBag.IsUpdate = false;

                return View("Details");

            }

        }


        // GET: LegalRegulatoryDatas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {

            FacilityData facilityDataObject = db.FacilityData.Where(m => m.FacilityDataID == id).FirstOrDefault();

            if (facilityDataObject != null)

            {

                try

                {

                    db.FacilityData.Remove(facilityDataObject);

                    db.SaveChanges();

                }

                catch { }

            }

            // TODO: Need to add in the redirect action to goto the correct tab.
            return RedirectToAction("Index");
        }

        // POST: FacilityData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            FacilityData facilityData = db.FacilityData.Find(id);
            db.FacilityData.Remove(facilityData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

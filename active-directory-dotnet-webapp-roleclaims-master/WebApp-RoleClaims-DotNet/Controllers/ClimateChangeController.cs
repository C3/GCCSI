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
    public class ClimateChangeController : Controller
    {
        private GCCSIContext db = new GCCSIContext();

        // GET: ClimateChange
        public ActionResult Index()
        {
            //Test for admin role based auth.
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
                    return View();
                }
            }
            return View();            
        }


        /// <summary>
        /// 
        /// Region to the country filter -- country id in case there is already one chosen (in the event of an edit).
        /// 
        /// </summary>
        /// <param name="regionID"></param>
        /// <param name="countryID"></param>
        /// <returns></returns>
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


        // ---- This section is for the Climate Change Initiative Data ----

        // GET: ClimateChangeInitiativeIndex
        [Authorize(Roles = "Admin")]
        public ActionResult ClimateChangeInitiativeIndex()
        {
            ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
            var climateChangeDataSet = db.ClimateChangeDataSet.Include(c => c.Country).Include(c => c.Region).Include(c => c.Year);
            return PartialView("ClimateChangeDataIndex", climateChangeDataSet.ToList());
        }

        // GET: ClimateChangeInitiativeDelete
        [Authorize(Roles = "Admin")]
        public ActionResult ClimateChangeInitiativeDelete(int? id)
        {

            ClimateChangeData climateDataObject = db.ClimateChangeDataSet.Where(m => m.ClimateChangeDataID == id).FirstOrDefault();

            if (climateDataObject != null)

            {

                try

                {

                    db.ClimateChangeDataSet.Remove(climateDataObject);

                    db.SaveChanges();

                }

                catch { }

            }

            // TODO: Need to add in the redirect action to goto the correct tab.
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditClimateChangeInitiativeRecord(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    ClimateChangeData climateChangeObject = db.ClimateChangeDataSet.Where(m => m.ClimateChangeDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", climateChangeObject.CountryID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", climateChangeObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", climateChangeObject.YearID);

                    return PartialView("_ClimateChangeEdit", climateChangeObject);

                }

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

                ViewBag.IsUpdate = false;

                return PartialView("_ClimateChangeEdit");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    ClimateChangeData climateChangeObject = db.ClimateChangeDataSet.Where(m => m.ClimateChangeDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", climateChangeObject.CountryID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", climateChangeObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", climateChangeObject.YearID);

                    return PartialView("ClimateChangeEdit", climateChangeObject);

                }

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

                ViewBag.IsUpdate = false;

                return PartialView("ClimateChangeEdit");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditClimateChangeInitiativeRecord(ClimateChangeData climateChangeDataObject, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        climateChangeDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.ClimateChangeDataSet.Add(climateChangeDataObject);

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

                        ClimateChangeData newClimateChangeObj = db.ClimateChangeDataSet.Where(m => m.ClimateChangeDataID == climateChangeDataObject.ClimateChangeDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newClimateChangeObj != null)

                        {

                            newClimateChangeObj.RegionID = climateChangeDataObject.RegionID;

                            newClimateChangeObj.CountryID = climateChangeDataObject.CountryID;

                            newClimateChangeObj.KeyMechanismsInNDC = climateChangeDataObject.KeyMechanismsInNDC;

                            newClimateChangeObj.PercentageReduction = climateChangeDataObject.PercentageReduction;

                            newClimateChangeObj.BaseYear = climateChangeDataObject.BaseYear;

                            newClimateChangeObj.TargetYear = climateChangeDataObject.TargetYear;

                            newClimateChangeObj.Source = climateChangeDataObject.Source;

                            newClimateChangeObj.Summary = climateChangeDataObject.Summary;

                            newClimateChangeObj.Note = climateChangeDataObject.Note;

                            newClimateChangeObj.YearID = climateChangeDataObject.YearID;


                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_ClimateChangeEdit", climateChangeDataObject);

            }

            else

            {

                return View("ClimateChangeEdit", climateChangeDataObject);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ClimateChangeInitiativeDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    ClimateChangeData climateChangeObj = db.ClimateChangeDataSet.Where(m => m.ClimateChangeDataID == id).FirstOrDefault();

                    return PartialView("_ClimateChangeDetails", climateChangeObj);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_ClimateChangeDetails");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    ClimateChangeData climateChangeObj = db.ClimateChangeDataSet.Where(m => m.ClimateChangeDataID == id).FirstOrDefault();

                    return PartialView("ClimateChangeDetails", climateChangeObj);

                }

                ViewBag.IsUpdate = false;

                return PartialView("ClimateChangeDetails");

            }

        }






        // ---- This section is for the Emissions Data ----
        [Authorize(Roles = "Admin")]
        public ActionResult EmissionsIndex()
        {
            ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
            var emissionsDataSet = db.EmissionsDataSet.Include(e => e.Country).Include(e => e.Region).Include(e => e.Year);
            return PartialView("EmissionsIndex", emissionsDataSet.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EmissionsDelete(int? id)
        {

            EmissionsData emissionsDataObject = db.EmissionsDataSet.Where(m => m.EmissionsDataID == id).FirstOrDefault();

            if (emissionsDataObject != null)

            {

                try

                {

                    db.EmissionsDataSet.Remove(emissionsDataObject);

                    db.SaveChanges();

                }

                catch { }

            }

            // TODO: Need to add in the redirect action to goto the correct tab.
            return RedirectToAction("Index");


        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditEmissionsRecord(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    EmissionsData emissionsDataObject = db.EmissionsDataSet.Where(m => m.EmissionsDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", emissionsDataObject.CountryID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", emissionsDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", emissionsDataObject.YearID);

                    return PartialView("_EmissionsEdit", emissionsDataObject);

                }

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

                ViewBag.IsUpdate = false;

                return PartialView("_EmissionsEdit");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    EmissionsData emissionsDataObject = db.EmissionsDataSet.Where(m => m.EmissionsDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", emissionsDataObject.CountryID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", emissionsDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", emissionsDataObject.YearID);

                    return PartialView("EmissionsEdit", emissionsDataObject);

                }

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

                ViewBag.IsUpdate = false;

                return PartialView("EmissionsEdit");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditEmissionsRecord(EmissionsData emissionsDataObject, string cmd)
        {
            if (ModelState.IsValid)

            {
                if (cmd == "Save")

                {
                    try

                    {
                        //Add appropriate update date field.
                        emissionsDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.EmissionsDataSet.Add(emissionsDataObject);

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

                        EmissionsData newEmissionsDataObject = db.EmissionsDataSet.Where(m => m.EmissionsDataID == emissionsDataObject.EmissionsDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newEmissionsDataObject != null)

                        {

                            newEmissionsDataObject.RegionID = emissionsDataObject.RegionID;

                            newEmissionsDataObject.CountryID = emissionsDataObject.CountryID;

                            newEmissionsDataObject.OtherIndustrialCombustion = emissionsDataObject.OtherIndustrialCombustion;

                            newEmissionsDataObject.NonCombustion = emissionsDataObject.NonCombustion;

                            newEmissionsDataObject.PowerIndustry = emissionsDataObject.PowerIndustry;

                            newEmissionsDataObject.Total = emissionsDataObject.Total;

                            newEmissionsDataObject.Summary = emissionsDataObject.Summary;

                            newEmissionsDataObject.Note = emissionsDataObject.Note;

                            newEmissionsDataObject.YearID = emissionsDataObject.YearID;


                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_EmissionsEdit", emissionsDataObject);

            }

            else

            {

                return View("EmissionsEdit", emissionsDataObject);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EmissionsRecordDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    EmissionsData emissionsDataObject = db.EmissionsDataSet.Where(m => m.EmissionsDataID == id).FirstOrDefault();

                    return PartialView("_EmissionsDetails", emissionsDataObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_EmissionsDetails");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    EmissionsData emissionsDataObject = db.EmissionsDataSet.Where(m => m.EmissionsDataID == id).FirstOrDefault();

                    return PartialView("EmissionsDetails", emissionsDataObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("EmissionsDetails");

            }

        }




        // ---- This section is for the Storage Estimates Data ----
        [Authorize(Roles = "Admin")]
        public ActionResult StorageEstimatesIndex()
        {
            ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
            var storageEstimatesDatas = db.StorageEstimatesDatas.Include(s => s.Country).Include(s => s.Region).Include(s => s.Year);
            return PartialView("StorageEstimatesIndex", storageEstimatesDatas.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult StorageEstimatesDelete(int? id)
        {

            StorageEstimatesData storageDataEstObj = db.StorageEstimatesDatas.Where(m => m.StorageEstimatesDataID == id).FirstOrDefault();

            if (storageDataEstObj != null)
            {
                try
                {
                    db.StorageEstimatesDatas.Remove(storageDataEstObj);
                    db.SaveChanges();
                }
                catch { }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditStorageEstimates(int? id)

        {

            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (id != null)

            {

                ViewBag.IsUpdate = true;

                StorageEstimatesData storageDataEstObj = db.StorageEstimatesDatas.Where(m => m.StorageEstimatesDataID == id).FirstOrDefault();

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", storageDataEstObj.CountryID);
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", storageDataEstObj.RegionID);
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", storageDataEstObj.YearID);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_StorageEstimateEdit", storageDataEstObj);
                }
                else
                {
                    return View("StorageEstimateEdit", storageDataEstObj);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_StorageEstimateEdit");
            }
            else
            {
                return View("StorageEstimateEdit");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditStorageEstimates(StorageEstimatesData storageDataEstObj, string cmd)
        {
            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        storageDataEstObj.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.StorageEstimatesDatas.Add(storageDataEstObj);

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

                        StorageEstimatesData newStorageDataEstObj = db.StorageEstimatesDatas.Where(m => m.StorageEstimatesDataID == storageDataEstObj.StorageEstimatesDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newStorageDataEstObj != null)

                        {

                            newStorageDataEstObj.RegionID = storageDataEstObj.RegionID;

                            newStorageDataEstObj.CountryID = storageDataEstObj.CountryID;

                            newStorageDataEstObj.NationalResourceEstimates = storageDataEstObj.NationalResourceEstimates;

                            newStorageDataEstObj.ConfidenceInEstimates = storageDataEstObj.ConfidenceInEstimates;

                            newStorageDataEstObj.ProspectiveBasins = storageDataEstObj.ProspectiveBasins;

                            newStorageDataEstObj.Summary = storageDataEstObj.Summary;

                            newStorageDataEstObj.Note = storageDataEstObj.Note;

                            newStorageDataEstObj.YearID = storageDataEstObj.YearID;

                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_StorageEstimateEdit", storageDataEstObj);

            }

            else

            {

                return View("StorageEstimateEdit", storageDataEstObj);

            }


        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult StorageEstimatesDetails(int? id)

        {

            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (id != null)

            {

                ViewBag.IsUpdate = true;

                StorageEstimatesData storageEstDataObj = db.StorageEstimatesDatas.Where(m => m.StorageEstimatesDataID == id).FirstOrDefault();

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", storageEstDataObj.CountryID);
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", storageEstDataObj.RegionID);
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", storageEstDataObj.YearID);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_StorageEstimatesDetails", storageEstDataObj);
                }
                else
                {
                    return View("StorageEstimatesDetails", storageEstDataObj);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_StorageEstimatesDetails");
            }
            else
            {
                return View("StorageEstimatesDetails");
            }

        }




        // ---- This section is for the CCS Readiness Data ----
        [Authorize(Roles = "Admin")]
        public ActionResult CCSReadinessIndex()
        {
            ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
            var cCSReadinessIndexDatas = db.CCSReadinessIndexDatas.Include(c => c.Country).Include(c => c.Region).Include(c => c.Year);
            return PartialView("CCSReadinessIndex", cCSReadinessIndexDatas.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CCSReadinessDelete(int? id)
        {

            CCSReadinessIndexData ccsIndexObj = db.CCSReadinessIndexDatas.Where(m => m.ReadinessIndexDataID == id).FirstOrDefault();

            if (ccsIndexObj != null)

            {

                try

                {

                    db.CCSReadinessIndexDatas.Remove(ccsIndexObj);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditCCSReadiness(int? id)

        {

            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (id != null)

            {

                ViewBag.IsUpdate = true;

                CCSReadinessIndexData ccsIndexObj = db.CCSReadinessIndexDatas.Where(m => m.ReadinessIndexDataID == id).FirstOrDefault();

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", ccsIndexObj.CountryID);
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", ccsIndexObj.RegionID);
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", ccsIndexObj.YearID);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_CCSReadinessEdit", ccsIndexObj);
                }
                else
                {
                    return View("CCSReadinessEdit", ccsIndexObj);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CCSReadinessEdit");
            }
            else
            {
                return View("CCSReadinessEdit");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditCCSReadiness(CCSReadinessIndexData ccsIndexObj, string cmd)
        {
            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        ccsIndexObj.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.CCSReadinessIndexDatas.Add(ccsIndexObj);

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

                        CCSReadinessIndexData newCcsIndexObj = db.CCSReadinessIndexDatas.Where(m => m.ReadinessIndexDataID == ccsIndexObj.ReadinessIndexDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newCcsIndexObj != null)

                        {

                            newCcsIndexObj.RegionID = ccsIndexObj.RegionID;

                            newCcsIndexObj.CountryID = ccsIndexObj.CountryID;

                            newCcsIndexObj.PolicyScore = ccsIndexObj.PolicyScore;

                            newCcsIndexObj.PolicyScore_100 = ccsIndexObj.PolicyScore_100;

                            newCcsIndexObj.StorageScore = ccsIndexObj.StorageScore;

                            newCcsIndexObj.LegalRegulatoryScore = ccsIndexObj.LegalRegulatoryScore;

                            newCcsIndexObj.LegalRegulatoryScore_100 = ccsIndexObj.LegalRegulatoryScore_100;

                            newCcsIndexObj.CCSReadinessIndexScore = ccsIndexObj.CCSReadinessIndexScore;

                            newCcsIndexObj.Summary = ccsIndexObj.Summary;

                            newCcsIndexObj.Note = ccsIndexObj.Note;

                            newCcsIndexObj.YearID = ccsIndexObj.YearID;

                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_CCSReadinessEdit", ccsIndexObj);

            }

            else

            {

                return View("CCSReadinessEdit", ccsIndexObj);

            }


        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CCSReadinessDetails(int? id)

        {
            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (id != null)

            {

                ViewBag.IsUpdate = true;

                CCSReadinessIndexData ccsIndexObj = db.CCSReadinessIndexDatas.Where(m => m.ReadinessIndexDataID == id).FirstOrDefault();

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", ccsIndexObj.CountryID);
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", ccsIndexObj.RegionID);
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", ccsIndexObj.YearID);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_CCSReadinessDetails", ccsIndexObj);
                }
                else
                {
                    return View("CCSReadinessDetails", ccsIndexObj);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CCSReadinessDetails");
            }
            else
            {
                return View("CCSReadinessDetails");
            }

        }




        // ---- This section is for the Inherent Interest ----
        [Authorize(Roles = "Admin")]
        public ActionResult InherentInterestIndex()
        {
            ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
            var inherentInterestDataSet = db.InherentInterestDataSet.Include(i => i.Country).Include(i => i.Region).Include(i => i.Year);
            return PartialView("InherentInterestIndex", inherentInterestDataSet.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult InherentInterestDelete(int? id)
        {

            InherentInterestData inherentInterestDataObject = db.InherentInterestDataSet.Where(m => m.InherentInterestID == id).FirstOrDefault();

            if (inherentInterestDataObject != null)

            {

                try

                {

                    db.InherentInterestDataSet.Remove(inherentInterestDataObject);

                    db.SaveChanges();

                }

                catch { }

            }

            // TODO: Need to add in the redirect action to goto the correct tab.
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentInterest(int? id)

        {

            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (id != null)

            {

                ViewBag.IsUpdate = true;

                InherentInterestData inherentInterestDataObject = db.InherentInterestDataSet.Where(m => m.InherentInterestID == id).FirstOrDefault();

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", inherentInterestDataObject.CountryID);
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", inherentInterestDataObject.RegionID);
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", inherentInterestDataObject.YearID);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_InherentInterestEdit", inherentInterestDataObject);
                }
                else
                {
                    return View("InherentInterestEdit", inherentInterestDataObject);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_InherentInterestEdit");
            }
            else
            {
                return View("InherentInterestEdit");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditInherentInterest(InherentInterestData inherentInterestDataObject, string cmd)
        {
            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        inherentInterestDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.InherentInterestDataSet.Add(inherentInterestDataObject);

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

                        InherentInterestData newInherentInterestDataObject = db.InherentInterestDataSet.Where(m => m.InherentInterestID == inherentInterestDataObject.InherentInterestID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newInherentInterestDataObject != null)

                        {

                            newInherentInterestDataObject.RegionID = inherentInterestDataObject.RegionID;

                            newInherentInterestDataObject.CountryID = inherentInterestDataObject.CountryID;

                            newInherentInterestDataObject.OilProduction = inherentInterestDataObject.OilProduction;

                            newInherentInterestDataObject.GasProduction = inherentInterestDataObject.GasProduction;

                            newInherentInterestDataObject.CoalProduction = inherentInterestDataObject.CoalProduction;

                            newInherentInterestDataObject.OilConsumption = inherentInterestDataObject.OilConsumption;

                            newInherentInterestDataObject.GasConsumption = inherentInterestDataObject.GasConsumption;

                            newInherentInterestDataObject.CoalConsumption = inherentInterestDataObject.CoalConsumption;

                            newInherentInterestDataObject.Summary = inherentInterestDataObject.Summary;

                            newInherentInterestDataObject.Note = inherentInterestDataObject.Note;

                            newInherentInterestDataObject.YearID = inherentInterestDataObject.YearID;

                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_InherentInterestEdit", inherentInterestDataObject);

            }

            else

            {

                return View("InherentInterestEdit", inherentInterestDataObject);

            }


        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult InherentInterestDetails(int? id)

        {
            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (id != null)

            {

                ViewBag.IsUpdate = true;

                InherentInterestData inherentInterestDataObject = db.InherentInterestDataSet.Where(m => m.InherentInterestID == id).FirstOrDefault();

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", inherentInterestDataObject.CountryID);
                ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", inherentInterestDataObject.RegionID);
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", inherentInterestDataObject.YearID);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_InherentIndustryDetails", inherentInterestDataObject);
                }
                else
                {
                    return View("InherentIndustryDetails", inherentInterestDataObject);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_InherentIndustryDetails");
            }
            else
            {
                return View("InherentIndustryDetails");
            }

        }

    }
}

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
using System.Security.Claims;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using GCCSI_CO2RE.Utils;

namespace GCCSI_CO2RE.Controllers
{
    public class StorageDataController : Controller
    {
        private GCCSIContext db = new GCCSIContext();

        // GET: StorageData
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
                    return View(db.StorageDataSet.ToList());
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                ViewBag.PossibleRegions = db.GetRegions();
                ViewBag.PossibleCountries = db.GetCountries();
                ViewBag.PossibleCriteria = db.GetStorageCriteria();
                ViewBag.PossibleYears = db.GetYearList();
                ViewBag.NullTrueFalse = db.GetTrueFalseNullList();

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    StorageData student = db.StorageDataSet.Where(m => m.StorageDataID == id).FirstOrDefault();

                    return PartialView("_Edit", student);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Edit");

            }

            else

            {
                ViewBag.PossibleRegions = db.GetRegions();
                ViewBag.PossibleCountries = db.GetCountries();
                ViewBag.PossibleCriteria = db.GetStorageCriteria();
                ViewBag.PossibleYears = db.GetYearList();
                ViewBag.NullTrueFalse = db.GetTrueFalseNullList();

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    StorageData student = db.StorageDataSet.Where(m => m.StorageDataID == id).FirstOrDefault();

                    return PartialView("Edit", student);

                }

                ViewBag.IsUpdate = false;

                return View("Edit");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(StorageData storageDataObj, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        storageDataObj.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.StorageDataSet.Add(storageDataObj);

                        db.SaveChanges();

                        return RedirectToAction("Index");

                    }

                    catch (Exception e){
                        Console.WriteLine("Error encountered.");
                    }

                }

                else

                {

                    try

                    {

                        StorageData newStorageDataObj = db.StorageDataSet.Where(m => m.StorageDataID == storageDataObj.StorageDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newStorageDataObj != null)

                        {

                            newStorageDataObj.RegionID = storageDataObj.RegionID;

                            newStorageDataObj.CountryID = storageDataObj.CountryID;

                            newStorageDataObj.ConventionalStoragePotential = storageDataObj.ConventionalStoragePotential;

                            newStorageDataObj.RegionalPotential = storageDataObj.RegionalPotential;

                            newStorageDataObj.RegionalAssessment = storageDataObj.RegionalAssessment;

                            newStorageDataObj.Dataset = storageDataObj.Dataset;

                            newStorageDataObj.AssessmentMaturity = storageDataObj.AssessmentMaturity;

                            newStorageDataObj.Injection = storageDataObj.Injection;

                            newStorageDataObj.CommercialScaleInjection = storageDataObj.CommercialScaleInjection;

                            newStorageDataObj.KnowledgeDissemination = storageDataObj.KnowledgeDissemination;

                            newStorageDataObj.Summary = storageDataObj.Summary;

                            newStorageDataObj.Note = storageDataObj.Note;

                            newStorageDataObj.YearID = storageDataObj.YearID;

                            newStorageDataObj.Year = storageDataObj.Year;

                            newStorageDataObj.Region = storageDataObj.Region;

                            newStorageDataObj.RegionalPotentialCriteria = storageDataObj.RegionalPotentialCriteria;

                            newStorageDataObj.RegionalAssessmentCriteria = storageDataObj.RegionalAssessmentCriteria;

                            newStorageDataObj.DatasetCriteria = storageDataObj.DatasetCriteria;

                            newStorageDataObj.AssessmentMaturityCriteria = storageDataObj.AssessmentMaturityCriteria;

                            newStorageDataObj.InjectionCriteria = storageDataObj.InjectionCriteria;

                            newStorageDataObj.CommercialScaleInjectionCriteria = storageDataObj.CommercialScaleInjectionCriteria;

                            newStorageDataObj.KnowledgeDisseminationCriteria = storageDataObj.KnowledgeDisseminationCriteria;

                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_Edit", storageDataObj);

            }

            else

            {

                return View("Edit", storageDataObj);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                ViewBag.PossibleRegions = db.GetRegions();
                ViewBag.PossibleCountries = db.GetCountries();
                ViewBag.PossibleCriteria = db.GetStorageCriteria();
                ViewBag.PossibleYears = db.GetYearList();
                ViewBag.NullTrueFalse = db.GetTrueFalseNullList();

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    StorageData student = db.StorageDataSet.Where(m => m.StorageDataID == id).FirstOrDefault();

                    return PartialView("_Details", student);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Details");

            }

            else

            {
                ViewBag.PossibleRegions = db.GetRegions();
                ViewBag.PossibleCountries = db.GetCountries();
                ViewBag.PossibleCriteria = db.GetStorageCriteria();
                ViewBag.PossibleYears = db.GetYearList();
                ViewBag.NullTrueFalse = db.GetTrueFalseNullList();

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    StorageData student = db.StorageDataSet.Where(m => m.StorageDataID == id).FirstOrDefault();

                    return PartialView("Details", student);

                }

                ViewBag.IsUpdate = false;

                return View("Details");

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteStorageRecord(int id)

        {

            StorageData storageRecord = db.StorageDataSet.Where(m => m.StorageDataID == id).FirstOrDefault();

            if (storageRecord != null)

            {

                try

                {

                    db.StorageDataSet.Remove(storageRecord);

                    db.SaveChanges();

                }

                catch { }

            }

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

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
    public class CCSReadinessIndexDatasController : Controller
    {
        private GCCSIContext db = new GCCSIContext();

        // GET: CCSReadinessIndexDatas
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
                    var cCSReadinessIndexDatas = db.CCSReadinessIndexDatas.Include(c => c.Country).Include(c => c.Region).Include(c => c.Year);
                    return View(cCSReadinessIndexDatas.ToList());
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
                    return PartialView("_Edit", ccsIndexObj);
                }
                else
                {
                    return View("Edit", ccsIndexObj);
                }
                    

            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Edit");
            }
            else
            {
                return View("Edit");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(CCSReadinessIndexData ccsIndexObj, string cmd)

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

                return PartialView("_Edit", ccsIndexObj);

            }

            else

            {

                return View("Edit", ccsIndexObj);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)

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
                    return PartialView("_Details", ccsIndexObj);
                }
                else
                {
                    return View("Details", ccsIndexObj);
                }


            }

            ViewBag.IsUpdate = false;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Details");
            }
            else
            {
                return View("Details");
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCCSReadyData(int id)

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

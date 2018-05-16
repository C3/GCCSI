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
    public class LegalRegulatoryDatasController : Controller
    {
        private GCCSIContext db = new GCCSIContext();

        // GET: LegalRegulatoryDatas
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
                    var legalRegDataSet = db.LegalRegDataSet.Include(l => l.Country).Include(l => l.LRBand).Include(l => l.Region).Include(l => l.Year);
                    return View(legalRegDataSet.ToList());
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
            //Generic List Setter
            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.LRBandID = new SelectList(db.LRBandSet, "LRBandID", "BandName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (Request.IsAjaxRequest())

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    LegalRegulatoryData legalRegDataObject = db.LegalRegDataSet.Where(m => m.LegalRegulatoryDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", legalRegDataObject.CountryID);
                    ViewBag.LRBandID = new SelectList(db.LRBandSet, "LRBandID", "BandName", legalRegDataObject.LRBandID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", legalRegDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", legalRegDataObject.YearID);

                    return PartialView("_Edit", legalRegDataObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Edit");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    LegalRegulatoryData legalRegDataObject = db.LegalRegDataSet.Where(m => m.LegalRegulatoryDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", legalRegDataObject.CountryID);
                    ViewBag.LRBandID = new SelectList(db.LRBandSet, "LRBandID", "BandName", legalRegDataObject.LRBandID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", legalRegDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", legalRegDataObject.YearID);

                    return PartialView("Edit", legalRegDataObject);

                }

                ViewBag.IsUpdate = false;

                return View("Edit");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(LegalRegulatoryData legalRegDataObject, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        legalRegDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.LegalRegDataSet.Add(legalRegDataObject);

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

                        LegalRegulatoryData newLegalRegDataObject = db.LegalRegDataSet.Where(m => m.LegalRegulatoryDataID == legalRegDataObject.LegalRegulatoryDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newLegalRegDataObject != null)

                        {

                            newLegalRegDataObject.RegionID = legalRegDataObject.RegionID;

                            newLegalRegDataObject.CountryID = legalRegDataObject.CountryID;

                            newLegalRegDataObject.ClarityAndEfficiencyOfAdministrativeProcess = legalRegDataObject.ClarityAndEfficiencyOfAdministrativeProcess;

                            newLegalRegDataObject.ComprehensivenessOfFrameworks = legalRegDataObject.ComprehensivenessOfFrameworks;

                            newLegalRegDataObject.SitingAndEIA = legalRegDataObject.SitingAndEIA;

                            newLegalRegDataObject.StakeholderPublicConsultation = legalRegDataObject.StakeholderPublicConsultation;

                            newLegalRegDataObject.LiabilityAndClosure = legalRegDataObject.LiabilityAndClosure;

                            newLegalRegDataObject.TotalScore = legalRegDataObject.TotalScore;

                            newLegalRegDataObject.LRBandID = legalRegDataObject.LRBandID;

                            newLegalRegDataObject.ListOfLaws= legalRegDataObject.ListOfLaws;

                            newLegalRegDataObject.Attachment= legalRegDataObject.Attachment;

                            newLegalRegDataObject.Summary= legalRegDataObject.Summary;

                            newLegalRegDataObject.Note= legalRegDataObject.Note;

                            newLegalRegDataObject.YearID = legalRegDataObject.YearID;

                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_Edit", legalRegDataObject);

            }

            else

            {

                return View("Edit", legalRegDataObject);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)

        {
            //Generic List Setter
            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.LRBandID = new SelectList(db.LRBandSet, "LRBandID", "BandName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    LegalRegulatoryData lrDataObject = db.LegalRegDataSet.Where(m => m.LegalRegulatoryDataID== id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", lrDataObject.CountryID);
                    ViewBag.LRBandID = new SelectList(db.LRBandSet, "LRBandID", "BandName", lrDataObject.LRBandID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", lrDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", lrDataObject.YearID);

                    return PartialView("_Details", lrDataObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Details");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    LegalRegulatoryData lrDataObject = db.LegalRegDataSet.Where(m => m.LegalRegulatoryDataID== id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", lrDataObject.CountryID);
                    ViewBag.LRBandID = new SelectList(db.LRBandSet, "LRBandID", "BandName", lrDataObject.LRBandID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", lrDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", lrDataObject.YearID);

                    return PartialView("Details", lrDataObject);

                }

                ViewBag.IsUpdate = false;

                return View("Details");

            }

        }


        // GET: LegalRegulatoryDatas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {

            LegalRegulatoryData legalRegDataObject = db.LegalRegDataSet.Where(m => m.LegalRegulatoryDataID == id).FirstOrDefault();

            if (legalRegDataObject != null)

            {

                try

                {

                    db.LegalRegDataSet.Remove(legalRegDataObject);

                    db.SaveChanges();

                }

                catch { }

            }

            // TODO: Need to add in the redirect action to goto the correct tab.
            return RedirectToAction("Index");
        }

        // POST: LegalRegulatoryDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            LegalRegulatoryData legalRegulatoryData = db.LegalRegDataSet.Find(id);
            db.LegalRegDataSet.Remove(legalRegulatoryData);
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

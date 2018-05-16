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
    public class InherentInterestDataController : Controller
    {
        private GCCSIContext db = new GCCSIContext();

        // GET: InherentInterestData
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
                    var inherentInterestDataSet = db.InherentInterestDataSet.Include(i => i.Country).Include(i => i.Region).Include(i => i.Year);
                    return View(inherentInterestDataSet.ToList());
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
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (Request.IsAjaxRequest())

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    InherentInterestData inherentInterestDataObject = db.InherentInterestDataSet.Where(m => m.InherentInterestID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", inherentInterestDataObject.CountryID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", inherentInterestDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", inherentInterestDataObject.YearID);

                    return PartialView("_Edit", inherentInterestDataObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Edit");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    InherentInterestData inherentInterestDataObject = db.InherentInterestDataSet.Where(m => m.InherentInterestID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", inherentInterestDataObject.CountryID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", inherentInterestDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", inherentInterestDataObject.YearID);

                    return PartialView("Edit", inherentInterestDataObject);

                }

                ViewBag.IsUpdate = false;

                return View("Edit");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(InherentInterestData inherentInterestDataObject, string cmd)

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

                return PartialView("_Edit", inherentInterestDataObject);

            }

            else

            {

                return View("Edit", inherentInterestDataObject);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)

        {
            //Generic List Setter
            ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
            ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName");
            ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    InherentInterestData inherentInterestDataObject = db.InherentInterestDataSet.Where(m => m.InherentInterestID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", inherentInterestDataObject.CountryID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", inherentInterestDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", inherentInterestDataObject.YearID);

                    return PartialView("_Details", inherentInterestDataObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Details");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    InherentInterestData inherentInterestDataObject = db.InherentInterestDataSet.Where(m => m.InherentInterestID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", inherentInterestDataObject.CountryID);
                    ViewBag.RegionID = new SelectList(db.RD_RegionSet, "RegionID", "RegionName", inherentInterestDataObject.RegionID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", inherentInterestDataObject.YearID);

                    return PartialView("Details", inherentInterestDataObject);

                }

                ViewBag.IsUpdate = false;

                return View("Details");

            }

        }


        // GET: LegalRegulatoryDatas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
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

        // POST: InherentInterestData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            InherentInterestData inherentInterestData = db.InherentInterestDataSet.Find(id);
            db.InherentInterestDataSet.Remove(inherentInterestData);
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

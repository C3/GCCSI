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
    public class COREUpdatesController : Controller
    {
        private GCCSIContext db = new GCCSIContext();

        // GET: COREUpdates
        public ActionResult Index()
        {
            ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
            var cO2REUpdateDataSet = db.CO2REUpdateDataSet.Include(c => c.Area);

            //Test for admin role based auth.
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return View("AdminIndex", cO2REUpdateDataSet.ToList());
                }
            }

             return View(cO2REUpdateDataSet.Where(r => r.IncludeOnHome == true).ToList());

        }

        // GET: COREUpdates/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COREUpdates cOREUpdates = db.CO2REUpdateDataSet.Find(id);
            if (cOREUpdates == null)
            {
                return HttpNotFound();
            }
            return View(cOREUpdates);
        }

        // POST: COREUpdates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            COREUpdates cOREUpdates = db.CO2REUpdateDataSet.Find(id);
            db.CO2REUpdateDataSet.Remove(cOREUpdates);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(int? id)

        {

            ViewBag.AreaID = new SelectList(db.CO2REAreasSet, "AreaID", "AreaName");

            if (Request.IsAjaxRequest())

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    COREUpdates update = db.CO2REUpdateDataSet.Where(m => m.UpdateID == id).FirstOrDefault();

                    ViewBag.AreaID = new SelectList(db.CO2REAreasSet, "AreaID", "AreaName", update.AreaID);

                    return PartialView("_Edit", update);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_Edit");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    COREUpdates update = db.CO2REUpdateDataSet.Where(m => m.UpdateID == id).FirstOrDefault();

                    ViewBag.AreaID = new SelectList(db.CO2REAreasSet, "AreaID", "AreaName", update.AreaID);

                    return PartialView("Edit", update);

                }

                ViewBag.IsUpdate = false;

                return View("Edit");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditRecord(COREUpdates updateObj, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        updateObj.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.CO2REUpdateDataSet.Add(updateObj);

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

                        COREUpdates newUpdateObj = db.CO2REUpdateDataSet.Where(m => m.UpdateID == updateObj.UpdateID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newUpdateObj != null)

                        {

                            newUpdateObj.AreaID = updateObj.AreaID;

                            newUpdateObj.Title = updateObj.Title;

                            newUpdateObj.Description = updateObj.Description;

                            newUpdateObj.IncludeOnHome = updateObj.IncludeOnHome;

                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_Edit", updateObj);

            }

            else

            {

                return View("Edit", updateObj);

            }

        }

        [HttpGet]
        public ActionResult Details(int? id)

        {

            ViewBag.AreaID = new SelectList(db.CO2REAreasSet, "AreaID", "AreaName");

            if (Request.IsAjaxRequest())

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    COREUpdates detailsObj = db.CO2REUpdateDataSet.Where(m => m.UpdateID == id).FirstOrDefault();

                    ViewBag.AreaID = new SelectList(db.CO2REAreasSet, "AreaID", "AreaName", detailsObj.AreaID);

                    return PartialView("_Details", detailsObj);

                }

                ViewBag.IsUpdate = false;

                return RedirectToAction("Index");

            }

            else

            {

                if (id != null)

                {

                    ViewBag.IsUpdate = true;


                    COREUpdates detailsObj = db.CO2REUpdateDataSet.Where(m => m.UpdateID == id).FirstOrDefault();

                    ViewBag.AreaID = new SelectList(db.CO2REAreasSet, "AreaID", "AreaName", detailsObj.AreaID);

                    return PartialView("Details", detailsObj);

                }

                ViewBag.IsUpdate = false;
                return RedirectToAction("Index");

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCOREUpdate(int id)

        {

            COREUpdates updateRecord = db.CO2REUpdateDataSet.Where(m => m.UpdateID == id).FirstOrDefault();

            if (updateRecord != null)

            {

                try

                {

                    db.CO2REUpdateDataSet.Remove(updateRecord);

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

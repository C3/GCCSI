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
    public class PoliciesController : Controller
    {

        private GCCSIContext db = new GCCSIContext();

        // GET: Policies
        public ActionResult Index()
        {
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
        /// THIS IS THE SECTION FOR POLICYLISTCONTROLLER.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: PolicyListData
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyListIndex()
        {
            ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
            var policyListDataSet = db.PolicyListDataSet.Include(p => p.Country).Include(p => p.Year);
            return PartialView("PolicyListIndex", policyListDataSet.ToList());
        }

        // GET: PolicyListData/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyListDelete(int? id)
        {
            PolicyListData polictListObject = db.PolicyListDataSet.Where(m => m.PolicyListDataID == id).FirstOrDefault();

            if (polictListObject != null)

            {

                try

                {

                    db.PolicyListDataSet.Remove(polictListObject);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("Index");
        }

        // POST: PolicyListData/Delete/5
        [HttpPost, ActionName("PolicyListDelete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyListDeleteConfirmed(int id)
        {
            PolicyListData policyListData = db.PolicyListDataSet.Find(id);
            db.PolicyListDataSet.Remove(policyListData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyListRecord(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    PolicyListData policyList = db.PolicyListDataSet.Where(m => m.PolicyListDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", policyList.CountryID);
                    ViewBag.PolicyListOptionID = new SelectList(db.RD_PolicyListOptionSet, "PolicyListOptionID", "PolicyListOptionName", policyList.PolicyListOptionID);
                    ViewBag.PolicyListStatusID = new SelectList(db.RD_PolicyListStatusSet, "PolicyListStatusID", "PolicyListStatusName", policyList.PolicyListStatusID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", policyList.YearID);

                    return PartialView("_PolicyListEdit", policyList);

                }

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
                ViewBag.PolicyListOptionID = new SelectList(db.RD_PolicyListOptionSet, "PolicyListOptionID", "PolicyListOptionName");
                ViewBag.PolicyListStatusID = new SelectList(db.RD_PolicyListStatusSet, "PolicyListStatusID", "PolicyListStatusName");
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

                ViewBag.IsUpdate = false;

                return PartialView("_PolicyListEdit");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    PolicyListData policyList = db.PolicyListDataSet.Where(m => m.PolicyListDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", policyList.CountryID);
                    ViewBag.PolicyListOptionID = new SelectList(db.RD_PolicyListOptionSet, "PolicyListOptionID", "PolicyListOptionName", policyList.PolicyListOptionID);
                    ViewBag.PolicyListStatusID = new SelectList(db.RD_PolicyListStatusSet, "PolicyListStatusID", "PolicyListStatusName", policyList.PolicyListStatusID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", policyList.YearID);


                    return View("PolicyListEdit", policyList);

                }

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
                ViewBag.PolicyListOptionID = new SelectList(db.RD_PolicyListOptionSet, "PolicyListOptionID", "PolicyListOptionName");
                ViewBag.PolicyListStatusID = new SelectList(db.RD_PolicyListStatusSet, "PolicyListStatusID", "PolicyListStatusName");
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

                ViewBag.IsUpdate = false;

                return View("PolicyListEdit");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditPolicyListRecord(PolicyListData policyDataObject, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        policyDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.PolicyListDataSet.Add(policyDataObject);

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

                        PolicyListData newStorageDataObj = db.PolicyListDataSet.Where(m => m.PolicyListDataID == policyDataObject.PolicyListDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newStorageDataObj != null)

                        {

                            newStorageDataObj.CountryID = policyDataObject.CountryID;

                            newStorageDataObj.PolicyListOptionID = policyDataObject.PolicyListOptionID;

                            newStorageDataObj.PolicyName = policyDataObject.PolicyName;

                            newStorageDataObj.PolicyDescription = policyDataObject.PolicyDescription;

                            newStorageDataObj.PolicyURL = policyDataObject.PolicyURL;

                            newStorageDataObj.PolicyListStatusID = policyDataObject.PolicyListStatusID;

                            newStorageDataObj.Summary = policyDataObject.Summary;

                            newStorageDataObj.Note = policyDataObject.Note;

                            newStorageDataObj.YearID = policyDataObject.PolicyListStatusID;


                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_PolicyListEdit", policyDataObject);

            }

            else

            {

                return View("PolicyListEdit", policyDataObject);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PolicyListDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    PolicyListData student = db.PolicyListDataSet.Where(m => m.PolicyListDataID == id).FirstOrDefault();

                    return PartialView("_PolicyListDetails", student);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_PolicyListDetails");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    PolicyListData student = db.PolicyListDataSet.Where(m => m.PolicyListDataID == id).FirstOrDefault();

                    return PartialView("PolicyListDetails", student);

                }

                ViewBag.IsUpdate = false;

                return View("PolicyListDetails");

            }

        }



        // <--- This is the CONSOLIDTEDPOLICY SECTION --->
        // GET: ConsolidatedPolicyIndexes
        [Authorize(Roles = "Admin")]
        public ActionResult ConsolidatedPolicyIndex()
        {
            ViewBag.PossibleYears = db.RD_YearDataSet.ToList();
            ViewBag.PolicyStatus = db.RD_PolicyStatusSet.ToList();
            var consolidatedPolicyIndex = db.ConsolidatedPolicyIndex.Include(c => c.Country).Include(c => c.Region).Include(c => c.Year);
            return PartialView("ConsolidatedPolicyIndex", consolidatedPolicyIndex.ToList());
        }

        // GET: PolicyListData/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult ConsolidatedPolicyDelete(int? id)
        {
            ConsolidatedPolicyIndex consolPolicyObject = db.ConsolidatedPolicyIndex.Where(m => m.CPIID == id).FirstOrDefault();

            if (consolPolicyObject != null)

            {

                try

                {

                    db.ConsolidatedPolicyIndex.Remove(consolPolicyObject);

                    db.SaveChanges();

                }

                catch { }

            }

            return RedirectToAction("Index");
        }

        // POST: PolicyListData/Delete/5
        [HttpPost, ActionName("ConsolidatedPolicyDelete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult ConsolidatedPolicyDeleteConfirmed(int id)
        {
            PolicyListData policyListData = db.PolicyListDataSet.Find(id);
            db.PolicyListDataSet.Remove(policyListData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditConsolidatedPolicyRecord(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    PolicyListData policyList = db.PolicyListDataSet.Where(m => m.PolicyListDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", policyList.CountryID);
                    ViewBag.PolicyListOptionID = new SelectList(db.RD_PolicyListOptionSet, "PolicyListOptionID", "PolicyListOptionName", policyList.PolicyListOptionID);
                    ViewBag.PolicyListStatusID = new SelectList(db.RD_PolicyListStatusSet, "PolicyListStatusID", "PolicyListStatusName", policyList.PolicyListStatusID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", policyList.YearID);

                    return PartialView("_ConsolidatedPolicyEdit", policyList);

                }

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
                ViewBag.PolicyListOptionID = new SelectList(db.RD_PolicyListOptionSet, "PolicyListOptionID", "PolicyListOptionName");
                ViewBag.PolicyListStatusID = new SelectList(db.RD_PolicyListStatusSet, "PolicyListStatusID", "PolicyListStatusName");
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

                ViewBag.IsUpdate = false;

                return PartialView("_ConsolidatedPolicyEdit");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    PolicyListData policyList = db.PolicyListDataSet.Where(m => m.PolicyListDataID == id).FirstOrDefault();

                    ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName", policyList.CountryID);
                    ViewBag.PolicyListOptionID = new SelectList(db.RD_PolicyListOptionSet, "PolicyListOptionID", "PolicyListOptionName", policyList.PolicyListOptionID);
                    ViewBag.PolicyListStatusID = new SelectList(db.RD_PolicyListStatusSet, "PolicyListStatusID", "PolicyListStatusName", policyList.PolicyListStatusID);
                    ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName", policyList.YearID);


                    return View("ConsolidatedPolicyEdit", policyList);

                }

                ViewBag.CountryID = new SelectList(db.RD_CountrySet, "CountryID", "CountryName");
                ViewBag.PolicyListOptionID = new SelectList(db.RD_PolicyListOptionSet, "PolicyListOptionID", "PolicyListOptionName");
                ViewBag.PolicyListStatusID = new SelectList(db.RD_PolicyListStatusSet, "PolicyListStatusID", "PolicyListStatusName");
                ViewBag.YearID = new SelectList(db.RD_YearDataSet, "YearID", "YearName");

                ViewBag.IsUpdate = false;

                return View("ConsolidatedPolicyEdit");

            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddEditConsolidatedPolicyRecord(PolicyListData policyDataObject, string cmd)

        {

            if (ModelState.IsValid)

            {

                if (cmd == "Save")

                {

                    try

                    {
                        //Add appropriate update date field.
                        policyDataObject.UpdateDateTime = DateTime.Now;

                        //Add the object to the database if it is new.
                        db.PolicyListDataSet.Add(policyDataObject);

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

                        PolicyListData newStorageDataObj = db.PolicyListDataSet.Where(m => m.PolicyListDataID == policyDataObject.PolicyListDataID).FirstOrDefault();

                        //Set all the variables if the object is not null (Update)
                        if (newStorageDataObj != null)

                        {

                            newStorageDataObj.CountryID = policyDataObject.CountryID;

                            newStorageDataObj.PolicyListOptionID = policyDataObject.PolicyListOptionID;

                            newStorageDataObj.PolicyName = policyDataObject.PolicyName;

                            newStorageDataObj.PolicyDescription = policyDataObject.PolicyDescription;

                            newStorageDataObj.PolicyURL = policyDataObject.PolicyURL;

                            newStorageDataObj.PolicyListStatusID = policyDataObject.PolicyListStatusID;

                            newStorageDataObj.Summary = policyDataObject.Summary;

                            newStorageDataObj.Note = policyDataObject.Note;

                            newStorageDataObj.YearID = policyDataObject.PolicyListStatusID;


                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }

                    catch { }

                }

            }



            if (Request.IsAjaxRequest())

            {

                return PartialView("_ConsolidatedPolicyEdit", policyDataObject);

            }

            else

            {

                return View("ConsolidatedPolicyEdit", policyDataObject);

            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ConsolidatedPolicyDetails(int? id)

        {

            if (Request.IsAjaxRequest())

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    ConsolidatedPolicyIndex policyIndexObject = db.ConsolidatedPolicyIndex.Where(m => m.CPIID == id).FirstOrDefault();

                    return PartialView("_ConsolidatedPolicyDetails", policyIndexObject);

                }

                ViewBag.IsUpdate = false;

                return PartialView("_ConsolidatedPolicyDetails");

            }

            else

            {
                if (id != null)

                {

                    ViewBag.IsUpdate = true;

                    ConsolidatedPolicyIndex policyIndexObject = db.ConsolidatedPolicyIndex.Where(m => m.CPIID == id).FirstOrDefault();

                    return PartialView("ConsolidatedPolicyDetails", policyIndexObject);

                }

                ViewBag.IsUpdate = false;

                return View("ConsolidatedPolicyDetails");

            }

        }
    }
}

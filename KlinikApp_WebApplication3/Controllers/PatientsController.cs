using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KlinikApp_WebApplication3.Models;

namespace KlinikApp_WebApplication3.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private KlinikDbEntities db = new KlinikDbEntities();

        // GET: Patients
        //public ActionResult Index(string searchPatient)
        //{
        //    ViewBag.suchstring = searchPatient;
        //    var patients = db.Patients.Include(p => p.Bundesland);
        //    if(!String.IsNullOrEmpty(searchPatient))
        //    {
        //        patients = patients.Where(p => p.P_Firstname.StartsWith(searchPatient) || p.P_Lastname.StartsWith(searchPatient));
        //    }
        //    return View(patients.ToList());
        //}

        public ActionResult Index()
        {
            var patients = db.Patients.Include(p => p.Bundesland);
            return View(patients.ToList());
        }

        public ActionResult PatientSearch(string searchPatient, string sortOrder)
        {
            ViewBag.patientsearch = searchPatient;
            ViewBag.LastnameSortParm = String.IsNullOrEmpty(sortOrder) ? "ln_desc" : "";
            ViewBag.FirstnameSortParm = String.IsNullOrEmpty(sortOrder) ? "fn_desc" : "fn";
            var patients = db.Patients.Include(p => p.Bundesland);
            if (!String.IsNullOrEmpty(searchPatient))
            {
                patients = patients.Where(p => p.P_Firstname.StartsWith(searchPatient) || p.P_Lastname.StartsWith(searchPatient));
            }
            switch (sortOrder)
            {
                case "ln_desc":
                    patients = patients.OrderByDescending(p => p.P_Lastname);
                    break;
                case "fn":
                    patients = patients.OrderBy(p => p.P_Firstname);
                    break;
                case "fn_desc":
                    patients = patients.OrderByDescending(p => p.P_Firstname);
                    break;
                default:
                    patients = patients.OrderBy(p => p.P_Lastname);
                    break;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_patients", patients.ToList());
            }
            return View("Index", patients.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            ViewBag.P_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pat_Id,P_Lastname,P_Firstname,P_Birthday,P_Address,P_Plz,P_Bundesland")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.P_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name", patient.P_Bundesland);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.P_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name", patient.P_Bundesland);
            Session["sessionPatientId"] = patient.Pat_Id;
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "P_Lastname,P_Firstname,P_Birthday,P_Address,P_Plz,P_Bundesland")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Pat_Id = (int)Session["sessionPatientId"];
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                Session["sessionPatientId"] = null;
                return RedirectToAction("Index");
            }
            ViewBag.P_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name", patient.P_Bundesland);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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

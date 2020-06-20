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
    public class KliniksController : Controller
    {
        private KlinikDbEntities db = new KlinikDbEntities();

        // GET: Kliniks
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Loaddata()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.Kliniks.Include(k => k.Bundesland);
            return Json(new
            {
                data = data.Select(k => new
                {
                    address = k.K_Address,
                    plz = k.K_Plz,
                    ort = k.K_Ort,
                    bland = k.Bundesland.B_Name
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Kliniks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klinik klinik = db.Kliniks.Find(id);
            if (klinik == null)
            {
                return HttpNotFound();
            }
            ViewBag.K_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name", klinik.K_Bundesland);
            return View(klinik);
        }

        // POST: Kliniks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "K_Id,K_Address,K_Plz,K_Ort,K_Bundesland")] Klinik klinik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klinik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.K_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name", klinik.K_Bundesland);
            return View(klinik);
        }

        // GET: Kliniks/Delete/5
        

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

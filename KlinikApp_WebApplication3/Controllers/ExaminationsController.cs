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
    public class ExaminationsController : Controller
    {
        private KlinikDbEntities db = new KlinikDbEntities();
        
        public ActionResult Index(string id, string lastname, string type, string searchExam)
        {
            var examinations = db.Examinations.Include(e => e.Employee).Include(e => e.Examtype).Include(e => e.Klinik).Include(e => e.Patient);
            if (!String.IsNullOrEmpty(id))
            {
                int pid = int.Parse(id);
                if (!String.IsNullOrEmpty(type) && type.Equals("patient"))
                {
                    examinations = examinations.Where(ex => ex.Ex_Patient == pid);
                    ViewBag.patientExams = "from " + lastname;
                }
                else if (!String.IsNullOrEmpty(type) && type.Equals("employee"))
                {
                    examinations = examinations.Where(ex => ex.Ex_Employee == pid);
                    ViewBag.patientExams = "from " + lastname;
                }
            }
            if (!String.IsNullOrEmpty(searchExam))
            {
                examinations = examinations.Where(ex => ex.Ex_Exam == searchExam);
            }
            ViewBag.searchExam = new SelectList(db.Examtypes, "Exty_Id", "Exty_Name");
            if(Request.IsAjaxRequest())
            {
                return PartialView("_examinations", examinations.ToList());
            }
            return View(examinations.ToList());
        }

        // GET: Examinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examination examination = db.Examinations.Find(id);
            if (examination == null)
            {
                return HttpNotFound();
            }
            return View(examination);
        }

        // GET: Examinations/Create
        public ActionResult Create()
        {
            int emp_id = (int)Session["emp_id"];
            var employees = from e in db.Employees.Include(e => e.Klinik) select e;
            if (emp_id != 1)
            {
                employees = employees.Where(e => e.Emp_Id == emp_id);// select e;
            }
            ViewBag.Ex_Employee = new SelectList(employees, "Emp_Id", "Emp_Lastname");
            ViewBag.Ex_Exam = new SelectList(db.Examtypes, "Exty_Id", "Exty_Name");
            ViewBag.Ex_Klinik = new SelectList(employees, "Klinik.K_Id", "Klinik.K_Address");
            ViewBag.Ex_Patient = new SelectList(db.Patients, "Pat_Id", "P_Lastname");
            return View();
        }

        // POST: Examinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ex_Id,Ex_Exam,Ex_Date,Ex_Patient,Ex_Employee,Ex_Klinik")] Examination examination)
        {
            int emp_id = (int)Session["emp_id"];
            if (ModelState.IsValid)
            {
                db.Examinations.Add(examination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (emp_id == 1)
            {
                ViewBag.Ex_Employee = new SelectList(db.Employees, "Emp_Id", "Emp_Lastname", examination.Ex_Employee);
                ViewBag.Ex_Klinik = new SelectList(db.Kliniks, "K_Id", "K_Address", examination.Ex_Klinik);
            }
            else
            {
                var employees = from e in db.Employees.Include(e => e.Klinik) where e.Emp_Id == emp_id select e;
                ViewBag.Ex_Employee = new SelectList(employees, "Emp_Id", "Emp_Lastname", examination.Ex_Employee);
                ViewBag.Ex_Klinik = new SelectList(employees, "Klinik.K_Id", "Klinik.K_Address", examination.Ex_Klinik);
            }
            ViewBag.Ex_Exam = new SelectList(db.Examtypes, "Exty_Id", "Exty_Name", examination.Ex_Exam);
            ViewBag.Ex_Patient = new SelectList(db.Patients, "Pat_Id", "P_Lastname", examination.Ex_Patient);
            return View(examination);
        }

        // GET: Examinations/Edit/5
        public ActionResult Edit(int? id)
        {
            int emp_id = (int)Session["emp_id"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examination examination = db.Examinations.Find(id);
            if (examination == null)
            {
                return HttpNotFound();
            }
            if(emp_id != 1 && examination.Ex_Employee != emp_id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (emp_id == 1)
            {
                ViewBag.Ex_Employee = new SelectList(db.Employees, "Emp_Id", "Emp_Lastname", examination.Ex_Employee);
                ViewBag.Ex_Klinik = new SelectList(db.Kliniks, "K_Id", "K_Address", examination.Ex_Klinik);
            }
            else
            {
                var employees = from e in db.Employees.Include(e => e.Klinik) where e.Emp_Id == emp_id select e;
                ViewBag.Ex_Employee = new SelectList(employees, "Emp_Id", "Emp_Lastname", examination.Ex_Employee);
                ViewBag.Ex_Klinik = new SelectList(employees, "Klinik.K_Id", "Klinik.K_Address", examination.Ex_Klinik);
            }
            ViewBag.Ex_Patient = new SelectList(db.Patients, "Pat_Id", "P_Lastname", examination.Ex_Patient);
            ViewBag.Ex_Exam = new SelectList(db.Examtypes, "Exty_Id", "Exty_Name", examination.Ex_Exam);
            Session["sessionExamId"] = examination.Ex_Id;
            return View(examination);
        }

        // POST: Examinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ex_Exam,Ex_Date,Ex_Patient,Ex_Employee,Ex_Klinik")] Examination examination)
        {
            if (ModelState.IsValid)
            {
                examination.Ex_Id = (int)Session["sessionExamId"];
                db.Entry(examination).State = EntityState.Modified;
                db.SaveChanges();
                Session["sessionExamId"] = null;
                return RedirectToAction("Index");
            }
            ViewBag.Ex_Employee = new SelectList(db.Employees, "Emp_Id", "Emp_Lastname", examination.Ex_Employee);
            ViewBag.Ex_Exam = new SelectList(db.Examtypes, "Exty_Id", "Exty_Name", examination.Ex_Exam);
            ViewBag.Ex_Klinik = new SelectList(db.Kliniks, "K_Id", "K_Address", examination.Ex_Klinik);
            ViewBag.Ex_Patient = new SelectList(db.Patients, "Pat_Id", "P_Lastname", examination.Ex_Patient);
            return View(examination);
        }

        // GET: Examinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examination examination = db.Examinations.Find(id);
            if (examination == null)
            {
                return HttpNotFound();
            }
            return View(examination);
        }

        // POST: Examinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Examination examination = db.Examinations.Find(id);
            db.Examinations.Remove(examination);
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

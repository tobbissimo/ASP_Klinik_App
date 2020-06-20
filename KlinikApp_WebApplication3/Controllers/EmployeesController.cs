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
    public class EmployeesController : Controller
    {
        private KlinikDbEntities db = new KlinikDbEntities();

        // GET: Employees
        public ActionResult Index(string searchEmployee, string klinik)
        {
            var employees = db.Employees.Include(e => e.Bundesland).Include(e => e.Klinik);
            if (!String.IsNullOrEmpty(searchEmployee))
            {
                employees = employees.Where(e => e.Emp_Lastname.StartsWith(searchEmployee) || e.Emp_Firstname.StartsWith(searchEmployee));
            }
            if(!String.IsNullOrEmpty(klinik))
            {
                employees = employees.Where(e => e.Emp_Klinik == klinik);
            }
            return View(employees.ToList());
        }


        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if ((int)Session["emp_id"] != 1 && (int)Session["emp_id"] != id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            if ((int)Session["emp_id"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Emp_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name");
            ViewBag.Emp_Klinik = new SelectList(db.Kliniks, "K_Id", "K_Address");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Emp_Id,Emp_Lastname,Emp_Firstname,Emp_Birthday,Emp_Address,Emp_Plz,Emp_Salary,Emp_Bundesland,Emp_Klinik")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Emp_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name", employee.Emp_Bundesland);
            ViewBag.Emp_Klinik = new SelectList(db.Kliniks, "K_Id", "K_Address", employee.Emp_Klinik);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            if((int)Session["emp_id"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Emp_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name", employee.Emp_Bundesland);
            ViewBag.Emp_Klinik = new SelectList(db.Kliniks, "K_Id", "K_Address", employee.Emp_Klinik);
            Session["lastEmpNr"] = employee.Emp_Id;
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Emp_Lastname,Emp_Firstname,Emp_Birthday,Emp_Address,Emp_Plz,Emp_Salary,Emp_Bundesland,Emp_Klinik")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Emp_Id = (int)Session["lastEmpNr"];
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                Session["lastEmpNr"] = null;
                return RedirectToAction("Index");
            }
            ViewBag.Emp_Bundesland = new SelectList(db.Bundeslands, "B_Id", "B_Name", employee.Emp_Bundesland);
            ViewBag.Emp_Klinik = new SelectList(db.Kliniks, "K_Id", "K_Address", employee.Emp_Klinik);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            if ((int)Session["emp_id"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

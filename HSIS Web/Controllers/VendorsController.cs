using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSIS_Web.Models;

namespace HSIS_Web.Controllers
{
    public class VendorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vendors
        public ActionResult Index(string sortOrder, string searchString)
        {
            var vendors = from v in db.Vendors select v;
            ViewBag.FirstNameSort = sortOrder == "firstNameSortDescending" ? "firstNameSort" : "firstNameSortDescending";
            ViewBag.LastNameSort = sortOrder == "lastNameSortDescending" ? "lastNameSort" : "lastNameSortDescending";
            ViewBag.PhoneNumberSort = sortOrder == "phoneNumberSortDescending" ? "phoneNumberSort" : "phoneNumberSortDescending";
            ViewBag.EmailSort = sortOrder == "emailSortDescending" ? "emailSort" : "emailSortDescending";
            ViewBag.SalarySort = sortOrder == "salarySortDescending" ? "salarySort" : "salarySortDescending";
            if (!string.IsNullOrEmpty(searchString))
            {
                vendors = vendors.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstNameSort":
                {
                    vendors = vendors.OrderBy(v => v.FirstName);
                } break;
                case "firstNameSortDescending":
                {
                    vendors = vendors.OrderByDescending(v => v.FirstName);
                } break;
                case "lastNameSort":
                {
                    vendors = vendors.OrderBy(v => v.LastName);
                } break;
                case "lastNameSortDescending":
                {
                    vendors = vendors.OrderByDescending(v => v.LastName);
                } break;
                case "phoneNumberSort":
                {
                    vendors = vendors.OrderBy(v => v.PhoneNumber);
                } break;
                case "phoneNumberSortDescending":
                {
                    vendors = vendors.OrderByDescending(v => v.PhoneNumber);
                } break;
                case "emailSort":
                {
                    vendors = vendors.OrderBy(v => v.Email);
                } break;
                case "emailSortDescending":
                {
                    vendors = vendors.OrderByDescending(v => v.Email);
                } break;
                case "salarySort":
                {
                    vendors = vendors.OrderBy(v => v.Salary);
                } break;
                case "salarySortDescending":
                {
                    vendors = vendors.OrderByDescending(v => v.Salary);
                } break;
            }
            return View(vendors.ToList());
        }

        // GET: Vendors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // GET: Vendors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Email,Salary")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Email,Salary")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
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

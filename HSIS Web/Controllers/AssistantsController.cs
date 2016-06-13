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
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Vendor")]
    public class AssistantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assistants
        //public ActionResult Index()
        //{
        //    return View(db.Assistants.ToList());
        //}

        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.FirstNameSort = sortOrder == "firstNameSortDescending" ? "firstNameSort" : "firstNameSortDescending";
            ViewBag.LastNameSort = sortOrder == "lastNameSortDescending" ? "lastNameSort" : "lastNameSortDescending";
            ViewBag.NickNameSort = sortOrder == "nickNameSortDescending" ? "nickNameSort" : "nickNameSortDescending";
            ViewBag.PhoneNumberSort = sortOrder == "phoneNumberSortDescending" ? "phoneNumberSort" : "phoneNumberSortDescending";
            ViewBag.EmailSort = sortOrder == "emailSortDescending" ? "emailSort" : "emailSortDescending";
            ViewBag.SalarySort = sortOrder == "salarySortDescending" ? "salarySort" : "salarySortDescending";
            ViewBag.PhoneLineNumberSort = sortOrder == "phoneLineNumberDescending" ? "phoneLineNumberSort" : "phoneLineNumberDescending";
            var assistants = from a in db.Assistants select a;
            if (!string.IsNullOrEmpty(searchString))
            {
                assistants = assistants.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstNameSort":
                {
                    assistants = assistants.OrderBy(a => a.FirstName);
                } break;
                case "firstNameSortDescending":
                {
                    assistants = assistants.OrderByDescending(a => a.FirstName);
                } break;
                case "lastNameSort":
                {
                    assistants = assistants.OrderBy(a => a.LastName);
                } break;
                case "lastNameSortDescending":
                {
                    assistants = assistants.OrderByDescending(a => a.LastName);
                } break;
                case "nickNameSort":
                {
                    assistants = assistants.OrderBy(a => a.NickName);
                } break;
                case "nickNameSortDescending":
                {
                    assistants = assistants.OrderByDescending(a => a.NickName);
                } break;
                case "phoneNumberSort":
                {
                    assistants = assistants.OrderBy(a => a.PhoneNumber);
                } break;
                case "phoneNumberSortDescending":
                {
                    assistants = assistants.OrderByDescending(a => a.PhoneNumber);
                } break;
                case "emailSort":
                {
                    assistants = assistants.OrderBy(a => a.Email);
                } break;
                case "emailSortDescending":
                {
                    assistants = assistants.OrderByDescending(a => a.Email);
                } break;
                case "salarySort":
                {
                    assistants = assistants.OrderBy(a => a.Salary);
                } break;
                case "salarySortDescending":
                {
                    assistants = assistants.OrderByDescending(a => a.Salary);
                } break;
                case "phoneLineNumberSort":
                {
                    assistants = assistants.OrderBy(a => a.PhoneLineNumber);
                } break;
                case "phoneLineNumberDescending":
                {
                    assistants = assistants.OrderByDescending(a => a.PhoneLineNumber);
                } break;
                default:
                {
                    assistants = assistants.OrderBy(a => a.FirstName);
                } break;
            }
            return View(assistants.ToList());
        }

        // GET: Assistants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistant assistant = db.Assistants.Find(id);
            if (assistant == null)
            {
                return HttpNotFound();
            }
            return View(assistant);
        }

        // GET: Assistants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assistants/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,NickName,PhoneNumber,Email,Salary,PhoneLineNumber")] Assistant assistant)
        {
            if (ModelState.IsValid)
            {
                db.Assistants.Add(assistant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assistant);
        }

        // GET: Assistants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistant assistant = db.Assistants.Find(id);
            if (assistant == null)
            {
                return HttpNotFound();
            }
            return View(assistant);
        }

        // POST: Assistants/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,NickName,PhoneNumber,Email,Salary,PhoneLineNumber")] Assistant assistant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assistant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assistant);
        }

        // GET: Assistants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistant assistant = db.Assistants.Find(id);
            if (assistant == null)
            {
                return HttpNotFound();
            }
            return View(assistant);
        }

        // POST: Assistants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assistant assistant = db.Assistants.Find(id);
            db.Assistants.Remove(assistant);
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

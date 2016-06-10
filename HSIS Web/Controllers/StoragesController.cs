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
    public class StoragesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Storages
        public ActionResult Index(string sortOrder, string searchString)
        {
            var storages = from s in db.Storages select s;
            ViewBag.TitleSort = sortOrder == "titleSortDescending" ? "titleSort" : "titleSortDescending";
            ViewBag.CapasitySort = sortOrder == "capasitySortDescending" ? "capasitySort" : "capasitySortDescending";
            if (!string.IsNullOrEmpty(searchString))
            {
                storages = storages.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "titleSort":
                {
                    storages = storages.OrderBy(s => s.Title);
                } break;
                case "titleSortDescending":
                {
                    storages = storages.OrderByDescending(s => s.Title);
                } break;
                case "capasitySort":
                {
                    storages = storages.OrderBy(s => s.Capasity);
                } break;
                case "capasitySortDescending":
                {
                    storages = storages.OrderByDescending(s => s.Capasity);
                } break;
                default:
                {
                    storages = storages.OrderBy(s => s.Title);
                } break;
            }
            return View(storages.ToList());
        }

        // GET: Storages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storage storage = db.Storages.Find(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            return View(storage);
        }

        // GET: Storages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Storages/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Capasity")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                db.Storages.Add(storage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(storage);
        }

        // GET: Storages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storage storage = db.Storages.Find(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            return View(storage);
        }

        // POST: Storages/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Capasity")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storage);
        }

        // GET: Storages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storage storage = db.Storages.Find(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            return View(storage);
        }

        // POST: Storages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Storage storage = db.Storages.Find(id);
            db.Storages.Remove(storage);
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

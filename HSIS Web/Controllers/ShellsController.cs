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
    public class ShellsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shells
        public ActionResult Index(string sortOrder, string searchString)
        {
            var shells1 = db.Shells.Include(s => s.Storage);
            var shells = from s in shells1 select s;
            ViewBag.StorageSort = sortOrder == "storageSortDescending" ? "storageSort" : "storageSortDescending";
            ViewBag.TitleSort = sortOrder == "titleSortDescending" ? "titleSort" : "titleSortDescending";
            ViewBag.CapasitySort = sortOrder == "capasitySortDescending" ? "capasitySort" : "capasitySortDescending";
            if (!string.IsNullOrEmpty(searchString))
            {
                shells = shells.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "storageSort":
                {
                    shells = shells.OrderBy(s => s.Storage.Title);
                } break;
                case "storageSortDescending":
                {
                    shells = shells.OrderByDescending(s => s.Storage.Title);
                } break;
                case "titleSort":
                {
                    shells = shells.OrderBy(s => s.Title);
                } break;
                case "titleSortDescending":
                {
                    shells = shells.OrderByDescending(s => s.Title);
                } break;
                case "capasitySort":
                {
                    shells = shells.OrderBy(s => s.Capasity);
                } break;
                case "capasitySortDescending":
                {
                    shells = shells.OrderByDescending(s => s.Capasity);
                } break;
                default:
                {
                    shells = shells.OrderBy(s => s.Title);
                } break;
            }
            return View(shells.ToList());
        }

        // GET: Shells/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shell shell = db.Shells.Find(id);
            if (shell == null)
            {
                return HttpNotFound();
            }
            return View(shell);
        }

        // GET: Shells/Create
        public ActionResult Create()
        {
            ViewBag.StorageId = new SelectList(db.Storages, "Id", "Title");
            return View();
        }

        // POST: Shells/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Capasity,StorageId")] Shell shell)
        {
            if (ModelState.IsValid)
            {
                db.Shells.Add(shell);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StorageId = new SelectList(db.Storages, "Id", "Title", shell.StorageId);
            return View(shell);
        }

        // GET: Shells/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shell shell = db.Shells.Find(id);
            if (shell == null)
            {
                return HttpNotFound();
            }
            ViewBag.StorageId = new SelectList(db.Storages, "Id", "Title", shell.StorageId);
            return View(shell);
        }

        // POST: Shells/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Capasity,StorageId")] Shell shell)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shell).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StorageId = new SelectList(db.Storages, "Id", "Title", shell.StorageId);
            return View(shell);
        }

        // GET: Shells/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shell shell = db.Shells.Find(id);
            if (shell == null)
            {
                return HttpNotFound();
            }
            return View(shell);
        }

        // POST: Shells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shell shell = db.Shells.Find(id);
            db.Shells.Remove(shell);
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

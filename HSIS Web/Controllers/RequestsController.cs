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
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        [Authorize(Roles = "Admin,Assistant,Client,Vendor")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            var requests1 = db.Requests.Include(r => r.Assistant).Include(r => r.Client);
            var requests = from s in requests1 select s;
            ViewBag.AssistantSort = sortOrder == "assistantSortDescending" ? "assistantSort" : "assistantSortDescending";
            ViewBag.ClientSort = sortOrder == "clientSortDescending" ? "clientSort" : "clientSortDescending";
            ViewBag.TimeSort = sortOrder == "timeSortDescending" ? "timeSort" : "timeSortDescending";
            ViewBag.ProblemSort = sortOrder == "problemSortDescending" ? "problemSort" : "problemSortDescending";
            ViewBag.SolutionSort = sortOrder == "solutionSortDescending" ? "solutionSort" : "solutionSortDescending";
            ViewBag.CostSort = sortOrder == "costSortDescending" ? "costSort" : "costSortDescending";
            if (!string.IsNullOrEmpty(searchString))
            {
                requests = requests.Where(s => s.Assistant.FullName.Contains(searchString)
                                       || s.Client.FullName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "assistantSort":
                {
                    requests = requests.OrderBy(r => r.Assistant.FirstName);
                } break;
                case "assistantSortDescending":
                {
                    requests = requests.OrderByDescending(r => r.Assistant.FirstName);
                } break;
                case "clientSort":
                {
                    requests = requests.OrderBy(r => r.Client.FirstName);
                } break;
                case "clientSortDescending":
                {
                    requests = requests.OrderByDescending(r => r.Client.FirstName);
                } break;
                case "timeSort":
                {
                    requests = requests.OrderBy(r => r.Time);
                } break;
                case "timeSortDescending":
                {
                    requests = requests.OrderByDescending(r => r.Time);
                } break;
                case "problemSort":
                {
                    requests = requests.OrderBy(r => r.Problem);
                } break;
                case "problemSortDescending":
                {
                    requests = requests.OrderByDescending(r => r.Problem);
                } break;
                case "solutionSort":
                {
                    requests = requests.OrderBy(r => r.Solution);
                } break;
                case "solutionSortDescending":
                {
                    requests = requests.OrderByDescending(r => r.Solution);
                } break;
                case "costSort":
                {
                    requests = requests.OrderBy(r => r.Cost);
                } break;
                case "costSortDescending":
                {
                    requests = requests.OrderByDescending(r => r.Cost);
                } break;
                default:
                {
                    requests = requests.OrderBy(r => r.Problem);
                } break;
            }
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        [Authorize(Roles = "Admin,Assistant")]
        public ActionResult Create()
        {
            ViewBag.AssistantId = new SelectList(db.Assistants, "Id", "FullName");
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName");
            return View();
        }

        // POST: Requests/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant")]
        public ActionResult Create([Bind(Include = "Id,Time,Problem,Solution,Cost,ClientId,AssistantId")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssistantId = new SelectList(db.Assistants, "Id", "FullName", request.AssistantId);
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", request.ClientId);
            return View(request);
        }

        // GET: Requests/Edit/5
        [Authorize(Roles = "Admin,Assistant")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssistantId = new SelectList(db.Assistants, "Id", "FullName", request.AssistantId);
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", request.ClientId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant")]
        public ActionResult Edit([Bind(Include = "Id,Time,Problem,Solution,Cost,ClientId,AssistantId")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssistantId = new SelectList(db.Assistants, "Id", "FullName", request.AssistantId);
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", request.ClientId);
            return View(request);
        }

        // GET: Requests/Delete/5
        [Authorize(Roles = "Admin,Assistant")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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

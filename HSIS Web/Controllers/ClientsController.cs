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
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        [Authorize(Roles = "Admin,Assistant,Vendor,Client")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            var clients = from c in db.Clients select c;
            ViewBag.FirstNameSort = sortOrder == "firstNameSortDescending" ? "firstNameSort" : "firstNameSortDescending";
            ViewBag.LastNameSort = sortOrder == "lastNameSortDescending" ? "lastNameSort" : "lastNameSortDescending";
            ViewBag.PhoneNumberSort = sortOrder == "phoneNumberSortDescending" ? "phoneNumberSort" : "phoneNumberSortDescending";
            ViewBag.EmailSort = sortOrder == "emailSortDescending" ? "emailSort" : "emailSortDescending";
            ViewBag.DiscountSort = sortOrder == "discountSortDescending" ? "discountSort" : "discountSortDescending";
            if(!string.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstNameSort":
                {
                    clients = clients.OrderBy(c => c.FirstName);
                } break;
                case "firstNameSortDescending":
                {
                    clients = clients.OrderByDescending(c => c.FirstName);
                } break;
                case "lastNameSort":
                {
                    clients = clients.OrderBy(c => c.LastName);
                } break;
                case "lastNameSortDescending":
                {
                    clients = clients.OrderByDescending(c => c.LastName);
                } break;
                case "phoneNumberSort":
                {
                    clients = clients.OrderBy(c => c.PhoneNumber);
                } break;
                case "phoneNumberSortDescending":
                {
                    clients = clients.OrderByDescending(c => c.PhoneNumber);
                } break;
                case "emailSort":
                {
                    clients = clients.OrderBy(c => c.Email);
                } break;
                case "emailSortDescending":
                {
                    clients = clients.OrderByDescending(c => c.Email);
                } break;
                case "discountSort":
                {
                    clients = clients.OrderBy(c => c.Discount);
                } break;
                case "discountSortDescending":
                {
                    clients = clients.OrderByDescending(c => c.Discount);
                } break;
                default:
                {
                    clients = clients.OrderByDescending(c => c.FirstName);
                } break;

            }
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin,Assistant,Vendor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant,Vendor")]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Email,Discount")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin,Assistant,Vendor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant,Vendor")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Email,Discount")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin,Assistant,Vendor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
